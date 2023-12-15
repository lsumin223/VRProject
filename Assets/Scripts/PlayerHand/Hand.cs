using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Hand;

public class Hand : MonoBehaviour
{
    public enum LR
    {
        left, right
    }
    public enum Handstate
    {
        open, grip, control
    }

    public LR lr = LR.left;
    public Handstate state = Handstate.open;
    public Transform ovrCameraRig;
    public Controller controller;
    Vector3 ControllerPosition;
    Quaternion ControllerRotation;

    bool isHandTriggerUp;
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
#if Oculus
        controller = null;
        
#endif
    }

    // Update is called once per frame
    void Update()
    {
#if PC
        if(controller != null)
        {
            state = Handstate.control;
            transform.position = controller.transform.position;
            transform.rotation = controller.handrotation.rotation;
        }
        
#endif
#if Oculus

        switch (lr)
        {
            case LR.left:
                isHandTriggerUp = OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
                ControllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LHand);
                ControllerRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LHand);
                break;
            case LR.right:
                isHandTriggerUp = OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);
                ControllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand);
                ControllerRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RHand);
                break;
            default:
                break;
        }
        


        // 왼손 컨트롤러의 Transform을 직접 추적

        if(state == Handstate.open || state == Handstate.grip)
        {
            transform.localPosition = ovrCameraRig.TransformPoint(ControllerPosition);
            transform.localRotation = ControllerRotation;
        }
        
        switch(lr)
        {
            case LR.left:
                if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
                {
                    state = Handstate.grip;
                    OVRInput.SetControllerVibration(0.1f, 0.1f, OVRInput.Controller.LHand);
                    GetComponent<MeshRenderer>().material.color = Color.red;
                }
                if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
                {
                    state = Handstate.open;
                    //OVRInput.SetControllerVibration(0.1f, 0.1f, OVRInput.Controller.LHand);
                    GetComponent<MeshRenderer>().material.color = Color.white;
                }
                if (state == Handstate.control)
                {
                    GetComponent<MeshRenderer>().material.color = Color.green;
                    //transform.position = new Vector3(controller.transform.position.x, controller.transform.position.y, controller.transform.position.z);
                }
                break;
            case LR.right:
                if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
                {
                    state = Handstate.grip;
                    OVRInput.SetControllerVibration(0.1f, 0.1f, OVRInput.Controller.RHand);
                    GetComponent<MeshRenderer>().material.color = Color.red;
                }
                if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
                {
                    state = Handstate.open;
                    //OVRInput.SetControllerVibration(0.1f, 0.1f, OVRInput.Controller.RHand);
                    GetComponent<MeshRenderer>().material.color = Color.white;
                }
                if (state == Handstate.control)
                {
                    GetComponent<MeshRenderer>().material.color = Color.green;
                    //transform.position = new Vector3(controller.transform.position.x, controller.transform.position.y, controller.transform.position.z);
                }
                break;
        }

        if(controller)
        {
            switch(lr)
            {
                case LR.left:
                    if(OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
                    {
                        controller.gamehand = null;
                        controller = null;
                    }
                    break;
                case LR.right:
                    if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
                    {
                        controller.gamehand = null;
                        controller = null;
                    }
                    break;
            }
        }
        
#endif
    }

    private void OnTriggerStay(Collider collider)
    {

        if(collider.GetComponent<Controller>() != null)
        {
            
            //Debug.Log("접촉함");
            //if (state == Handstate.grip)
            switch(lr)
            {
                case LR.left:
                    if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
                    {
                        controller = collider.GetComponent<Controller>();
                        collider.GetComponent<Controller>().Gripcontol(gameObject);
                    }
                    break;

                case LR.right:
                    if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
                    {
                        controller = collider.GetComponent<Controller>();
                        collider.GetComponent<Controller>().Gripcontol(gameObject);
                    }
                    break;
            }
            

            switch(lr)
            {
                case LR.left:
                    if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
                    {
                        controller = collider.GetComponent<Controller>();
                        collider.GetComponent<Controller>().Gripcontol(gameObject);
                    }
                    break;
                case LR.right:
                    if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
                    {
                        controller = collider.GetComponent<Controller>();
                        collider.GetComponent<Controller>().Gripcontol(gameObject);
                    }
                    break;
            }
            
        }
    }

    /*private void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<Controller>() != null)
        {
            controller.gamehand = null;
            controller = null;
            Debug.Log("뗌");

        }
    }*/

}
