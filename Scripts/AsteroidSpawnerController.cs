using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawnerController : MonoBehaviour
{
    [SerializeField]
    private GameObject asteroidPrefab;
    private GameObject camera;
    private Camera cameraCamera;
    private int currentsortingOrder = 0;
    private GameObject player;
    private PlayerController playerController;
    private float time = 0;
    private float waitingTime;
    private float waitingTimeMax = 4f;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Camera");
        cameraCamera = camera.GetComponent<Camera>();
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        // waitingTime
        waitingTime = Random.Range(0f, waitingTimeMax);
    }

    // Update is called once per frame
    void Update()
    {
        // spawn asteroid
        time += Time.deltaTime;
        if (time > waitingTime)
        {
            // reset time and waitingTime
            time = 0f;
            waitingTime = Random.Range(0f, waitingTimeMax);
            // ySize
            float ySize = Random.Range(4f, 8f);
            // tranform.position
            float xPosition = Random.Range(-10f, 10f);
            float yPosition = camera.transform.position.y + cameraCamera.orthographicSize + ySize * 0.5f;
            Vector3 position = new Vector3(xPosition, yPosition, 0f);
            // transform.rotation (because some rotation must be passed as argument to call Instantiate when position should also be passed)
            Quaternion rotation = new Quaternion(0f, 0f, 0f, 1f);
            // spawn
            GameObject asteroid = Instantiate(asteroidPrefab, position, rotation, camera.transform) as GameObject;
            // spriteRenderer.sortingOrder
            asteroid.GetComponent<SpriteRenderer>().sortingOrder = currentsortingOrder;
            currentsortingOrder++;
            // transform.localScale
            float scale = ySize / asteroid.GetComponent<SpriteRenderer>().bounds.size.y;
            asteroid.transform.localScale = new Vector3(scale, scale, 1f);
        }
    }
}