using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeListener : MonoBehaviour {

    public void MyPointerEnter()
    {
        GetComponent<Renderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
    }

    public void MyPointerLeave()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
}
