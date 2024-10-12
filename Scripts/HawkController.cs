using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class HawkController : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject camera;
    private Camera cameraCamera;
    private CapsuleCollider2D capsuleCollider2D;
    private Rigidbody2D rigidBody2D;
    private float speed = -8f;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        camera = GameObject.Find("Camera");
        cameraCamera = camera.GetComponent<Camera>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // audioSource
        audioSource.panStereo = transform.position.x / 10f;
        // capsuleCollider2D
        capsuleCollider2D.isTrigger = true;
        // gameObject.tag
        gameObject.tag = "Hawk";
        // rigidbody2D
        rigidBody2D.isKinematic = true;
        // spriteRenderer.sortingLayer
        spriteRenderer.sortingLayerName = "Hawks";
        // transform.localScale and transform.position are handled by hawkSpawnerController
    }

    // Update is called once per frame
    void Update()
    {   
        // vertical movement
        float movedDistance = speed * Time.deltaTime;
        transform.Translate(new Vector3(0f, movedDistance, 0f));
        // destroy when out of screen
        if (transform.position.y < camera.transform.position.y - cameraCamera.orthographicSize * 2f - spriteRenderer.bounds.size.y * 2f)
        {
            Destroy(gameObject);
        }
    }
}