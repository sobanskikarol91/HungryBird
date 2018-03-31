using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VisualBar : MonoBehaviour
{
    public Transform barTransform;
    public Vector2 offset;
    float maxValue;
    Transform playerTransform;
    Transform[] children;

    float currentValue;
    float CurrentValue
    {
        get { return currentValue; }
        set
        {
            currentValue = value;
            float percent = currentValue / (float)maxValue;

            if (percent < 0)                                                                  // it's prevent scale bar in left side
                HideBar();

            //ScaleHealthBar
            barTransform.localScale = new Vector3(percent, barTransform.localScale.y, barTransform.localScale.z);
        }
    }

    private void Start()
    {
        children = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            children[i] = transform.GetChild(i).GetComponent<Transform>();

        HideBar();
    }

    IEnumerator UpdateBar()
    {
        playerTransform = GameManager.instance.Player.transform;

        while (true)
        {
            transform.position = playerTransform.position + (Vector3)offset;
            CurrentValue -= Time.deltaTime;
            yield return null;
        }
    }

    public void ShowBar(float value)
    {
        StopAllCoroutines();
        CurrentValue = maxValue = value;

        foreach (Transform trans in children)
            trans.gameObject.SetActive(true);

        StartCoroutine(UpdateBar());
    }

    void HideBar()
    {
        StopAllCoroutines();

        foreach (Transform trans in children)
            trans.gameObject.SetActive(false);
    }
}
