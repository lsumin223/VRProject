using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    [SerializeField]
    public GameObject[] uiList;

    [SerializeField]
    public GameObject[] stageButtons;

    public GameObject titleUI;
    private int remember = 4;
    private int stageNumber = 99;

    public GameObject stage1UI;
    public GameObject stage2UI;
    public GameObject stage3UI;
    public Text stageText;
    public Text stageName;
    public RawImage StageImage;

    // Start is called before the first frame update
    void Start()
    {
       // player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClickPlay()
    {
        AudioManager.Instance.PlayButton(AudioManager.Button.select);
        Debug.Log("Play");
        uiList[0].SetActive(true);
        remember = 0;
        titleUI.SetActive(false);

    }

    public void onClickHTP()
    {
        AudioManager.Instance.PlayButton(AudioManager.Button.select);

        Debug.Log("How to Play");
        uiList[1].SetActive(true);
        remember = 1;
        titleUI.SetActive(false);

    }

    public void onClickSetting()
    {
        AudioManager.Instance.PlayButton(AudioManager.Button.select);

        Debug.Log("Setting");
        uiList[2].SetActive(true);
        remember = 2;
        titleUI.SetActive(false);

    }

    public void onClickClose()
    {
        AudioManager.Instance.PlayButton(AudioManager.Button.select);
        Debug.Log("Close");
        uiList[remember].SetActive(false);
        titleUI.SetActive(true);
        if (remember == 0)
        {
            stage1UI.SetActive(false);
            stage2UI.SetActive(false);
            stage3UI.SetActive(false);
            for (int i = 0; i < stageButtons.Length; i++)
            {
                stageButtons[i].SetActive(true);
            }
        }


    }

    public void onClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

    public void onClickStage1()
    {
        AudioManager.Instance.PlayButton(AudioManager.Button.select);

        stage1UI.SetActive(true);
        for (int i = 0; i < stageButtons.Length; i++)
        {
            stageButtons[i].SetActive(false);
        }
        stageName.text = "노베나";
        stageText.text = "노베나 행성 입니다";
    }

    public void onClickStage2()
    {
        AudioManager.Instance.PlayButton(AudioManager.Button.select);

        stage2UI.SetActive(true);
        for (int i = 0; i < stageButtons.Length; i++)
        {
            stageButtons[i].SetActive(false);
        }
        stageName.text = "솔란트";
        stageText.text = "솔란트 행성입니다";
    }

    public void onClickStage3()
    {
        AudioManager.Instance.PlayButton(AudioManager.Button.select);

        stage3UI.SetActive(true);
        for (int i = 0; i < stageButtons.Length; i++)
        {
            stageButtons[i].SetActive(false);
        }
        stageName.text = "";
        stageText.text = "행성입니다";
    }

    public void onClickStageClose()
    {
        AudioManager.Instance.PlayButton(AudioManager.Button.select);

        stage1UI.SetActive(false);
        stage2UI.SetActive(false);
        stage3UI.SetActive(false);
        for (int i = 0; i < stageButtons.Length; i++)
        {
            stageButtons[i].SetActive(true);
        }
    }

    public void onClickStage1Start()
    {
        AudioManager.Instance.PlayButton(AudioManager.Button.select);
        SceneManager.LoadScene("Stage1Scene");
        AudioManager.Instance.PlayBGM(AudioManager.BGM.firstBGM);


        /*Debug.Log("실행");

        if (playerMoveScript != null && !playerMoveScript.enabled)
        {
            playerMoveScript.enabled = true;
        }

        Spawn.SetActive(true);
        GameManager.Instance.isUiOn = false;
        GameManager.Instance.isLive = true;

        gameObject.SetActive(false);*/

    }
    public void onClickStage2Start()
    {
        AudioManager.Instance.PlayButton(AudioManager.Button.select);
        SceneManager.LoadScene("Stage2Scene");
        AudioManager.Instance.PlayBGM(AudioManager.BGM.secondBGM);
    }

    public void onClickStage3Start()
    {
        AudioManager.Instance.PlayButton(AudioManager.Button.select);
        SceneManager.LoadScene("Stage3Scene");
        AudioManager.Instance.PlayBGM(AudioManager.BGM.thirdBGM);
    }
}
