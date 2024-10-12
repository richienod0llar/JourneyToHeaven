using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class HotAirBalloonController : MonoBehaviour
{
    [SerializeField]
    private GameObject lightningPrefab;
    private GameObject player;
    private PlayerController playerController;
    private PolygonCollider2D polygonCollider2D;
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private float verticalFlyingSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // polygonCollider2D
        polygonCollider2D.isTrigger = true;
        // rigidbody2D
        rigidbody2D.isKinematic = true;
        // transform.localScale
        transform.localScale = new Vector3(1f, 1f, 1f);
        float scale = 5f / spriteRenderer.bounds.size.y;
        transform.localScale = new Vector3(scale, scale, 1f);
        // transform.position
        transform.position = new Vector3(-8f, 107.6f, 0f);
        // spriteRenderer.sortingLayer
        spriteRenderer.sortingLayerName = "HotAirBalloon";
    }

    // Update is called once per frame
    void Update()
    {
        // destroy when out of world
        if (transform.position.y > 1200f)
        {
            Destroy(gameObject);
        }
        // vertical movement
        float verticalDistance = verticalFlyingSpeed * Time.deltaTime;
        Vector3 verticalMovement = new Vector3(0f, verticalDistance, 0f);
        transform.Translate(verticalMovement);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // collision with thundercloud
        if (collider.gameObject.tag == "Thundercloud")
        {
            GameObject lightning = Instantiate(lightningPrefab, transform) as GameObject;
            lightning.GetComponent<LightningController>().SetIsChildOfHotAirBalloon(true);
            playerController.SetStunTime(4f);
        }
    }

    public void SetVerticalFlyingSpeed(float value)
    {
        verticalFlyingSpeed = value;
    }
}
