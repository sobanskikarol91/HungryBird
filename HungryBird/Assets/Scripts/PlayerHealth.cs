
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Color maxColor = new Color(0 / 255f, 255 / 255f, 0 / 255f);
    public Color minColor = new Color(255 / 255f, 0 / 255f, 0 / 255f);

    Transform healthBarTransform;
    Image playerHealthBar;
    float maxValue = 100;
    bool isAlive = true;
    public float hungerFactor;
    HungerController _hungerCon;

    float warningValue = 50f;
    float warningValue2 = 10f;
    float currentValue;
    float CurrentValue
    {
        get { return currentValue; }
        set
        {
            currentValue = value;
            float percent = currentValue / (float)maxValue;
            if (percent < 0)                                                                  // it's prevent scale bar in left side
            {
                percent = 0;
                isAlive = false;


                GetComponent<PlayerController>().DieOfHunger();
            }

            //ScaleHealthBar
            healthBarTransform.localScale = new Vector3(percent, healthBarTransform.localScale.y, healthBarTransform.localScale.z);
            playerHealthBar.color = Color.Lerp(minColor, maxColor, percent);
        }
    }

    private void Awake()
    {
        playerHealthBar = GameObject.FindGameObjectWithTag("PlayerHealthBar").GetComponent<Image>();
        healthBarTransform = playerHealthBar.transform;
        _hungerCon = healthBarTransform.parent.GetComponent<HungerController>();
    }

    IEnumerator UpdateBar()
    {
        CurrentValue = maxValue;
        bool isPlayerWarning = false;
        bool isPlayerWarning2 = false;
        while (isAlive)
        {
            CurrentValue -= Time.deltaTime * hungerFactor;

            if (currentValue > warningValue)
                isPlayerWarning = false;
            if (currentValue > warningValue2)
                isPlayerWarning2 = false;

            if (CurrentValue < warningValue && !isPlayerWarning)
            {
                isPlayerWarning = true;
                _hungerCon.Warning();
            }
            else if (CurrentValue < warningValue2 && !isPlayerWarning2)
            {
                isPlayerWarning = true;
                _hungerCon.Warning();
            }
            yield return null;
        }
    }

    public void AddHealth(float energy)
    {
        if (currentValue + energy > maxValue)
            CurrentValue = 100;
        else CurrentValue += energy;
    }

    void OnEnable()
    {
        CurrentValue = 100;
        isAlive = true;
        StartCoroutine(UpdateBar());
    }
}   // Karol Sobanski





