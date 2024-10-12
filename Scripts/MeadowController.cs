using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SpriteRenderer))]

public class MeadowController : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject camera;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        camera = GameObject.Find("Camera");
        spriteRenderer = GetComponent<SpriteRenderer>();
        // audioSource
        audioSource.loop = true;
        // spriteRenderer.sortingLayer
        spriteRenderer.sortingLayerName = "Meadow";
        // transform.localScale
        transform.localScale = new Vector3(1f, 1f, 1f);
        float scale = 20f / spriteRenderer.bounds.size.x;
        transform.localScale = new Vector3(scale, scale, 1f);
        // transform.position
        transform.position = new Vector3(0f, spriteRenderer.bounds.size.y / 2f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        // audioSource (fade out between y = 0 and y = 200)
        if (audioSource.isPlaying)
        {
            if (audioSource.volume == 0f)
            {
                audioSource.Stop();
            }
            else
            {
                audioSource.volume = Math.Max(0f, 1f - camera.transform.position.y / 200f);
            }
        }
    }
}