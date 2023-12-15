using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    public GameObject heartImage1;
    public GameObject heartImage2;
    public GameObject heartImage3;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.Instance.playerLife >= 3)
        {
            heartImage3.SetActive(true);
        }
        else heartImage3.SetActive(false);

        if (GameManager.Instance.playerLife >= 2)
        {
            heartImage2.SetActive(true);
        }
        else heartImage2.SetActive(false);

        if (GameManager.Instance.playerLife >= 1)
        {
            heartImage1.SetActive(true);
        }
        else heartImage1.SetActive(false);


    }
}
