using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxySpawnerController : MonoBehaviour
{
    private GameObject camera;
    private Camera cameraCamera;
    [SerializeField]
    private List<GameObject> galaxyPrefabList;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Camera");
        cameraCamera = camera.GetComponent<Camera>();
        for (int i = 0; i < 16; i++)
        {
            // transform.position
            // multiplication with 3 because the maximum deltaYPositionRatio of a galaxy is 1
            float xPosition = Random.Range(-10f, 10f);
            float yPositionMax = camera.transform.position.y + 3f * cameraCamera.orthographicSize;
            float yPositionMin = camera.transform.position.y - cameraCamera.orthographicSize;
            float yPosition = Random.Range(yPositionMin, yPositionMax);
            Vector3 position = new Vector3(xPosition, yPosition, 0f);
            // transform.rotation (because some rotation must be passed as argument to call Instantiate when position should also be passed)
            Quaternion rotation = new Quaternion(0f, 0f, 0f, 1f);
            // spawn
            int j = Random.Range(0, galaxyPrefabList.Count);
            GameObject galaxy = Instantiate(galaxyPrefabList[j], position, rotation, camera.transform) as GameObject;
            // spriteRenderer.soringOrder
            galaxy.GetComponent<SpriteRenderer>().sortingOrder = i;
        }
        // destroy when job is done
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }
}