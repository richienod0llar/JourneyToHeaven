using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(AudioSource))]
// [RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class ThundercloudController : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject camera;
    private Camera cameraCamera;
    private float lastCameraYPosition;
    [SerializeField]
    private GameObject lightningPrefab;
    private PolygonCollider2D polygonCollider2D;
    private Rigidbody2D rigidBody2D;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        camera = GameObject.Find("Camera");
        cameraCamera = camera.GetComponent<Camera>();
        lastCameraYPosition = camera.transform.position.y;
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // audioSource
        audioSource.panStereo = transform.position.x / 10f;
        // gameObject.tag
        gameObject.tag = "Thundercloud";
        // lightning
        GameObject lightning = Instantiate(lightningPrefab, transform) as GameObject;
        // polygonCollider2D
        polygonCollider2D.isTrigger = true;
        // rigidbody2D
        rigidBody2D.isKinematic = true;
        // spriteRenderer.sortingLayer
        spriteRenderer.sortingLayerName = "Thunderclouds";
        // transform.localScale and transform.position get handled by thundercloudSpawnerController
    }

    // Update is called once per frame
    void Update()
    {
        // destroy when out of screen
        if (transform.position.y < camera.transform.position.y - cameraCamera.orthographicSize * 2f - spriteRenderer.bounds.size.y * 2f)
        {
            Destroy(gameObject);
        }
        // transform.position
        float deltaCameraYPosition = camera.transform.position.y - lastCameraYPosition;
        float yPosition = transform.position.y + deltaCameraYPosition / 3f;
        transform.position = new Vector3(transform.position.x, yPosition, 0f);
        // lastCameraYPosition
        lastCameraYPosition = camera.transform.position.y;
    }
}