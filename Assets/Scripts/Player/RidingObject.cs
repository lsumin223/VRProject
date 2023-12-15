using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidingObject : MonoBehaviour
{
    public GameObject PC_object;
    public GameObject VR_object;
    // Start is called before the first frame update
    void Start()
    {
#if PC
        PC_object.SetActive(true);
#endif
#if Oculus
        VR_object.SetActive(true);
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
