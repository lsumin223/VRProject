using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        transform.parent.parent = GameManager.Instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
    public void onClickRestart()
    {
        AudioManager.Instance.PlayButton(AudioManager.Button.select);
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "Stage1Scene")
        {
            SceneManager.LoadScene("Stage1Scene");
        AudioManager.Instance.PlayBGM(AudioManager.BGM.firstBGM);
    }
        else if (currentSceneName == "Stage2Scene")
        {
            SceneManager.LoadScene("Stage2Scene");
            AudioManager.Instance.PlayBGM(AudioManager.BGM.secondBGM);
        }
        else if (currentSceneName == "Stage3Scene")
        {
            SceneManager.LoadScene("Stage3Scene");
        AudioManager.Instance.PlayBGM(AudioManager.BGM.thirdBGM);
    }
        Time.timeScale = 1f;
        GameManager.Instance.isPause = false;
        GameManager.Instance.isUiOn = false;
    }

    public void onClickContinue()
    {
        AudioManager.Instance.PlayButton(AudioManager.Button.select);
        GameManager.Instance.isRestart();
    }

    public void onClickExit()
    {
        AudioManager.Instance.PlayBGM(AudioManager.BGM.titleBGM);
        AudioManager.Instance.PlayButton(AudioManager.Button.select);
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;
    }

}
