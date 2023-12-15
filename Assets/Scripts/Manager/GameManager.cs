using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public enum PlayerState
    {
        handling, nohandling
    }

    public enum SceneType { title, ingame }

    public SceneType sceneType = SceneType.ingame;
    [Header("Game Start")]
    public float startCount;
    public GameObject spawnPoint;

    [Header("# PlayerData")]
    public PlayerData playerData;
    public bool isUiOn;
    public PlayerState playerState = PlayerState.nohandling;
    public Playermove player;

    [Header("Game Status")]
    public bool isLive = true;
    public bool isPause = false;
    public int playerLife = 3;

    public float xRange;
    public float yRange;
    public float cutrrentscore = 100000;
    public float playRate;
    public float clearPlayRate;


    [Header("Player Status")]
    public float cutrrentspeed;
    public float minspeed = 10f;
    public float maxspeed = 180f;

    public float accelerator;
    public float breaktime;
    public float boostgage;
    public float maxboostgage;
    public float boostRecharge;
    public float boostDecrease = 14f;



    [Header("Bools")]
    public float hitrate;
    public bool hitted;

    public bool boosted;
    public bool boostRecharging = true;
    public bool canboost = true;


    public bool gameWin;
    public bool gameOver;

    [Header("Manager / UI")]
    public PoolManager MapSpawner;
    public PoolManager EnemySpanwer;
    public PoolManager ThEnemySpanwer;

    public float mapSpeed;
    public float flighttype;
    public VRInformation vRInformation;

    public IngameUI ingameUI;
    public ResultUI result;
    public GameObject pauseCanvas;
    public bool isGameOver;


    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        Time.timeScale = 1f;

        if (sceneType == SceneType.ingame)
        {
            GameObject Player = Instantiate(playerData.playerPrefab, new Vector3(0, yRange, 0), Quaternion.identity);
            player = Player.GetComponent<Playermove>();
            player.headsetrotation = vRInformation;
            maxspeed = playerData.maxspeed;
            minspeed = playerData.minspeed;
            accelerator = playerData.accelerator;
            breaktime = playerData.breaktime;
            maxboostgage = playerData.maxboostguage;
            boostRecharge = playerData.boostRecharge;
            boostDecrease = playerData.boostDecrease;
            isLive = false;
            isGameOver = false;
        }

        //isLive = false;
#if Oculus

        if (sceneType == SceneType.ingame)
        {
            player.UiOnOff(isUiOn);
        }
#endif
    }

    void Update()
    {
        if (sceneType != SceneType.ingame)
            return;

        GameStart();

        if (!isLive) return;

        if (sceneType == SceneType.ingame && !isGameOver)
            Gamepause();

        cutrrentscore -= Time.deltaTime * 50f;
        playRate += Time.deltaTime * cutrrentspeed * 0.1f;

        if (clearPlayRate <= playRate)
        {
            GameClear();
        }

        if (!boosted)
        {
            //cutrrentspeed = Mathf.Clamp(cutrrentspeed, minspeed, maxspeed);

            if (cutrrentspeed >= maxspeed)
            {
                cutrrentspeed = maxspeed;
            }
        }

        if (cutrrentspeed < minspeed)
        {
            cutrrentspeed = minspeed;
        }

        boostgage = Mathf.Clamp(boostgage, -10, maxboostgage);

        if (boosted)
        {
            boostgage -= (Time.deltaTime * boostDecrease);
        }

        if (boostgage >= 0 && canboost)
        {
            if (boostRecharging)
            {
                boostgage += (Time.deltaTime * boostRecharge);
            }

        }
    }

    private void LateUpdate()
    {
        if (sceneType == SceneType.ingame)
        {
            if (cutrrentscore < 0)
            {
                GameOver();
            }

            if (clearPlayRate <= playRate)
            {
                GameClear();
            }

            player.UiOnOff(isUiOn);
        }

    }

    public void GameOver()
    {
        /*isGameOver = true;
        isUiOn = true;
        result.Gameover();
        isLive = false;
        isPause = true;
        GameStop();*/
        AudioManager.Instance.PlayBGM(AudioManager.BGM.titleBGM);
        SceneManager.LoadScene("DefeatScene");
        Debug.Log("게임 클리어");
    }

    public void GameClear()
    {
        AudioManager.Instance.PlayBGM(AudioManager.BGM.titleBGM);
        SceneManager.LoadScene("ClearScene");
        Debug.Log("게임 클리어");
    }

    public void GameStop()
    {
        switch (isPause)
        {
            case true:
                Time.timeScale = 0f;
                break;

            case false:
                Time.timeScale = 1f;
                break;
        }
    }

    public void BoostRepair()
    {
        StartCoroutine(Boostrepair());
    }
    public IEnumerator Boostrepair()
    {
        boosted = false;
        canboost = false;
        yield return new WaitForSeconds(2f);
        canboost = true;
        boostgage = maxboostgage * 0.3f;
    }

    public void GameStart()
    {
        startCount -= Time.deltaTime;
        if (startCount <= 0)
        {
            isLive = true;
            string currentSceneName = SceneManager.GetActiveScene().name;
            if (sceneType == SceneType.ingame)
            { 
                    spawnPoint.SetActive(true);
                    if (currentSceneName == "Stage3Scene")
                    {
                        GameObject pigeonSpawner = GameObject.Find("PigeonSpawn");
                        ThirdStageSpawner script = pigeonSpawner.GetComponent<ThirdStageSpawner>();
                        script.enabled = true;
                    }
            }
        }
    }

    public void isStop()
    {
        isPause = true;
        isUiOn = true;
        GameStop();
        pauseCanvas.SetActive(true);
        player.UiOnOff(isUiOn);
    }

    public void isRestart()
    {
        isPause = false;
        isUiOn = false;
        GameStop();
        pauseCanvas.SetActive(false);
        
        player.UiOnOff(isUiOn);
    }


    public void Hit(float hit)
    {
        //hitrate = hit;
        cutrrentscore -= hit;
        ingameUI.LetsHit1(hit);
    }

    public void Gamepause()
    {
#if Oculus
        if (isPause)
        {
            if (OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.RTouch))
                isRestart();
        }
        else
        {
            if (OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.RTouch))
                isStop();
        }
        
#endif
#if PC
        if (Input.GetKeyDown(KeyCode.Space))
            isStop();

        if (Input.GetKeyDown(KeyCode.Escape))
            isRestart();
#endif
    }
}
