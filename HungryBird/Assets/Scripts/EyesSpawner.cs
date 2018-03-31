using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;

    [Range(0, 100)]
    public int spawnChance = 20;

    [Range(0, 1)]
     float minScale = 1.4f;

    [Range(0, 1)]
     float maxScale = 1.7f;



    private void Start()
    {
        if (objectsToSpawn == null)
            return;

        if (Random.Range(0, 100) > spawnChance)
            return;

        int nr = objectsToSpawn.Length == 1 ? 0 : Random.Range(0, objectsToSpawn.Length);
        GameObject go = Instantiate(objectsToSpawn[nr], transform.position, Quaternion.identity);
        go.transform.SetParent(gameObject.transform);

        float randomScale = Random.Range(minScale, maxScale);
        go.transform.localScale *= randomScale;
    }
}
