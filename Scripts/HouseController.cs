using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]

// house is a child of cliff
public class HouseController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // spriteRenderer.sortingLayer
        spriteRenderer.sortingLayerName = "House";
        // transform.localPosition
        transform.localPosition = new Vector3(0f, 7.2f, 0f);
        // transform.localScale
        transform.localScale = new Vector3(1f, 1f, 1f);
        float scale = 6f / spriteRenderer.bounds.size.x;
        transform.localScale = new Vector3(scale, scale, 1f);
    }

    // Update is called once per frame
    void Update()
    {
    }
}