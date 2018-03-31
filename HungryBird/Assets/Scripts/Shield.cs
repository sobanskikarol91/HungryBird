using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shield : MonoBehaviour
{
    public GameObject shieldPrefab;
    protected string visualBarTag;

    VisualBar visualBarRtrans;
    GameObject shield;
    [HideInInspector]
    public bool isActived = false;// playerController

    protected virtual void Start()
    {
        shield = Instantiate(shieldPrefab, transform.position, Quaternion.identity);
        shield.transform.SetParent(transform);
        shield.SetActive(false);

        visualBarRtrans = GameObject.FindGameObjectWithTag(visualBarTag).GetComponent<VisualBar>();
    }

    public void ActiveShield(float time)
    {
        StopAllCoroutines();
        visualBarRtrans.ShowBar(time);
        shield.SetActive(true);
        StartCoroutine(IEActiveShield(time));
        isActived = true;
    }

    public IEnumerator IEActiveShield(float time)
    {
        while (true)
        {
            time -= Time.deltaTime;
            if (time < 0) break;

            yield return null;
        }

        ShieldDeactivation();
    }

    public void ShieldDeactivation() // player controller
    {
        isActived = false;
        shield.SetActive(false);
    }
}
