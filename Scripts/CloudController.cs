using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]

public class CloudController : MonoBehaviour
{
    private GameObject camera;
    private Camera cameraCamera;
    private float lastCameraYPosition;
    private SpriteRenderer spriteRenderer;
    // xSize must be set by cloudSpawnerController.Start through SetXSize after instantiating
    private float xSize = 1f;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Camera");
        cameraCamera = camera.GetComponent<Camera>();
        lastCameraYPosition = camera.transform.position.y;
        spriteRenderer = GetComponent<SpriteRenderer>();
        // spriteRenderer.sortingLayer
        spriteRenderer.sortingLayerName = "Clouds";
        // spriteRenderer.sortingOrder, transform.localScale, transform.position and xSize are handled by cloudSpawnerController.Start
    }

    // Update is called once per frame
    void Update()
    {
        // destroy when out of screen
        if (transform.position.y < camera.transform.position.y - cameraCamera.orthographicSize * 2f - spriteRenderer.bounds.size.y * 2f)
        {
            Destroy(gameObject);
        }
        // spriteRenderer.color (sunset: the higher the cloud, the darker the sky and thus the less visible the cloud
        // -> between y = 0 and y = 200 the clouds become transparent)
        float ratio = transform.position.y / 200f;
        float a = Math.Max(0f, 1f - ratio);
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, a);
        // transform.position (dependent on xSize: the smaller the cloud, the further away and thus the slower it moves out of screen;
        // the minimum xSize of a cloud is 4, therefore division by 3 makes the smallest clouds move about 1.33 times slower than the camera)
        float deltaCameraYPosition = camera.transform.position.y - lastCameraYPosition;
        float yPosition = transform.position.y + deltaCameraYPosition / (xSize / 3f);
        transform.position = new Vector3(transform.position.x, yPosition, 0f);
        // lastCameraYPosition
        lastCameraYPosition = camera.transform.position.y;
    }

    public void SetXSize(float value)
    {
        xSize = value;
    }
}