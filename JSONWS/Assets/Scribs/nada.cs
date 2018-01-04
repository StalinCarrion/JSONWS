using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;

public class nada : MonoBehaviour
{

    public int min, max;
    public GameObject sphere;
    public LineRenderer traza;

    void Start()
    {

        var nEsferas = 8;

        traza = gameObject.AddComponent<LineRenderer>();
        traza.startWidth = traza.endWidth = .2f;
        traza.positionCount = nEsferas;
        
        for (int i = 0; i < nEsferas; i++)
        {
            var posiciona = GeneratedPosition();
            Instantiate(sphere, posiciona, Quaternion.identity);
            traza.SetPosition(i, posiciona);
        }

    }

    Vector3 GeneratedPosition()
    {
        float x, y, z;
        x = (float)Random.Range(min, max);
        y = (float)Random.Range(min, max);
        z = (float)Random.Range(min, max);
        return new Vector3(x, y, z);

    }
}
