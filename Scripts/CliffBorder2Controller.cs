using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

// cliffBorder2 is a child of cliff
public class CliffBorder2Controller : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    private Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        // boxCollider2D
        boxCollider2D.size = new Vector2(20f, 1f);
        // rigidbody2D
        rigidbody2D.isKinematic = true;
        // transform.localPosition
        transform.localPosition = new Vector3(0f, 4.8f, 0f);
        // transform.localScale
        float xScale = 1f / transform.parent.transform.localScale.x;
        float yScale = 1f / transform.parent.transform.localScale.y;
        transform.localScale = new Vector3(xScale, yScale, 1f);
    }

    // Update is called once per frame
    void Update()
    {
    }
}