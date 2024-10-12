using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SpriteRenderer))]

// sky is a child of camera
// sprite is square (default asset)
public class SkyController : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject blackHole;
    private Camera cameraCamera;
    private float deltaCameraYPosition;
    private float initialCameraYPosition;
    private Color initialColor;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        blackHole = GameObject.Find("BlackHole");
        cameraCamera = transform.parent.GetComponent<Camera>();
        initialCameraYPosition = transform.parent.position.y;
        deltaCameraYPosition = 300f - initialCameraYPosition;
        initialColor = new Color(0.5f, 0.8f, 1f, 1f);
        spriteRenderer = GetComponent<SpriteRenderer>();
        // audioSource
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.Play();
        audioSource.Pause();
        // spriteRenderer.color
        spriteRenderer.color = initialColor;
        // spriteRenderer.sortingLayer
        spriteRenderer.sortingLayerName = "Sky";
        // transform.localPosition
        float zPosition = 10f / transform.parent.transform.localScale.z;
        transform.localPosition = new Vector3(0f, 0f, zPosition);
    }

    // Update is called once per frame
    void Update()
    {
        // audioSource (fade out between y = 100 and y = 300)
        if (audioSource.isPlaying)
        {
            audioSource.volume = 1f - Math.Max(0f, Math.Min(1f, (transform.position.y - 100f) / 200f));
        }
        // spriteRenderer.color (sunset)
        if (transform.parent.transform.position.y < 400f)
        {
            float movedDistance = transform.parent.position.y - initialCameraYPosition;
            float ratio = movedDistance / deltaCameraYPosition;
            float r = Math.Max(0f, initialColor.r - initialColor.r * ratio);
            float g = Math.Max(0f, initialColor.g - initialColor.g * ratio);
            float b = Math.Max(0f, initialColor.b - initialColor.b * ratio);
            spriteRenderer.color = new Color(r, g, b, 1f);
        }
        // transform.localScale (depends on camera.orthographicSize, which is controlled in cameraController.Update)
        transform.localScale = new Vector3(1f, 1f, 1f);
        float xScale = 20f / spriteRenderer.bounds.size.x;
        float yScale = cameraCamera.orthographicSize * 2f / spriteRenderer.bounds.size.y;
        transform.localScale = new Vector3(xScale, yScale, 1f);
    }
}