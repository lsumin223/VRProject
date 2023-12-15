using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spacecraft : MonoBehaviour
{
    public float flySpeed = 20f;
    Vector3 move;
    float H;
    float V;

    void Start()
    {

    }

    void Update()
    {
        transform.Translate(Vector3.forward * flySpeed * Time.deltaTime);

        H = Input.GetAxisRaw("Horizontal");
        V = Input.GetAxisRaw("Vertical");
        move = new Vector3(H, V, 0).normalized;

        transform.position += move * 10f * Time.deltaTime;
    }
}
