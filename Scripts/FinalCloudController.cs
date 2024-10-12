using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SpriteRenderer))]

public class FinalCloudController : MonoBehaviour
{
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // audioSource
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.Play();
        audioSource.Pause();
        // spriteRenderer.sortingLayer
        spriteRenderer.sortingLayerName = "FinalCloud";
        // transform.localScale
        transform.localScale = new Vector3(1f, 1f, 1f);
        float scale = 20f / spriteRenderer.bounds.size.x;
        transform.localScale = new Vector3(scale, scale, 1f);
        // transform.position
        transform.position = new Vector3(0f, 1100f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
    }
}