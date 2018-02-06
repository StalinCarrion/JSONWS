using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cambiarSkybox : MonoBehaviour {

    public Material skyone;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            RenderSettings.skybox = skyone;
        }
    }
    void Update()
    {
        transform.Rotate(0.0f, 40.0f * Time.deltaTime, 0.0f);
    }
}
