using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class VRButton : MonoBehaviour
{
    public Image BackgroundImage;
    public Color NormalColor;
    public Color HighlightColor;
    public Color otroColor;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        OVRTouchpad.Create();

    }
    public void OnGazeEnter()
    {
        BackgroundImage.color = HighlightColor;
        //SceneManager.LoadScene("escena2");

    }

    public void OnGazeExit()
    {
        BackgroundImage.color = NormalColor;
    }

    public void OnClick()
    {
        BackgroundImage.color = otroColor;
    }


}
