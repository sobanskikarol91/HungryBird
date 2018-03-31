using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnObject
{
    // some object like background is not interactive, when game will over all interactible willl destroied
    public bool isInteractive = true;
    // variable needed for test in insector
    public bool spawnThisObject = true;
    public bool randomSpawn = false;
    public bool changeSpeed = false;
    public float minTimeToSpawn = 1;
    public float maxTimeToSpawn = 3;
    public float speed = 1;
    public float scale = 1;
    public Transform spawnPoint;
    public GameObject[] prefabs;
    [HideInInspector]
    public List<GameObject> createdObjects = new List<GameObject>();
    [Tooltip("how many times prefabs can be repeat")]
    public int repeatingObject = 1;

    List<int> indexReadyObjects = new List<int>();
    List<int> indexSpawnedObjects = new List<int>();
    List<InteractiveObject> _interactiveObjects = new List<InteractiveObject>();

    // To generate begining bg we'll offset position
    public void ActiveRandomObject(Vector3 onPosition)
    {
        // if there is no objects to spawn return;
        if (indexReadyObjects.Count == 0)
            return;

        int randomNr = RandomObjectIndex();
        int createdGONr = indexReadyObjects[randomNr];
        EnableObject(createdGONr, true);
        SetPosition(createdGONr, onPosition);
        SetObjectIsUsed(randomNr);
    }

    // To generate all objects when game in on
    public void ActiveRandomObject()
    {
        // if there is no ready objects to spawn
        if (indexReadyObjects.Count == 0)
        {
            Debug.Log("No ready objects to spawn: " + prefabs[0].name);
            return;
        }

        int randomNr = RandomObjectIndex();
        int createdGONr = indexReadyObjects[randomNr];
        EnableObject(createdGONr, true);

        if (randomSpawn)
            RandomPosition(createdGONr);
        else
            SetPosition(createdGONr);

        // set that this createdObject is now used
        SetObjectIsUsed(randomNr);
    }

    //script attatched to spawned gameobject has access
    public void ObjectIsReadyToUse(int nr)
    {
        SetObjectIsUnused(nr);
        EnableObject(nr, false);
    }

    int RandomObjectIndex()
    {
        // random object from unUsedObjects
        int length = indexReadyObjects.Count;
        int randomIndex = Random.Range(0, length);
        return randomIndex;
    }

    void EnableObject(int nr, bool state)
    {
        createdObjects[nr].SetActive(state);
    }

    void RandomPosition(int nr)
    {
        Vector3 up = SpawnerManager.instance.upMapPosition;
        Vector3 down = SpawnerManager.instance.downMapPosition;

        Vector2 randomPosition = new Vector2(up.x, Random.Range(down.y, up.y));
        createdObjects[nr].transform.position = randomPosition;
    }

    void SetPosition(int nr)
    {
        createdObjects[nr].transform.position = spawnPoint.position;
    }

    void SetPosition(int nr, Vector3 newPosition)
    {
        createdObjects[nr].transform.position = newPosition;
    }

    void SetObjectIsUsed(int nr)
    {
        indexReadyObjects.RemoveAt(nr);
        indexSpawnedObjects.Add(nr);
    }

    void SetObjectIsUnused(int nr)
    {
        indexReadyObjects.Add(nr);
        indexSpawnedObjects.Remove(nr);
    }

    #region set prefab parametrs
    public void InitialInstantiateObject()
    {
        ChangeScaleAllObjects();
        GiveSOIndividualNr();
        FillindexReadyObjects();
        ChangeSpeed();
        DisableAllCreatedObjects();
        GetAllInteractiveObjects();
    }

    void ChangeScaleAllObjects()
    {
        foreach (GameObject go in createdObjects)
            go.transform.localScale *= scale;
    }

    void GiveSOIndividualNr()
    {
        SpawnManagerInformator om;
        for (int i = 0; i < createdObjects.Count; i++)
        {
            om = createdObjects[i].GetComponent<SpawnManagerInformator>();
            if (om == null)
                Debug.Log(createdObjects[i].name);
            om.individualNr = i;
            om._soReference = this;
        }
    }

    void FillindexReadyObjects()
    {
        for (int i = 0; i < prefabs.Length * repeatingObject; i++)
            indexReadyObjects.Add(i);
    }

    void DisableAllCreatedObjects()
    {
        foreach (GameObject gm in createdObjects)
            gm.SetActive(false);
    }

    public void DestroyAllObjects()
    {
        // don't destroy if object is background
        if (!isInteractive) return;


        foreach (InteractiveObject ia in _interactiveObjects)
        {
            // if object is active on scene
            if (ia.isAlive == true)
                ia.InstantDestroy();
        }
    }

    void GetAllInteractiveObjects()
    {
        if (!isInteractive) return;

        for (int i = 0; i < createdObjects.Count; i++)
        {
            // get script form all object
            InteractiveObject io = createdObjects[i].GetComponent<InteractiveObject>();
            // Add interactiveObject to script
            if (io == null)
            {
                //Debug.Log("There is no InteractiveObject attached to: " +createdObjects[i].name);
            }
            else
                _interactiveObjects.Add(io);
        }
    }

    #endregion

    void ChangeSpeed()
    {
        if (!changeSpeed) return;

        for (int i = 0; i < createdObjects.Count; i++)
        {
            ObjectMover om = createdObjects[i].GetComponent<ObjectMover>();

            if (om == null) return;
            om.speed = speed;
        }
    }

    public void ChangeSpeed(float newSpeed)
    {
        for (int i = 0; i < indexSpawnedObjects.Count; i++)
        {
            int index = indexSpawnedObjects[i];
            ObjectMover om = createdObjects[index].GetComponent<ObjectMover>();

            if (om != null)
                om.gameSpeed = newSpeed;
        }
    }
}
