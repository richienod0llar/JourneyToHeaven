using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

// sprite is a circle (default asset)
public class BlackHoleController : MonoBehaviour
{
    private GameObject camera;
    private CircleCollider2D circleCollider2D;
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Camera");
        circleCollider2D = GetComponent<CircleCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // circleCollider2D
        circleCollider2D.isTrigger = true;
        // gameObject.tag
        gameObject.tag = "BlackHole";
        // rigidbody2D
        rigidbody2D.isKinematic = true;
        // spriteRenderer.color
        spriteRenderer.color = new Color(0f, 0f, 0f, 1f);
        // spriteRenderer.sortingLayer
        spriteRenderer.sortingLayerName = "BlackHole";
        // transform.localScale
        transform.localScale = new Vector3(1f, 1f, 1f);
        float scale = 40f / spriteRenderer.bounds.size.x;
        transform.localScale = new Vector3(scale, scale, 1f);
        // transform.position
        transform.position = new Vector3(0f, 700f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
    }
}