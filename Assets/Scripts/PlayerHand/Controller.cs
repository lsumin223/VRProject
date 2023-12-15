using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public enum LR
    {
        left, right
    }

    public enum Type
    {
        speed, height
    }

    
    public Playermove player;
    public LR lr = LR.left;
    public Type type = Type.speed;
    public Hand gamehand;
    public Transform handrotation;
    public Vector3 startposition;
    private float wichi;
    float currentZ;
    public bool moved;
    public bool boosted;
    public bool breaked;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.player;
        startposition = transform.localPosition;
    }


    private void Update()
    {
        if (gamehand)
        {
            moved = true;
            player.state = Playermove.State.moving;
            gamehand.transform.position = transform.position;
            gamehand.transform.rotation = handrotation.rotation;
            Vector3 LhandPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LHand);
            Vector3 RhandPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand);

            // State.control�� �� ������ LHand�� Z�� ��ġ�� ����
            if (wichi == 0f)
            {
                switch (gamehand.lr)
                {
                    case Hand.LR.left:
                        wichi = LhandPosition.z;
                        break;
                    case Hand.LR.right:
                        wichi = RhandPosition.z;
                        break;
                }

            }

            //currentZ = LhandPosition.z;
            // ���� �����ӿ����� LHand�� Z�� ��ġ
            switch (gamehand.lr)
            {
                case Hand.LR.left:
                    currentZ = LhandPosition.z;
                    break;
                case Hand.LR.right:
                    currentZ = RhandPosition.z;
                    break;
            }

            // ���� �������� ��� ��� (Z ���� ������ ���)
            if (currentZ < wichi - 0.02f)
            {
                // - �������� ������
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - 0.1f * Time.deltaTime * 1.2f);
            }
            // ���� ������ �� ��� (Z ���� ������ ���)
            else if (currentZ > wichi + 0.02f)
            {
                // + �������� ������
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 0.1f * Time.deltaTime * 1.2f);
            }

            // Z�� ��ġ�� Clamp�Ͽ� Ư�� ���� ���� ����
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                transform.localPosition.y,
                Mathf.Clamp(transform.localPosition.z, startposition.z - 0.02f, startposition.z + 0.05f)
            );

            switch (lr)
            {
                case LR.left:
                    if (GameManager.Instance.boosted == false)
                    {
                        if (transform.localPosition.z < startposition.z)
                        {
                            GameManager.Instance.cutrrentspeed -= Time.deltaTime * GameManager.Instance.breaktime;

                            if (OVRInput.Get(OVRInput.Button.Two, OVRInput.Controller.LTouch))
                            {

                                if (boosted)
                                {
                                    player.outBoost();
                                    boosted = false;
                                }

                                if (!breaked)
                                {
                                    breaked = true;
                                    player.Break();
                                }

                            }
                            else if (OVRInput.GetUp(OVRInput.Button.Two, OVRInput.Controller.LTouch))
                            {
                                if (transform.localPosition.z < startposition.z)
                                {
                                    breaked = false;
                                    player.Breakout();
                                }
                            }
                        }
                        else if (transform.localPosition.z > startposition.z)
                        {

                            GameManager.Instance.cutrrentspeed += Time.deltaTime * GameManager.Instance.accelerator;
                        }
                        else
                        {
                            return;
                        }
                    }


                    break;
                case LR.right:
                    if (OVRInput.Get(OVRInput.Button.Two, OVRInput.Controller.RTouch))
                    {

                        if (transform.localPosition.z > startposition.z)
                        {
                            if (breaked)
                            {
                                player.Breakout();
                                breaked = false;
                            }
                            if (!boosted)
                            {
                                boosted = true;
                                player.InBoost();
                            }

                        }
                    }
                    else if (OVRInput.GetUp(OVRInput.Button.Two, OVRInput.Controller.RTouch))
                    {
                        if (transform.localPosition.z > startposition.z)
                        {
                            boosted = false;
                            player.outBoost();
                        }
                    }


                    break;

            }

        }
        else
        {
            // �ٸ� ���¿����� wichi�� �ʱ�ȭ
            transform.localPosition = startposition;
            player.state = Playermove.State.notmoving;
            wichi = 0f;


            if (moved)
            {
                if (lr == LR.left)
                {
                    if (breaked)
                    {
                        breaked = true;
                        player.Breakout();
                    }
                }
                else if (lr == LR.right)
                {
                    if (boosted)
                    {
                        player.outBoost();
                        boosted = false;
                    }
                }
                moved = false;

            }

            /*if (player.state == Playermove.State.notmoving)
            {
                GameManager.Instance.cutrrentspeed -= Time.deltaTime * GameManager.Instance.breaktime;
            }*/
        }
    }
    // Update is called once per frame
    void LateUpdate()
    {
        


        //������ �и� �ӵ� ����, �ڷ� ���� �ӵ� �϶�





        /*switch (type)
        {
            case Type.speed:
                if (transform.localPosition.z < startposition.z)
                {
                    //GameManager.Instance.mapSpeed -= Time.deltaTime * 15;
                }
                else if (transform.localPosition.z > startposition.z)
                {
                    //GameManager.Instance.mapSpeed += Time.deltaTime * 15;
                }
                else
                {
                    return;
                }

                break;
            case Type.height:
                if (transform.localPosition.z < startposition.z)
                {
                    //Debug.Log("��");
                    //GameManager.Instance.flighttype = 1f;
                }
                else if (transform.localPosition.z > startposition.z)
                {
                    //Debug.Log("�Ʒ�");
                    //GameManager.Instance.flighttype = -1f;
                }
                else if (transform.localPosition.z == startposition.z)
                {
                    //Debug.Log("����");
                    //GameManager.Instance.flighttype = 0f;
                }
                break;
            default:
                break;
        }*/


    }

    public void Gripcontol(GameObject hand)
    {
        //gamehand = hand.GetComponent<Hand>();

        switch (lr)
        {
            case LR.left:
                if(hand.GetComponent<Hand>().lr == Hand.LR.left)
                {
                    gamehand = hand.GetComponent<Hand>();
                    gamehand.state = Hand.Handstate.control;
                }
                break;
            case LR.right:
                if (hand.GetComponent<Hand>().lr == Hand.LR.right)
                {
                    gamehand = hand.GetComponent<Hand>();
                    gamehand.state = Hand.Handstate.control;
                }
                break;
            
        }

    }


}
