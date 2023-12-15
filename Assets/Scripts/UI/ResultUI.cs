using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultUI : MonoBehaviour
{

    public GameObject clear;
    public GameObject over;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.player != null)
        {
            transform.parent = GameManager.Instance.player.transform;
            transform.localPosition = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Gameover()
    {
        over.SetActive(true);
    }

    public void Gameclear()
    {
        clear.SetActive(true);
    }

    public void onClickReturnMain()
    {
        AudioManager.Instance.PlayBGM(AudioManager.BGM.titleBGM);
        AudioManager.Instance.PlayButton(AudioManager.Button.select);
        SceneManager.LoadScene("MainScene");
    }
}
