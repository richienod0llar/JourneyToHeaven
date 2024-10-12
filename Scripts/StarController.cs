using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]

// star is a child of camera
// sprite is a circle (default asset)
public class StarController : MonoBehaviour
{
    private Camera cameraCamera;
    private float deltaCameraYPosition;
    // yPositionRatio is between 0 and 1, where 0 = lower border of the screen and 1 = upper border of the screen
    private float deltaYPositionRatio;
    private float initialCameraYPosition;
    private float initialYPositionRatio;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        cameraCamera = transform.parent.GetComponent<Camera>();
        // deltaYPositionRatio = 0: star does not move at all (in relation to the camera)
        // deltaYPositionRatio = 1: in the time the camera moves from y = 0 to y = 700, the star moves one screen height
        deltaYPositionRatio = UnityEngine.Random.Range(0f, 1f);
        initialCameraYPosition = transform.parent.transform.position.y;
        deltaCameraYPosition = 700f - initialCameraYPosition;
        initialYPositionRatio = (transform.localPosition.y / transform.parent.transform.localScale.y + cameraCamera.orthographicSize) / (cameraCamera.orthographicSize * 2f);
        spriteRenderer = GetComponent<SpriteRenderer>();
        // spriteRenderer.color
        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        // spriteRenderer.sortingLayer
        spriteRenderer.sortingLayerName = "Stars";
        // transform.localScale
        transform.localScale = new Vector3(1f, 1f, 1f);
        float size = UnityEngine.Random.Range(0.01f, 0.05f);
        float scale = size / spriteRenderer.bounds.size.x;
        transform.localScale = new Vector3(scale, scale, 1f);
        // transform.position is handled by starSpawnerController
    }

    // Update is called once per frame
    void Update()
    {
        // destroy when camera above y = 700
        if (transform.parent.transform.position.y > 700f)
        {
            Destroy(gameObject);
        }
        // destroy when out of screen
        if (transform.position.y < transform.parent.transform.position.y - cameraCamera.orthographicSize - spriteRenderer.bounds.size.y)
        {
            Destroy(gameObject);
        }
        // transform.localPosition
        float movedDistance = transform.parent.transform.position.y - initialCameraYPosition;
        float ratio = movedDistance / deltaCameraYPosition;
        float localYPositionRatio = initialYPositionRatio - deltaYPositionRatio * ratio;
        float localYPosition = (localYPositionRatio * cameraCamera.orthographicSize * 2f - cameraCamera.orthographicSize) / transform.parent.transform.localScale.y;
        float localZPosition = 10f / transform.parent.transform.localScale.y;
        transform.localPosition = new Vector3(transform.localPosition.x, localYPosition, localZPosition);
        // become less transparent between y = 100 and y = 300
        if (transform.parent.transform.position.y > 90f && transform.parent.transform.position.y < 310f)
        {
            float a = Math.Min(1f, Math.Max(0f, (transform.parent.transform.position.y - 100f) / 200f));
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, a);
        }
    }
}