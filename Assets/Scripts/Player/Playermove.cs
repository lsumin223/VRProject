using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Playermove : MonoBehaviour
{
    public enum State
    {
        moving, notmoving
    }
    public State state = State.notmoving;

    [Header("# Player status")]
    public float speed = 5f;
    public float rollangle;
    public float pitchangle;
    public float xRange;
    public float yRange;
    public Vector3 playerposition;
    public VRInformation headsetrotation;
    public Transform playermodel;
    public GameObject boostereffect;

    private Vector3 direction;
    public float H;
    public float V;
    private float B;
    private float S;

    bool boosted = false;
    bool breaked = false;

    public GameObject[] uipointers;

    private Animator myAnim;
    // Start is called before the first frame update
    void Start()
    {
        if(playermodel != null)
        {
            myAnim = playermodel.GetComponent<Animator>();
        }
        yRange = GameManager.Instance.yRange; 
        xRange = GameManager.Instance.xRange;
        headsetrotation = GameManager.Instance.vRInformation;
        transform.position = new Vector3(0, yRange, 0);
        playerposition = transform.position;
        speed = GameManager.Instance.playerData.flightspeed;
        rollangle = GameManager.Instance.playerData.rollangle;
        AudioManager.Instance.PlaySfxLoop(AudioManager.SFX.EngineSound);
        
    }

    // Update is called once per frame
    void Update()
    {
#if PC
        H = Input.GetAxis("Horizontal");
        V = Input.GetAxis("Vertical");

        direction.x = H;
        direction.y = V;

        if(Input.GetKey(KeyCode.R))
        {
            GameManager.Instance.cutrrentspeed += Time.deltaTime * GameManager.Instance.accelerator;
        }
        else if(Input.GetKey(KeyCode.F))
        {
            GameManager.Instance.cutrrentspeed -= Time.deltaTime * GameManager.Instance.breaktime;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (breaked)
            {
                Breakout();
                breaked = false;
            }
            if (!boosted)
            {
                boosted = true;
                InBoost();
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            outBoost();
            boosted = false;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (boosted)
            {
                outBoost();
                boosted = false;
            }

            if (!breaked)
            {
                breaked = true;
                Break();
            }
        }
        else if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            
            Breakout();
            breaked = false;
        }
#endif
#if Oculus

        //V = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick,OVRInput.Controller.RTouch).y;

        //V = GameManager.Instance.flighttype;


        if (state == State.moving)
        {
            if(myAnim)
            {
                myAnim.SetBool("handling", true);
            }
            
            if (headsetrotation.pitch <= -pitchangle)
            {
                V = -(headsetrotation.pitch / 30);
            }
            else if (headsetrotation.pitch >= pitchangle)
            {
                V = -(headsetrotation.pitch / 30);
            }
            else
            {
                V = 0;
            }

            if (headsetrotation.roll <= -rollangle)
            {
                H = -(headsetrotation.roll / 40);
            }
            else if (headsetrotation.roll >= rollangle)
            {
                H = -(headsetrotation.roll / 40);
            }
            else
            {
                H = 0;
            }

            if (playermodel)
            {
                playermodel.rotation = Quaternion.Euler(headsetrotation.pitch / 1.8f, 0, headsetrotation.roll / 1.2f);
            }




        }

        else if (state == State.notmoving)
        {
            if(myAnim)
            {
                myAnim.SetBool("handling", false);
            }
            V = 0;
            H = 0;
        }

        direction.y = V;
        direction.x = H;
#endif
        if(boostereffect != null)
        {
            if (GameManager.Instance.boosted)
            {
                boostereffect.SetActive(true);
            }
            else if (!GameManager.Instance.boosted)
            {
                boostereffect.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (transform.position.x < playerposition.x - xRange)
        {
            transform.position = new Vector3(playerposition.x - xRange, transform.position.y, 0);
        }
        else if (transform.position.x > xRange)
        {
            transform.position = new Vector3(playerposition.x + xRange, transform.position.y, 0);
        }
        else if (transform.position.y < playerposition.y - yRange)
        {
            transform.position = new Vector3(transform.position.x, playerposition.y - yRange, 0);
        }
        else if (transform.position.y > playerposition.y + yRange)
        {
            transform.position = new Vector3(transform.position.x, playerposition.y + yRange, 0);
        }
        else
        {
            transform.Translate(direction * speed * Time.fixedDeltaTime);
        }
    }

    private void LateUpdate()
    {
        if(GameManager.Instance.boosted == true)
        {
            GameManager.Instance.cutrrentspeed = GameManager.Instance.playerData.boostspeed;
        }

        if(GameManager.Instance.boostgage <= 0)
        {
            GameManager.Instance.BoostRepair();
        }
    }

    public void InBoost()
    {
        S = GameManager.Instance.cutrrentspeed;
        AudioManager.Instance.PlaySFX(AudioManager.SFX.Booster);
        GameManager.Instance.boosted = true;
        GameManager.Instance.boostRecharging = false;
    }

    public void outBoost()
    {
        if (GameManager.Instance.cutrrentspeed >= GameManager.Instance.maxspeed)
        {
            GameManager.Instance.cutrrentspeed = GameManager.Instance.maxspeed;
        }
        else
        {
            GameManager.Instance.cutrrentspeed = S;
        }
        GameManager.Instance.boosted = false;
        GameManager.Instance.boostRecharging = true;

    }
    public void Break()
    {
        B = GameManager.Instance.breaktime;
        AudioManager.Instance.PlaySFX(AudioManager.SFX.Break);
        GameManager.Instance.breaktime = GameManager.Instance.maxspeed / 3;
    }

    public void Breakout()
    {
        GameManager.Instance.breaktime = B;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    public void UiOnOff(bool onoff)
    {

        for (int i = 0; i < uipointers.Length; i++)
        {
            uipointers[i].SetActive(onoff);
        }
    }

}
