using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavenCloudSpawnerController : MonoBehaviour
{
    private GameObject camera;
    private Camera cameraCamera;
    [SerializeField]
    private List<GameObject> heavenCloudPrefabList;
    float maxRandomCloudSize = 16f;
    int numberOfHeavenClouds = 400;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Camera");
        cameraCamera = camera.GetComponent<Camera>();
        // xSizeList (as an in ascending order sorted list, so that the smaller the heavenCloud is, the lower its sortingOrder is and thus the further it is in the background)
        List<float> xSizeList = new List<float>();
        for (int i = 0; i < numberOfHeavenClouds; i++)
        {
            xSizeList.Add(getRandomHeavenCloudXSize());
        }
        xSizeList.Sort();
        for (int i = 0; i < numberOfHeavenClouds; i++)
        {
            // transform.position
            float xPosition = Random.Range(-10f, 10f);
            float yPositionMax = camera.transform.position.y + ((xSizeList[i]) * 2 + 1) * cameraCamera.orthographicSize;        // xSizeList[i] because the deltaYPositionRatio of a heavenCloud is its xSize
            float yPositionMin = camera.transform.position.y - cameraCamera.orthographicSize;
            float yPosition = Random.Range(yPositionMin, yPositionMax);
            Vector3 position = new Vector3(xPosition, yPosition, 0f);
            // transform.rotation (because some rotation must be passed as argument to call Instantiate when position should also be passed)
            Quaternion rotation = new Quaternion(0f, 0f, 0f, 1f);
            // spawn
            int j = Random.Range(0, heavenCloudPrefabList.Count);
            GameObject heavenCloud = Instantiate(heavenCloudPrefabList[j], position, rotation, camera.transform) as GameObject;
            // spriteRenderer.soringOrder
            heavenCloud.GetComponent<SpriteRenderer>().sortingOrder = i;
            // transform.localScale
            heavenCloud.transform.localScale = new Vector3(1f, 1f, 1f);
            float scale = xSizeList[i] / heavenCloud.GetComponent<SpriteRenderer>().bounds.size.x;
            heavenCloud.transform.localScale = new Vector3(scale, scale, 1f);
            // xSize
            heavenCloud.GetComponent<HeavenCloudController>().setXSize(xSizeList[i]);
        }
        // destroy when job is done
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }

    // favor smaller sizes (more clouds in the background than in the foreground):
    float getRandomHeavenCloudXSize()
    {
        float randomXSize = Random.Range(0f, maxRandomCloudSize);
        // randomSize (examples):      probabilty that randomSize gets returned:
        // 0                           1
        // 1                           1/11
        // 2                           1/21
        // 15                          1/151
        // 16                          1/161
        if (randomXSize < Random.Range(0f, randomXSize + 0.1f))
        {
            return randomXSize;
        }
        else
        {
            return getRandomHeavenCloudXSize();
        }
    }
}