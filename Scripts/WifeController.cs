using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class WifeController : MonoBehaviour
{
    private Animator animator;
    private bool gameOver = false;
    private bool isPlayerLeft;
    private bool isPlayerRight;
    private GameObject player;
    private bool runningWithPlayer = false;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        // spriteRenderer
        spriteRenderer.sortingLayerName = "Wife";
        // transform.localScale
        transform.localScale = new Vector3(1f, 1f, 1f);
        float scale = 1f / spriteRenderer.bounds.size.x;
        transform.localScale = new Vector3(scale, scale, 1f);
        // transform.position
        transform.position = new Vector3(0f, 1098.5f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        // destroy when out of screen
        if (transform.position.x < -40f || transform.position.x > 40f)
        {
            Destroy(gameObject);
        }
        // isPlayerLeft/isPlayerRight
        if (gameOver == false && runningWithPlayer == false)
        {
            if (player.transform.position.x <= transform.position.x)
            {
                isPlayerLeft = true;
                isPlayerRight = false;
            }
            else
            {
                isPlayerLeft = false;
                isPlayerRight = true;
            }
        }
        // animator
        animator.SetBool("isPlayerLeft", isPlayerLeft);
        animator.SetBool("isPlayerRight", isPlayerRight);
        animator.SetBool("runningWithPlayer", runningWithPlayer);
        // RunningWithPlayer
        if (runningWithPlayer)
        {
            RunningWithPlayer();
        }
    }

    void RunningWithPlayer()
    {
        // movement
        float horizontalDistance;
        if (isPlayerLeft)
        {
            horizontalDistance = 4f * Time.deltaTime;
        }
        else
        {
            horizontalDistance = -1f * 4f * Time.deltaTime;
        }
        Vector3 movement = new Vector3(horizontalDistance, 0f, 0f);
        transform.Translate(movement);
    }

    public void SetIsGameOver(bool value)
    {
        gameOver = value;
    }

    public void SetRunningWithPlayer(bool value)
    {
        runningWithPlayer = value;
    }
}