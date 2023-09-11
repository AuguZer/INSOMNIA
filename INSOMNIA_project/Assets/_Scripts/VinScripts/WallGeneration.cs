using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGeneration : MonoBehaviour
{
    [SerializeField] GameObject _endCornerPrefab;
    [SerializeField] GameObject _wallPrefab;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateWall()
    {
        Instantiate(_endCornerPrefab);
        Instantiate(_wallPrefab);
    }
}
