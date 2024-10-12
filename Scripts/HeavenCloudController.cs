using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]

// heavenCloud is a child of camera
public class HeavenCloudController : MonoBehaviour
{
    private Camera cameraCamera;
    private float deltaCameraYPosition;
    // yPositionRatio is between 0 and 1, where 0 = lower border of the screen and 1 = upper border of the screen
    private float deltaYPositionRatio;
    private float initialCameraYPosition;
    private float initialYPositionRatio;
    private SpriteRenderer spriteRenderer;
    // xSize must be set by heavenCloudSpawnerController after instantiating through SetXSize
    private float xSize = 1f;

    // Start is called before the first frame update
    void Start()
    {
        cameraCamera = transform.parent.GetComponent<Camera>();
        initialCameraYPosition = transform.parent.transform.position.y;
        deltaCameraYPosition = 1100f - initialCameraYPosition;
        initialYPositionRatio = (transform.localPosition.y / transform.parent.transform.localScale.y + cameraCamera.orthographicSize) / (cameraCamera.orthographicSize * 2f);
        spriteRenderer = GetComponent<SpriteRenderer>();
        // spriteRenderer.color
        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        // spriteRenderer.sortingLayer
        spriteRenderer.sortingLayerName = "Clouds";
        // transform.localScale, transform.position and xSize are handled by galaxySpawnerController
    }

    // Update is called once per frame
    void Update()
    {
        // destroy when out of screen
        if (transform.position.y < transform.parent.transform.position.y - cameraCamera.orthographicSize - spriteRenderer.bounds.size.y)
        {
            Destroy(gameObject);
        }
        // transform.localPosition
        // deltaYPositionRatio = 0: heavenCloud does not move at all (in relation to the camera)
        // deltaYPositionRatio = 1: in the time the camera moves from y = 700 to y = 1100, the heavenCloud moves one screen height
        deltaYPositionRatio = xSize;
        float movedDistance = transform.parent.transform.position.y - initialCameraYPosition;
        float ratio = movedDistance / deltaCameraYPosition;
        float localYPositionRatio = initialYPositionRatio - deltaYPositionRatio * ratio;
        float localYPosition = (localYPositionRatio * cameraCamera.orthographicSize * 2f - cameraCamera.orthographicSize) / transform.parent.transform.localScale.y;
        float localZPosition = 10f / transform.parent.transform.localScale.y;
        transform.localPosition = new Vector3(transform.localPosition.x, localYPosition, localZPosition);
        // become less transparent between initialCameraYPosition and y = 800
        if (transform.parent.transform.position.y < 810f)
        {
            float a = Math.Min(1f, Math.Max(0f, (transform.parent.transform.position.y - initialCameraYPosition) / (800f - initialCameraYPosition)));
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, a);
        }
    }

    public void setXSize(float value)
    {
        xSize = value;
    }
}