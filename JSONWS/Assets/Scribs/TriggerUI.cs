using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TriggerUI : MonoBehaviour
{

    public Transform target;
    public Collider UICollider;
    public Graphic[] UIGraphic;
    public bool targetLookAt = false;
    public float timeFade = 0f;


    float fade = 0f;

    // Update is called once per frame
    void FixedUpdate()
    {

        bool state = this.UICollider.bounds.Contains(this.target.position);

        if (state)
        {
            if (this.fade <= this.timeFade)
            {
                if (this.fade <= 0f)
                {
                    this.fade = 0f;
                    foreach (Graphic graphic in this.UIGraphic)
                    {
                        graphic.enabled = true;
                    }
                }
                this.fade += Time.fixedDeltaTime;
                float alfa;
                if (this.timeFade <= 0f)
                {
                    alfa = 1f;
                }
                else
                {
                    alfa = this.fade / this.timeFade;
                }

                foreach (Graphic graphic in this.UIGraphic)
                {
                    Color color = graphic.color;
                    color.a = alfa;
                    graphic.color = color;
                }
            }

            if (this.targetLookAt)
            {
                foreach (Graphic graphic in this.UIGraphic)
                {
                    Vector3 position = target.position;
                    position.y = this.transform.position.y;
                    graphic.transform.rotation = Quaternion.LookRotation(this.transform.position - position);
                }
            }
        }
        else
        {
            if (this.fade >= 0f)
            {
                this.fade -= Time.fixedDeltaTime;
                if (this.fade <= 0f)
                {
                    foreach (Graphic graphic in this.UIGraphic)
                    {
                        graphic.enabled = false;
                    }
                }
                else
                {
                    float alfa;
                    if (this.timeFade <= 0f)
                    {
                        alfa = 0f;
                    }
                    else
                    {
                        alfa = this.fade / this.timeFade;
                    }
                    foreach (Graphic graphic in this.UIGraphic)
                    {
                        Color color = graphic.color;
                        color.a = alfa;
                        graphic.color = color;
                    }
                }
            }
        }
    }
}