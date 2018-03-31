using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ObjectMover : MonoBehaviour
{
    [HideInInspector]
    public float gameSpeed; // SpawnManager can change it
    public float speed = 1f; // SpawnOBject can change it
    public float downSpeed= 1f;
    float timeToDestroy = 13f;

    Transform transform;
    Vector2 direction = Vector3.left;
    public bool moveDown = false;


    private void Awake()
    {
        transform = GetComponent<Transform>();
    }

    private void Start()
    {
        if (moveDown)
            direction = Vector2.down;

        gameSpeed = SpawnerManager.instance.speedSpawn;
    }

    private void Update()
    {
        transform.Translate(direction * Time.deltaTime * gameSpeed * speed);
    }

    public void MoveDown()
    {
        StartCoroutine(IEMoveDown());
    }

    IEnumerator IEMoveDown()
    {
        while (true)
        {
            transform.Translate(Physics2D.gravity * Time.deltaTime * downSpeed);
            yield return null;
        }
    }

    private void OnEnable()
    {
        gameSpeed = SpawnerManager.instance.speedSpawn;
    }
}