using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    MeshRenderer m_Renderer;
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
        m_Renderer = GetComponent<MeshRenderer>();
    }

    private void LateUpdate()
    {
        if(!GameManager.Instance.isUiOn)
        {
            m_Renderer.enabled = false;
        }
        else if(GameManager.Instance.isUiOn)
        {
            m_Renderer.enabled = true;
        }
    }
    // Update is called once per frame
}
