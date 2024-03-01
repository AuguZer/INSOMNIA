Shader "Unlit/TVSnow"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ScreenColor("Screen Color", Color) = (0.0, 0.0, 0.0, 0.0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"  

            #define SNOW_ON

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                float4 tangent : TANGENT; // xyz = tangent direction, w = tangent sign
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 normal : TEXCOORD2;
                float3 tangent : TEXCOORD3;
                float3 bitangent : TEXCOORD4;
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD1;
                LIGHTING_COORDS(5,6)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _ScreenColor;

            float ScreenCurve(float2 uv)
            {
                float2 uvCentered = uv * 2 - 1;
                float x = sqrt(pow(1, 2) - pow(uvCentered.x, 2));
                float y = sqrt(pow(1, 2) - pow(uvCentered.y, 2));
                return x * y;
            }

            float Noise21(float2 p, float ta, float tb) 
            {
                return frac(sin(p.x*ta+p.y*tb)*5678.0);
            }

            v2f vert (appdata v)
            {
                v2f o;

                o.worldPos = mul(UNITY_MATRIX_M, float4(v.vertex.xyz, 1.0));
                v.vertex.y = ScreenCurve(v.uv) * 0.05;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.tangent = UnityObjectToWorldDir(v.tangent.xyz);
                o.bitangent = cross(o.normal, o.tangent);
                o.bitangent *= v.tangent.w * unity_WorldTransformParams.w; // Correctly handle flipping/mirroring
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.uv = v.uv;
                TRANSFER_VERTEX_TO_FRAGMENT(o);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                #ifdef SNOW_ON
                    float t = _Time + 123.0; // tweak the start moment
                    float ta = t * 0.654321;
                    float tb = t * ( ta * 0.123456);
                    float c = Noise21(i.uv, ta, tb);
                    float4 col = tex2D(_MainTex, i.uv);
                    return float4(c.xxx, 1.0);
                #else

                    float3 N = normalize(i.normal);
                    float3 L = normalize(UnityWorldSpaceLightDir(i.worldPos));
                    float attenuation = LIGHT_ATTENUATION(i);
                    float3 lambert = saturate(dot(N, L));
                    float3 diffuseLight = (lambert * attenuation);


                    float4 col = tex2D(_MainTex, i.uv);
                    return float4(diffuseLight, 1.0) + _ScreenColor;
                    return _ScreenColor;
                #endif
                    
            }
            ENDCG
        }
    }
}
