using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    Coroutine _pauseCorutine;

    float currentTimeSclale = 1;

    public void CheckPause()
    {
        if (_pauseCorutine == null)
            _pauseCorutine = StartCoroutine(IECheckPause());
    }

    public void StopCheckPause()
    {
        if(_pauseCorutine != null )
        StopCoroutine(_pauseCorutine);
        Time.timeScale = 1f;
        _pauseCorutine = null;
    }

    IEnumerator IECheckPause()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                ChangePause();

            yield return null;
        }
    }

    public void ChangePause() // Button return has access
    {
        if (Time.timeScale != 0.0f)
        {
            currentTimeSclale = Time.timeScale;
            Time.timeScale = 0.0f;
            MenuManager.instance.ChangePanelToPauseMenu();
            Cursor.visible = true;
            return;
        }

        Cursor.visible = true;
        Time.timeScale = currentTimeSclale;
        MenuManager.instance.ToPreviosPanel();
    }
}
