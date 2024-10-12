using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HawkSpawnerController : MonoBehaviour
{
    private GameObject camera;
    private Camera cameraCamera;
    [SerializeField]
    private GameObject hawkPrefab;
    private SpriteRenderer hawkPrefabSpriteRenderer;
    private float time = 0;
    private float waitingTime;
    private float waitingTimeMax = 4f;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Camera");
        cameraCamera = camera.GetComponent<Camera>();
        hawkPrefabSpriteRenderer = hawkPrefab.GetComponent<SpriteRenderer>();
        // waitingTime
        waitingTime = Random.Range(0f, waitingTimeMax);
    }

    // Update is called once per frame
    void Update()
    {
        // spawn hawk
        time += Time.deltaTime;
        if (time > waitingTime)
        {
            // reset time and waitingTime
            time = 0f;
            waitingTime = Random.Range(0f, waitingTimeMax);
            // scale
            float scale = 1f / (hawkPrefabSpriteRenderer.bounds.size.x / hawkPrefab.transform.localScale.x);
            // tranform.position
            float xPosition = Random.Range(-10f, 10f);
            float ySize = hawkPrefabSpriteRenderer.bounds.size.y / hawkPrefab.transform.localScale.y * scale;
            float yPosition = camera.transform.position.y + cameraCamera.orthographicSize + ySize * 8f;
            Vector3 position = new Vector3(xPosition, yPosition, 0f);
            // transform.rotation (because some rotation must be passed as argument to call Instantiate when position should also be passed)
            Quaternion rotation = new Quaternion(0f, 0f, 0f, 1f);
            // spawn
            GameObject hawk = Instantiate(hawkPrefab, position, rotation) as GameObject;
            // transform.localScale
            hawk.transform.localScale = new Vector3(scale, scale, 1f);
        }
    }
}