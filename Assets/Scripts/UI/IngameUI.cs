using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameUI : MonoBehaviour
{
    public Slider speedSlider;
    public Slider boosterSlider;
    public Slider stageSlider;
    public Text score;
    public Text hit;
    public Text hitrate;
    public Slider hitSlider;

    private float hitcooltime;
    private float hittime = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance.ingameUI == null)
        {
            GameManager.Instance.ingameUI = this;
        }
        hitcooltime = GameManager.Instance.player.gameObject.GetComponent<PlayerHit>().coolTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.boosted == false)
        {
            speedSlider.value = (GameManager.Instance.cutrrentspeed - GameManager.Instance.minspeed) / (GameManager.Instance.maxspeed - GameManager.Instance.minspeed);
        }
        else if(GameManager.Instance.boosted == true)
        {
            speedSlider.value = 1;
        }

        hittime = GameManager.Instance.player.gameObject.GetComponent<PlayerHit>().currentcoolTime;
        boosterSlider.value = GameManager.Instance.boostgage / GameManager.Instance.maxboostgage;
        stageSlider.value = GameManager.Instance.playRate / GameManager.Instance.clearPlayRate;
        hitSlider.value = hittime / hitcooltime;
        score.text = Mathf.FloorToInt(GameManager.Instance.cutrrentscore).ToString();
        
        
        
    }

    public void LetsHit1(float hit)
    {
        StartCoroutine(LetsHit2(hit));
    }

    public IEnumerator LetsHit2(float Hit)
    {
        hit.gameObject.SetActive(true);
        hitrate.gameObject.SetActive(true);
        hitSlider.gameObject.SetActive(true);
        hitrate.text = "Score -" + Mathf.FloorToInt(Hit).ToString();

        yield return new WaitForSeconds(hitcooltime);

        hit.gameObject.SetActive(false);
        hitrate.gameObject.SetActive(false);
        hitSlider.gameObject.SetActive(false);
    }
}
