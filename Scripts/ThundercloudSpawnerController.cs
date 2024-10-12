using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ThundercloudSpawnerController : MonoBehaviour
{
    private GameObject camera;
    private Camera cameraCamera;
    float lastCameraYPosition;
    private int currentsortingOrder = 0;
    [SerializeField]
    private List<GameObject> thundercloudPrefabList;
    private float movedDistance = 0;
    private float waitingDistance;
    private float waitingDistanceMax = 40f;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Camera");
        cameraCamera = camera.GetComponent<Camera>();
        lastCameraYPosition = camera.transform.position.y;
        // waitingTime
        waitingDistance = Random.Range(0f, waitingDistanceMax);
    }

    // Update is called once per frame
    void Update()
    {
        // spawn thundercloud
        movedDistance += camera.transform.position.y - lastCameraYPosition;
        lastCameraYPosition = camera.transform.position.y;
        if (movedDistance > waitingDistance)
        {
            // reset movedDistance and waitingDistance
            movedDistance = 0f;
            waitingDistance = Random.Range(0f, waitingDistanceMax);
            // prefab
            int i = Random.Range(0, thundercloudPrefabList.Count);
            GameObject thundercloudPrefab = thundercloudPrefabList[i];
            SpriteRenderer thundercloudPrefabSpriteRenderer = thundercloudPrefab.GetComponent<SpriteRenderer>();
            // scale
            float scale = 8f / (thundercloudPrefabSpriteRenderer.bounds.size.x / thundercloudPrefab.transform.localScale.x);
            // tranform.position
            float xPosition = Random.Range(-10f, 10f);
            float ySize = thundercloudPrefabSpriteRenderer.bounds.size.y / thundercloudPrefab.transform.localScale.y * scale;
            float yPosition = camera.transform.position.y + cameraCamera.orthographicSize + ySize * 2f;
            Vector3 position = new Vector3(xPosition, yPosition, 0f);
            // transform.rotation (because some rotation must be passed as argument to call Instantiate when position should also be passed)
            Quaternion rotation = new Quaternion(0f, 0f, 0f, 1f);
            // spawn
            GameObject thundercloud  = Instantiate(thundercloudPrefabList[i], position, rotation) as GameObject;
            // spriteRenderer.sortingOrder
            thundercloud.GetComponent<SpriteRenderer>().sortingOrder = currentsortingOrder;
            currentsortingOrder++;
            // transform.localScale
            thundercloud.transform.localScale = new Vector3(scale, scale, 1f);
        }
    }
}