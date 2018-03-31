using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerInformator : MonoBehaviour
{
    // to know what index object has, when we spawn it from GameObject array
    [HideInInspector]
    public int individualNr;

    // to know which SpawnObject created this gameobject
    [HideInInspector]
    public SpawnObject _soReference;
    public bool destroyIt = false;
    float mapBoundryX;


    void Awake()
    {
        mapBoundryX = SpawnerManager.instance.endMapX;
    }

    // that object is not needful death/or out of the map en.
    public void InformSpawnManager()
    {
        if (destroyIt) Destroy(gameObject);
        else _soReference.ObjectIsReadyToUse(individualNr);
    }

    IEnumerator CheckIfObjectExitMapBoundy()
    {
        while (transform.position.x > mapBoundryX)
            yield return null;

        // if Object exit map
         InformSpawnManager();
    }

    private void OnEnable()
    {
        StartCoroutine(CheckIfObjectExitMapBoundy());
    }
}
