using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBackgroundStream : MonoBehaviour
{
    ParticleSystem myPS;
    ParticleSystem.MainModule myPSmain;
    // Start is called before the first frame update
    void Start()
    {
        myPS = GetComponent<ParticleSystem>();
        myPSmain = myPS.main;
    }

    // Update is called once per frame
    void Update()
    {
        myPSmain.simulationSpeed = GameManager.Instance.cutrrentspeed * 0.002f;
    }
}
