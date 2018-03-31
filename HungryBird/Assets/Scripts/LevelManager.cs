using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Text lvlText;
    Animator lvlTextAnimator;
    int currentLvl=0;
    public int CurrentLvl { get {return currentLvl; }  set { currentLvl = value; lvlText.text = "Level " + currentLvl.ToString(); } }
    float timeBetweenlvls=10f;
    float timeToHideLvlTxt=2.5f;
    float timeToShowFirstLvl=0.5f;



    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        lvlTextAnimator = lvlText.GetComponent<Animator>();
    }

    public void StartLevel()
    {
        InterruptChangingLvls();
        StartCoroutine(ChangeLvl());
    }

    IEnumerator ChangeLvl()
    {
        CurrentLvl = 0;
        yield return new WaitForSeconds(timeToShowFirstLvl);

        while(true)
        {
            CurrentLvl++;
            ShowLvlLabel(true);
            yield return new WaitForSeconds(timeToHideLvlTxt);
            ShowLvlLabel(false);
            yield return new WaitForSeconds(timeBetweenlvls);
            Time.timeScale += 0.1f;
        }
    }


    void ShowLvlLabel(bool state)
    {
        lvlTextAnimator.SetBool("showLvlLable", state);
    }

     public void InterruptChangingLvls()
    {
        StopAllCoroutines();
    }
}