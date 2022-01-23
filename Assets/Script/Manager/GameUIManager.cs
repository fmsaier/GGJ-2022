using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    #region Singleton pattern

    /*
    ** Singleton pattern
    */

    private static GameUIManager _instance;
    public static GameUIManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    public TMP_Text timerTextVictory;
    public TMP_Text timerTextDefeat;
    public GameObject VictoryPanel;
    public GameObject DefeatPanel;
    public Animator VictoryAnimator;
    public Animator DefeatAnimator;
    public GameObject Darkness;
    public float DarknessMaxBaseScale;
    public float DarknessMinBaseScale;

    public void UpdateTimer(float time)
    {
        var timeSpan = TimeSpan.FromSeconds(time);
        
        timerTextVictory.text = timeSpan.ToString("m\\:ss");
        timerTextDefeat.text = timeSpan.ToString("m\\:ss");
    }

    public void UpdateDarkness(float currentHp, float maxHp)
    {
        float lifeRatio = currentHp / maxHp;
        if (lifeRatio == 0)
        {
            Darkness.transform.localScale = new Vector3(DarknessMinBaseScale, DarknessMinBaseScale, DarknessMinBaseScale);
        }
        else
        {
            StartCoroutine(SmothRescale(Darkness.transform.localScale, new Vector3(DarknessMaxBaseScale * lifeRatio, DarknessMaxBaseScale * lifeRatio, DarknessMaxBaseScale * lifeRatio)));
        }
    }

    public void ShowVictory()
    {
        GameManager.Instance.PauseGame();
        VictoryPanel.SetActive(true);
        VictoryAnimator.Play("FadeIn");
    }

    public void ShowDefeat()
    {
        GameManager.Instance.PauseGame();
        DefeatPanel.SetActive(true);
        DefeatAnimator.Play("FadeIn");
    }

    public void HideVictoryPanel()
    {
        VictoryAnimator.Play("FadeOut");
        StartCoroutine(HideCoroutine());
    }

    IEnumerator HideCoroutine()
    {
        yield return new WaitForSeconds(1);
        VictoryPanel.SetActive(false);
        GameManager.Instance.ResumeGame();
    }

    IEnumerator SmothRescale(Vector3 a, Vector3 b)
    {
        float i = 0;

        while (i <= 1)
        {
            i += Time.deltaTime;
            Darkness.transform.localScale = Vector3.Lerp(a, b, i);
            yield return new WaitForEndOfFrame();
        }
    }
}
