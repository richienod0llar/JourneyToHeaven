using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]

public class CliffController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // spriteRenderer.sortingLayer
        spriteRenderer.sortingLayerName = "Cliff";
        // transform.localScale
        transform.localScale = new Vector3(1f, 1f, 1f);
        float scale = 20f / spriteRenderer.bounds.size.x;
        transform.localScale = new Vector3(scale, scale, 1f);
        // transform.position
        transform.position = new Vector3(0f, 100f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
    }
}