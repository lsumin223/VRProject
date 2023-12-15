using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Handray : MonoBehaviour
{
    public enum LR
    {
        left, right
    }

    public LR lr = LR.left;

    Vector3 ControllerPosition;
    public Transform ovrCameraRig;
    Quaternion ControllerRotation;
    private LineRenderer myLr;
    public Transform crosshair;
    public GameObject hitObject;
    bool Triggerenter;

    private bool isDragging = false;


    // Start is called before the first frame update
    void Start()
    {

        transform.parent = null;
#if PC
        
#endif
        myLr = GetComponent<LineRenderer>();
      

    }
   
    // Update is called once per frame
    void Update()
    {
#if Oculus

        crosshair.gameObject.SetActive(true);
        //OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LHand);
        switch (lr)
        {
            case LR.left:
                ControllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LHand);
                ControllerRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LHand);
                break;
            case LR.right:
                ControllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand);
                ControllerRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RHand);
                break;
            default:
                break;
        }


        transform.localPosition = ovrCameraRig.TransformPoint(ControllerPosition);
        transform.localRotation = ControllerRotation;
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;
        int layer = 1 << LayerMask.NameToLayer("UI");
        if (Physics.Raycast(ray, out hitInfo, 600, layer))
        {
            myLr.SetPosition(0, ray.origin);
            myLr.SetPosition(1, hitInfo.point);
            crosshair.position = hitInfo.point;

            hitObject = hitInfo.collider.gameObject;
            Debug.Log("Hit Object: " + hitInfo.collider.gameObject.name);
        switch (lr)
            {
                case LR.left:
                    Triggerenter = OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger , OVRInput.Controller.LTouch);
                    break;
                case LR.right:
                    Triggerenter = OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch);
                    break;
            }

            if(Triggerenter)
            {
                Click();

                if (hitObject.GetComponent<Slider>() != null)
                {
                    Debug.Log(isDragging); 
                    isDragging = true;
                }
            }

            if (isDragging && hitObject.GetComponent<Slider>() != null)
            {
                // Slider 핸들 이동 처리
                Debug.LogError("핸들 선택됨");
                MoveSliderHandle();
            }
            else
            {
                isDragging = false;
            }
        }
        else
        {
            crosshair.gameObject.SetActive(false);
            myLr.SetPosition(0, ray.origin);
            myLr.SetPosition(1, ray.origin + transform.forward * 200);
        }

#endif
    }

    public void Click()
    {
        if (hitObject.GetComponent<Button>() != null)
        {
            hitObject.GetComponent<Button>().onClick.Invoke();
        }

    }

    void MoveSliderHandle()
    {
        if (hitObject != null)
        {
            Slider slider = hitObject.GetComponent<Slider>();
            if (slider != null)
            {
                RectTransform handleRect = slider.fillRect.GetComponent<RectTransform>();
                float handleWidth = handleRect.rect.width;

                float normalizedValue = Mathf.InverseLerp(slider.minValue, slider.maxValue, slider.value);
                float handlePosition = Mathf.Lerp(0, slider.GetComponent<RectTransform>().rect.width - handleWidth, normalizedValue);

                handleRect.anchoredPosition = new Vector2(handlePosition, handleRect.anchoredPosition.y);
                slider.value = Mathf.Lerp(slider.minValue, slider.maxValue, handlePosition / (slider.GetComponent<RectTransform>().rect.width - handleWidth));
            }
        }
    }
}
