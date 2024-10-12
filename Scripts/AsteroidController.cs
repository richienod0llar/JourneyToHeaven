using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

// asteroid is a child of camera
public class AsteroidController : MonoBehaviour
{
    private AudioSource audioSource;
    private Camera cameraCamera;
    private GameObject player;
    private PlayerController playerController;
    private PolygonCollider2D polygonCollider2D;
    private Rigidbody2D rigidBody2D;
    private float screenPassingTime = 4f;
    private SpriteRenderer spriteRenderer;
    private float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        cameraCamera = transform.parent.GetComponent<Camera>();
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // audioSource
        audioSource.panStereo = transform.position.x / 10f;
        audioSource.playOnAwake = false;
        // gameObject.tag
        gameObject.tag = "Asteroid";
        // polygonCollider2D
        polygonCollider2D.isTrigger = true;
        // rigidbody2D
        rigidBody2D.isKinematic = true;
        // spriteRenderer.sortingLayer
        spriteRenderer.sortingLayerName = "Asteroid";
        // transform.localScale and transform.position are handled by asteroidSpawnerController
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
        // vertical movement
        time += Time.deltaTime;
        float initialLocalYPosition = cameraCamera.orthographicSize + spriteRenderer.bounds.size.y * 0.5f;
        float finalLocalYPosition = -cameraCamera.orthographicSize - spriteRenderer.bounds.size.y;
        float deltaLocalYPosition = initialLocalYPosition - finalLocalYPosition;
        float ratio = time / screenPassingTime;
        float localYPosition = (initialLocalYPosition - deltaLocalYPosition * ratio) / transform.parent.transform.localScale.y;
        float localZPosition = 10f / transform.parent.transform.localScale.z;
        transform.localPosition = new Vector3(transform.localPosition.x, localYPosition, localZPosition);
    }
}