using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handle : MonoBehaviour
{
    public Playermove player;
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = player.playermodel.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
