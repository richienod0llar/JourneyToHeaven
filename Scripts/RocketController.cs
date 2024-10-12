using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class RocketController : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private AudioClip explosionSound;
    private AudioSource audioSource;
    private bool reachedBlackHole = false;
    private int condition = 4;
    private float explosionTime;
    private bool gameOver = false;
    private GameObject player;
    private PlayerController playerController;
    private PolygonCollider2D polygonCollider2D;
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private float verticalFlyingSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        explosionTime = explosionSound.length;
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // audioSource
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = 0.4f;
        audioSource.Play();
        audioSource.Pause();
        // polygonCollider2D
        polygonCollider2D.isTrigger = true;
        // rigidbody2D
        rigidbody2D.isKinematic = true;
        // spriteRenderer.sortingLayer
        spriteRenderer.sortingLayerName = "Rocket";
        // transform.localScale
        transform.localScale = new Vector3(1f, 1f, 1f);
        float scale = 6f / spriteRenderer.bounds.size.y;
        transform.localScale = new Vector3(scale, scale, 1f);
        // transform.position
        transform.position = new Vector3(4.3f, 301.3f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        // destroy when out of world
        if (transform.position.y > 1200f)
        {
            Destroy(gameObject);
        }
        // audioSource.panStereo
        audioSource.panStereo = transform.position.x / 10f;
        // gameOver
        if (gameOver == true)
        {
            explosionTime -= Time.deltaTime;
            if (explosionTime < 0f)
            {
                Destroy(player);
            }
        }
        // vertical movement
        float verticalDistance = verticalFlyingSpeed * Time.deltaTime;
        Vector3 verticalMovement = new Vector3(0f, verticalDistance, 0f);
        transform.Translate(verticalMovement);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // collision with asteroid
        if (collider.gameObject.tag == "Asteroid" && gameOver == false && reachedBlackHole == false)
        {
            animator.SetTrigger("collisionWithAsteroid");
            audioSource.volume = Math.Max(0f, audioSource.volume - 0.1f);
            condition--;
            playerController.DecrementVerticalFlyingSpeed(4f);
            if (condition > 0)
            {
                collider.GetComponent<AudioSource>().Play();
            }
            else
            {
                collider.GetComponent<AudioSource>().panStereo = transform.position.x / 10f;
                collider.GetComponent<AudioSource>().PlayOneShot(explosionSound);
                gameOver = true;
                playerController.SetIsGameOver(true);
            }
        }
        // collision with blackHole
        else if (collider.gameObject.tag == "BlackHole")
        {
            reachedBlackHole = true;
        }
    }

    public void SetVerticalFlyingSpeed(float value)
    {
        verticalFlyingSpeed = value;
    }
}