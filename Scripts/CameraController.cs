using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(AudioListener))]
[RequireComponent(typeof(Camera))]

public class CameraController : MonoBehaviour
{
    private bool alreadyInstantiatedHeavenCloudSpawner = false;
    private bool alreadyInstantiatedMoon = false;
    private GameObject blackHole;
    private SpriteRenderer blackHoleSpriteRenderer;
    private Camera camera;
    private float cameraWidth = 20f;
    [SerializeField]
    private GameObject heavenCloudSpawnerPrefab;
    private bool isGameFinished = false;
    [SerializeField]
    private GameObject moonPrefab;
    private GameObject player;
    private GameObject sky;
    private SpriteRenderer skySpriteRenderer;
    private bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        blackHole = GameObject.Find("BlackHole");
        blackHoleSpriteRenderer = blackHole.GetComponent<SpriteRenderer>();
        camera = GetComponent<Camera>();
        player = GameObject.Find("Player");
        sky = GameObject.Find("Sky");
        skySpriteRenderer = sky.GetComponent<SpriteRenderer>();
        // camera.orthohraphic
        camera.orthographic = true;
        // camera.orthographicCize
        Update();
        // gameObject.tag
        gameObject.tag = "MainCamera";
        // transform.position
        camera.transform.position = new Vector3(0f, camera.orthographicSize, -10f);
    }

    // Update is called once per frame
    void Update()
    {
        // everything takes place between y = -10 and y = 10, so the width of the camera is always 20 (in world units)
        // and the height is set in relation to the screen size
        float unitsPerPixel = cameraWidth / Screen.width;
        float desiredHalfHeight = Screen.height * unitsPerPixel * 0.5f;
        camera.orthographicSize = desiredHalfHeight;
        // transform.position (follow player vertically)
        if (!isGameFinished && !isGameOver)
        {
            float yPosition = player.transform.position.y - 1.5f + desiredHalfHeight;
            transform.position = new Vector3(transform.position.x, yPosition, -10f);
        }
        // instantiate moon at y = 100
        if (alreadyInstantiatedMoon == false && transform.position.y > 100f)
        {
            alreadyInstantiatedMoon = true;
            GameObject moon = Instantiate(moonPrefab, transform) as GameObject;
        }
        // instantiate heavenCloudSpawner when lower border of camera is above y = 720 (upper border of blackHole)
        if (alreadyInstantiatedHeavenCloudSpawner == false && transform.position.y - camera.orthographicSize > 720f)
        {
            alreadyInstantiatedHeavenCloudSpawner = true;
            GameObject heavenCloudSpawner = Instantiate(heavenCloudSpawnerPrefab) as GameObject;
        }
        // make blackHole and sky white after passing centre of blackHole
        if (transform.position.y > blackHole.transform.position.y && transform.position.y < blackHole.transform.position.y + 110f)
        {
            float ratio = (transform.position.y - blackHole.transform.position.y) / 100f;
            float rgb = Math.Min(1f, ratio);
            blackHoleSpriteRenderer.color = new UnityEngine.Color(rgb, rgb, rgb, 1f);
            skySpriteRenderer.color = new UnityEngine.Color(rgb, rgb, rgb, 1f);
        }
    }
    public void SetIsGameFinished(bool value)
    {
        isGameFinished = value;
    }

    public void SetIsGameOver(bool value)
    {
       isGameOver = value;
    }
}