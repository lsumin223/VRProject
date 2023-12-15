using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlUIScript : MonoBehaviour
{
    public Controller controller;
    public Image image;
    public Sprite go;
    public Sprite down;
    public Sprite breaking;
    public Sprite ready;

    // Start is called before the first frame update
    void Start()
    {
        controller = transform.parent.GetComponent<Controller>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(controller.moved)
        {
            if(controller.transform.localPosition.z < controller.startposition.z)
            {
                if(controller.breaked)
                {
                    image.sprite = breaking;
                }
                else
                {
                    image.sprite = down;
                }
                
            }
            else if(controller.transform.localPosition.z > controller.startposition.z)
            {
                image.sprite = go;
            }
        }
        else
        {
            image.sprite = ready;
        }
    }
}
