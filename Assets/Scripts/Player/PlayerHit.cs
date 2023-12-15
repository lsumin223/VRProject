using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public float coolTime;
    public float currentcoolTime;
    public bool isHit;

    // Start is called before the first frame update
    void Start()
    {
        coolTime = GameManager.Instance.playerData.hitCooltime;
        isHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        HitTimer();
    }

    void HitTimer()
    {
        if (isHit)
        {
            currentcoolTime -= Time.deltaTime;

            if (currentcoolTime <= 0f)
            {
                currentcoolTime = coolTime;
                isHit = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);

        if(other.gameObject.CompareTag("Kill") && !isHit)
        {
            float maphit = other.GetComponent<MapHit>().hitDamage;
            maphit = maphit * ((100f - GameManager.Instance.playerData.DamageDecreasePercent) / 100);
            GameManager.Instance.Hit(maphit);
            AudioManager.Instance.PlaySFX(AudioManager.SFX.Hit);
            OVRInput.SetControllerVibration(2f, 0.2f, OVRInput.Controller.LHand);
            OVRInput.SetControllerVibration(2f, 0.2f, OVRInput.Controller.RHand);
            currentcoolTime = coolTime;
            isHit = true;
            
            
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {

        }
    }

}
