Shader "Unlit/TVSnow"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

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
                //o.normal = UnityObjectToWorldNormal(v.normal);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float t = _Time + 123.0; // tweak the start moment
                float ta = t * 0.654321;
                float tb = t * ( ta * 0.123456);
    
                float c = Noise21(i.uv, ta, tb);

                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                return float4(c.xxx, 1.0);
            }
            ENDCG
        }
    }
}
