using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRReset : MonoBehaviour
{
    public VRInformation vrinfo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown, OVRInput.Controller.RHand))
        {
            StartCoroutine(Reset());
        }
    }

    IEnumerator Reset()
    {
        vrinfo.enabled = false;
        yield return new WaitForSeconds(3f);
        vrinfo.enabled = true;
    }
}
