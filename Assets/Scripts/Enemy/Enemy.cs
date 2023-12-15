using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{

    Vector3 movevec = Vector3.forward;
    public Transform removeTR;
    float speed;
    
    public float rate;

    void Start()
    {
        GameObject[] RemoveTag = GameObject.FindGameObjectsWithTag("Remove");
        if (RemoveTag.Length > 0)
        {
            removeTR = RemoveTag[0].transform;
        }
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {

        speed = (GameManager.Instance.cutrrentspeed * rate);
        transform.Translate(movevec * speed * Time.deltaTime);

        if (transform.position.z <= removeTR.position.z)
        {
            gameObject.SetActive(false);
        }


    }


}
