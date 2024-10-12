using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CloudSpawnerController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> cloudPrefabList;

    // Start is called before the first frame update
    void Start()
    {
        // number of clouds
        int numberOfClouds = Random.Range(40, 800);
        // xSizeList (as an in ascending order sorted list, so that the smaller the cloud is, the lower its sortingOrder is and thus the further it is in the background)
        List<float> xSizeList = new List<float>();
        for (int i = 1; i <= numberOfClouds; i++)
        {
            xSizeList.Add(Random.Range(4f, 20f));
        }
        xSizeList.Sort();
        // spawn clouds
        for (int i = 0; i < numberOfClouds; i++)
        {
            // prefab
            int cloudNumber = Random.Range(0, cloudPrefabList.Count);
            GameObject cloudPrefab = cloudPrefabList[cloudNumber];
            // transform.position (the clouds should appear in level 1 & 2, so between y = 0 and y = 300)
            float xPosition = Random.Range(-10f, 10f);
            float yPosition = Random.Range(0f, 200f);
            Vector3 position = new Vector3(xPosition, yPosition, 0f);
            // transform.rotation (because some rotation must be passed as argument to call Instantiate when position should also be passed)
            Quaternion rotation = new Quaternion(0f, 0f, 0f, 1f);
            // spawn cloud
            GameObject cloud = Instantiate(cloudPrefab, position, rotation) as GameObject;
            // spriteRenderer.sortingOrder
            cloud.GetComponent<SpriteRenderer>().sortingOrder = i;
            // transform.localScale
            cloud.transform.localScale = new Vector3(1f, 1f, 1f);
            float scale = xSizeList[i] / cloud.GetComponent<SpriteRenderer>().bounds.size.x;
            cloud.transform.localScale = new Vector3(scale, scale, 1f);
            // xSize
            cloud.GetComponent<CloudController>().SetXSize(xSizeList[i]);
        }
        // destroy after job is done
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }
}