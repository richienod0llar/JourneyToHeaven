using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]

// sun is a child of camera
// sprite is a circle (default asset)
public class SunController : MonoBehaviour
{
    private Camera cameraCamera;
    private float deltaCameraYPositionColor;
    private float deltaCameraYPositionPosition;
    private float initialCameraYPosition;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        cameraCamera = transform.parent.GetComponent<Camera>();
        initialCameraYPosition = transform.parent.position.y;
        deltaCameraYPositionColor = 200f - initialCameraYPosition;
        deltaCameraYPositionPosition = 300f - initialCameraYPosition;
        spriteRenderer = GetComponent<SpriteRenderer>();
        // spriteRenderer.color
        spriteRenderer.color = new Color(1f, 1f, 0f, 1f);
        // spriteRenderer.sortingLayer
        spriteRenderer.sortingLayerName = "Sun";
        // transform.localScale
        transform.localScale = new Vector3(1f, 1f, 1f);
        float scale = 3f / spriteRenderer.bounds.size.x;
        transform.localScale = new Vector3(scale, scale, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        // destroy when out of screen
        if (transform.position.y < transform.parent.transform.position.y - cameraCamera.orthographicSize - spriteRenderer.bounds.size.y)
        {
            Destroy(gameObject);
        }
        SunsetColor();
        SunsetPosition();
    }

    void SunsetColor()
    {
        float movedDistance = transform.parent.position.y - initialCameraYPosition;
        float ratio = movedDistance / deltaCameraYPositionColor;
        // spriteRenderer.color (sunset)
        if (spriteRenderer.color.g < 0.001f)
        {
            spriteRenderer.color = new Color(1f, 0f, 0f, 1f);
        }
        else
        {
            float g = Math.Max(0f, 1f - 1f * ratio);
            spriteRenderer.color = new Color(1f, g, 0f, 1f);
        }
    }

    void SunsetPosition()
    {
        float movedDistance = transform.parent.position.y - initialCameraYPosition;
        float ratio = movedDistance / deltaCameraYPositionPosition;
        // transform.localPosition (sunset) (depends on cameraCamera.orthographicSize, which gets handled in cameraController.Update)
        float initialLocalYPosition = cameraCamera.orthographicSize - spriteRenderer.bounds.size.y * 0.6f;
        float finalLocalYPosition = -cameraCamera.orthographicSize - spriteRenderer.bounds.size.y * 0.6f;
        float deltaLocalYPosition = initialLocalYPosition - finalLocalYPosition;
        float newLocalYPosition = (initialLocalYPosition - deltaLocalYPosition * ratio) / transform.parent.transform.localScale.y;
        transform.localPosition = new Vector3(0f, newLocalYPosition, 10f);
    }
}