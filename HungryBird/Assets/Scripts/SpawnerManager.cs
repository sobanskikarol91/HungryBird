using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public static SpawnerManager instance;

    [Range(0,2)]
    public float speedSpawn = 0.65f;
    [Header("Spawn begining objects on BG")]
    public float minDistanceSpawn = 3;
    public float maxDistanceSpawn = 5;
    public Vector3 upMapPosition;
    public Vector3 downMapPosition;

    [HideInInspector]
    public float endMapX = -16f;

    [Header("Spawn Objects")]
    [Range(0,100)]
    public int rewardSpawnChance=30;
    public SpawnObject playerSO;
    public bool isEnemySpawn = true;
    public bool isBGSpawn = true;
    public bool isPlayerSpawn = true;
    public bool isPickUpsSpawn = true;

    [Space]
    [Header("Background")]
    public SpawnObject backTrees;
    public SpawnObject trees;
    public SpawnObject rocks;
    public SpawnObject flowers;
    public SpawnObject bushes;
    public SpawnObject signs; 

    [Space]
    [Header("Enemies")]
    public SpawnObject birds;
    public SpawnObject groundObstacle;
    public SpawnObject spikes;

    [Space]
    [Header("PickUps")]
    public SpawnObject consunable;
    public SpawnObject coin;


    // add all prefabs to one List of type
    List<SpawnObject> bgToSpawn = new List<SpawnObject>();
    List<SpawnObject> enemiesToSpawn = new List<SpawnObject>();
    List<SpawnObject> pickUpsToSpawn = new List<SpawnObject>();

    // keep all object in Hierarchy
    Transform spawnHolder;
    List<Coroutine> _pickUpsCorutine = new List<Coroutine>();
    List<Coroutine> _enemiesCorutine = new List<Coroutine>();
    bool playerIsCreated = false;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);

        // to holding created objects
        spawnHolder = new GameObject("SpawnHolder").transform;
    }

    private void Start()
    {
        PreperAllObjectsToSpawn();

        if (isBGSpawn) SpawnBGObjects();

        //StartCoroutine(IECoinSpawner());
    }

    #region Spawn     // GameManager has access

    public void SpawnInteractibleObjects()
    {
        if (isPlayerSpawn && !playerIsCreated) CreatePlayer();
        if (isPlayerSpawn) SpawnPlayer();
        if (isEnemySpawn) SpawnEnemies();
        if (isPickUpsSpawn) SpawnPickUps();
    }

    void SpawnPlayer()
    {
        playerSO.ActiveRandomObject();
    }

    void SpawnEnemies()
    {
        StartSpawnObjects(enemiesToSpawn, _enemiesCorutine);
    }

    void SpawnPickUps()
    {
        StartSpawnObjects(pickUpsToSpawn, _pickUpsCorutine);
    }

    void SpawnBGObjects()
    {
        CreateAllBeginingBG();
        for (int i = 0; i < bgToSpawn.Count; i++)
            StartCoroutine(ActiveAndPositionObject(bgToSpawn[i]));
    }
    
    void StartSpawnObjects(List<SpawnObject> _spawnObjects, List<Coroutine> _corutines)
    {
        for (int i = 0; i < _spawnObjects.Count; i++)
        {
            // if is mark to spawn this object;
            if (_spawnObjects[i].spawnThisObject)
            {
                Coroutine c = StartCoroutine(ActiveAndPositionObject(_spawnObjects[i]));
                _corutines.Add(c);
            }
        }
    }

    void StopSpawnObjects(List<Coroutine> _corutine)
    {
        for (int i = 0; i < _corutine.Count; i++)
            StopCoroutine(_corutine[i]);
    }
    
    IEnumerator ActiveAndPositionObject(SpawnObject so)
    {
        while (true)
        {
            so.ActiveRandomObject();
            yield return new WaitForSeconds(Random.Range(so.minTimeToSpawn, so.maxTimeToSpawn));
        }
    }
    #endregion

    #region Prepear all Objects To Spown

    void PreperAllObjectsToSpawn()
    {
        if (isBGSpawn) AddBGToList();
        if (isEnemySpawn) AddEnemyToList();
        if (isPickUpsSpawn) AddPickUpsToList();

        if (isBGSpawn) PreperObjectsToSpawn(bgToSpawn);
        if (isEnemySpawn) PreperObjectsToSpawn(enemiesToSpawn);
        if (isPickUpsSpawn) PreperObjectsToSpawn(pickUpsToSpawn);
    }

    void CreatePlayer()
    {
        playerIsCreated = true;
        GameObject newPlayer = Instantiate(playerSO.prefabs[0], transform.position, Quaternion.identity);
        playerSO.createdObjects.Add(newPlayer);
        playerSO.InitialInstantiateObject();
    }

    void PreperObjectsToSpawn(List<SpawnObject> _objectToSpawn)
    {
        for (int i = 0; i < _objectToSpawn.Count; i++)
            CreateAndSetObject(_objectToSpawn[i]);
    }

    void CreateAndSetObject(SpawnObject so)
    {
        // go through all prefabs
        for (int j = 0; j < so.repeatingObject; j++)
        {
            // many times
            for (int i = 0; i < so.prefabs.Length; i++)
            {
                // create instance 
                GameObject createdObject = Instantiate(so.prefabs[i], so.spawnPoint.position, Quaternion.identity);
                so.createdObjects.Add(createdObject);
                createdObject.transform.SetParent(spawnHolder);
            }
        }
        // Change scale all created objects
        // give created object individual number and reference to script

        so.InitialInstantiateObject();
    }

    #endregion

    #region Prepear Map With BG
    void CreateAllBeginingBG()
    {
        for (int i = 0; i < bgToSpawn.Count; i++)
            StartCoroutine(CreateBeginingBG(bgToSpawn[i]));
    }

    void AddBGToList()
    {
        bgToSpawn.Add(trees);
        bgToSpawn.Add(rocks);
        bgToSpawn.Add(flowers);
        bgToSpawn.Add(bushes);
        bgToSpawn.Add(backTrees);
        bgToSpawn.Add(signs);
    }

    void AddEnemyToList()
    {
        if (birds.prefabs != null && birds.spawnThisObject) enemiesToSpawn.Add(birds);
        if (spikes.prefabs != null && spikes.spawnThisObject) enemiesToSpawn.Add(spikes);
        if (groundObstacle.prefabs != null && groundObstacle.spawnThisObject) enemiesToSpawn.Add(groundObstacle);
    }

    void AddPickUpsToList()
    {
        pickUpsToSpawn.Add(coin);
        pickUpsToSpawn.Add(consunable);
    }

    IEnumerator CreateBeginingBG(SpawnObject so)
    {
        float endMapX = 8f;
        Vector3 currentSpawnPoint = new Vector3(-8f, so.spawnPoint.position.y, so.spawnPoint.position.z);

        while (currentSpawnPoint.x < endMapX)
        {
            // Random position 
            float offSet = Random.Range(minDistanceSpawn, maxDistanceSpawn);
            currentSpawnPoint += new Vector3(offSet, 0, 0);
           // enable and set Object on new position
            so.ActiveRandomObject(currentSpawnPoint);
            
            yield return null;
        }
    }

    #endregion

    public void Restart()
    {
        InteruptGame();
        Invoke("SpawnInteractibleObjects", 1f);
    }

    public void InteruptGame() // GameMaster has access
    {
        StopSpawnObjects(_enemiesCorutine);
        StopSpawnObjects(_pickUpsCorutine);

        DestroyAllObjects(enemiesToSpawn);
        DestroyAllObjects(pickUpsToSpawn);

        playerSO.DestroyAllObjects();
    }


    void DestroyAllObjects(List<SpawnObject> soList)
    {
        foreach (SpawnObject so in soList)
            so.DestroyAllObjects();
    }  


    void ChangeSpeedAllSpawnedObjects(float newSpeed)
    {
        foreach (SpawnObject so in bgToSpawn) so.ChangeSpeed(newSpeed);
        foreach (SpawnObject so in enemiesToSpawn) so.ChangeSpeed(newSpeed);
        foreach (SpawnObject so in pickUpsToSpawn) so.ChangeSpeed(newSpeed);
    }

    public GameObject coins;

    [Range(0,100)]
    public float hight = 10;
    [Range(0, 100)]
    public float width = 10;

    [Range(0, 100)]
    public float coinTimeSpawn = 1f;


    IEnumerator IECoinSpawner()
    {
        float y=0,t=0;

        while (y>=0)
        {
            Instantiate(coins, new Vector2(12, y), Quaternion.identity);
            t += Time.deltaTime * width;
            y = Mathf.Sin(t / 2) * hight;
            yield return new WaitForSeconds(coinTimeSpawn);
        }
    }
}

