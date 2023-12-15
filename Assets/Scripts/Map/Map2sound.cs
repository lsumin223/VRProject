using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map2sound : MonoBehaviour
{
    // Start is called before the first frame update
    public float cooltime;
    private float currentCooltime;
    // Start is called before the first frame update
    void Start()
    {
        currentCooltime = cooltime;
    }

    // Update is called once per frame
    void Update()
    {
        currentCooltime -= Time.deltaTime;

        if (currentCooltime <= 0)
        {
            AudioManager.Instance.PlaySFX(AudioManager.SFX.Whale);
            currentCooltime = cooltime;
        }
    }
}
