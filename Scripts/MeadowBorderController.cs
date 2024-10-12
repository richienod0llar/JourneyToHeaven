using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

// meadowBorder is a child of meadow
public class MeadowBorderController : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    private GameObject player;
    private BoxCollider2D playerBoxCollider2D;
    private SpriteRenderer meadowSpriteRenderer;
    private Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        player = GameObject.Find("Player");
        playerBoxCollider2D = player.GetComponent<BoxCollider2D>();
        meadowSpriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        // boxCollider
        float height = 2f * (playerBoxCollider2D.bounds.center.y - playerBoxCollider2D.bounds.extents.y);
        boxCollider2D.size = new Vector2(20f, height);
        // rigidbody2D
        rigidbody2D.isKinematic = true;
        // transform.localPosition
        float localYPosition = (-meadowSpriteRenderer.bounds.size.y / 2f) / transform.parent.transform.localScale.y;
        transform.localPosition = new Vector3(0f, localYPosition, 0f);
        // transform.localScale
        float xScale = 1 / transform.parent.localScale.x;
        float yScale = 1 / transform.parent.localScale.y;
        transform.localScale = new Vector3(xScale, yScale, 1f);
    }

    // Update is called once per frame
    void Update()
    {
    }
}