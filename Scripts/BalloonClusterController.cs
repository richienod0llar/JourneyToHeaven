using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class BalloonClusterController : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;
    private CapsuleCollider2D capsuleCollider2D;
    private GameObject player;
    private PlayerController playerController;
    private int numberOfBalloons = 8;
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private float verticalFlyingSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // audioSource
        audioSource.playOnAwake = false;
        // capsuleCollider2D
        capsuleCollider2D.direction = CapsuleDirection2D.Horizontal;
        capsuleCollider2D.isTrigger = true;
        // rigidbody2D
        rigidbody2D.isKinematic = true;
        // transform.localScale
        transform.localScale = new Vector3(1f, 1f, 1f);
        float scale = 3 / spriteRenderer.bounds.size.x;
        transform.localScale = new Vector3(scale, scale, 1f);
        // transform.position
        transform.position = new Vector3(8.2f, 3.4f, 0f);
        // spriteRenderer.sortingLayer
        spriteRenderer.sortingLayerName = "BalloonCluster";
    }

    // Update is called once per frame
    void Update()
    {
        // destroy when out of world
        if (transform.position.y < -100f || transform.position.y > 1200f)
        {
            Destroy(gameObject);
        }
        // vertical movement
        float verticalDistance = verticalFlyingSpeed * Time.deltaTime;
        Vector3 verticalMovement = new Vector3(0f, verticalDistance, 0f);
        transform.Translate(verticalMovement);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // collision with hawk
        if (numberOfBalloons >= 5 && collider.gameObject.tag == "Hawk")
        {
            animator.SetTrigger("collisionWithHawk");
            audioSource.panStereo = transform.position.x / 10f;
            audioSource.Play();
            playerController.DecrementVerticalFlyingSpeed(1f);
            numberOfBalloons--;
            // if only 4 balloons left, game over
            if (numberOfBalloons == 4 && transform.parent == player.transform)
            {
                playerController.SetIsGameOver(true);
            }
        }
    }

    public void SetVerticalFlyingSpeed(float value)
    {
        verticalFlyingSpeed = value;
    }
}