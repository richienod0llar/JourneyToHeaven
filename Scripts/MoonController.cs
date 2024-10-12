using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]

// moon is a child of camera
public class MoonController : MonoBehaviour
{
    private Camera cameraCamera;
    private float deltaCameraYPositionPosition;
    private float deltaCameraYPositionTransparency;
    private float initialCameraYPosition;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        cameraCamera = transform.parent.GetComponent<Camera>();
        initialCameraYPosition = transform.parent.position.y;
        deltaCameraYPositionPosition = 500f - initialCameraYPosition;
        deltaCameraYPositionTransparency = 300f - initialCameraYPosition;
        spriteRenderer = GetComponent<SpriteRenderer>();
        // spriteRenderer.sortingLayer
        spriteRenderer.sortingLayerName = "Moon";
        // transform.localPosition
        float localXPosition = UnityEngine.Random.Range(-9f, 9f) / transform.parent.transform.localScale.x;
        transform.localPosition = new Vector3(localXPosition, transform.localPosition.y, 10f);
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
        Position();
        Transparency();
    }

    void Position()
    {
        float movedDistance = transform.parent.position.y - initialCameraYPosition;
        float ratio = movedDistance / deltaCameraYPositionPosition;
        // transform.localPosition (depends on cameraCamera.orthographicSize, which gets handled in cameraController.Update)
        float initialLocalYPosition = cameraCamera.orthographicSize + spriteRenderer.bounds.size.y * 0.6f;
        float finalLocalYPosition = -cameraCamera.orthographicSize - spriteRenderer.bounds.size.y * 0.6f;
        float deltaLocalYPosition = initialLocalYPosition - finalLocalYPosition;
        float newLocalYPosition = (initialLocalYPosition - deltaLocalYPosition * ratio) / transform.parent.transform.localScale.y;
        transform.localPosition = new Vector3(transform.localPosition.x, newLocalYPosition, 10f);
    }

    void Transparency()
    {
        // become less transparent between y = 100 and y = 300
        if (transform.parent.transform.position.y < 310f)
        {
            float movedDistance = transform.parent.position.y - initialCameraYPosition;
            float ratio = movedDistance / deltaCameraYPositionTransparency;
            float a = Math.Min(1f, ratio);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, a);
        }
    }
}