using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountUI : MonoBehaviour
{
    private bool isCheck;

    public Text countText;
    public int countDownStartNum;
    public float countDownNum;
    public string countDownEndMsg;

    // Start is called before the first frame update

    private void Start()
    {
        isCheck = false;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        gameObject.transform.parent = player.transform;
    }

    void Update()
    {

        if (GameManager.Instance.startCount <= 3f && !isCheck)
        {
            StartCountDown();
            AudioManager.Instance.PlaySFX(AudioManager.SFX.countdown);
        }

    }

    private void StartCountDown()
    {

        countDownNum = Mathf.CeilToInt(GameManager.Instance.startCount);
        countText.gameObject.SetActive(true);

        StartCoroutine(CountDownCo());
    }

    private IEnumerator CountDownCo()
    {
        isCheck = true;

        if (countDownNum > 0)
        {
            countText.text = $"{countDownNum:N0}";
        }
        else if ((countDownNum == 0))
        {
            countText.text = countDownEndMsg;
        }


        yield return new WaitForSeconds(1f);
        countDownNum--;

        if (countDownNum >= 0)
        {
            StartCoroutine(CountDownCo());
        }
        else
        {
            countText.gameObject.SetActive(false);
        }

    }
}