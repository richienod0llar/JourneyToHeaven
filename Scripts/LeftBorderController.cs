using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

// leftBorderController is a child of camera
public class LeftBorderController : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    private Camera cameraCamera;
    private Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        cameraCamera = transform.parent.GetComponent<Camera>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        // rigidbody2D
        rigidbody2D.isKinematic = true;
        // transform.localPosition
        float localXPosition = -10.5f / transform.parent.transform.localScale.x;
        float localZPosition = 10f / transform.parent.transform.localScale.z;
        transform.localPosition = new Vector3(localXPosition, 0f, localZPosition);
        // transform.localScale
        float xScale = 1f / transform.parent.localScale.x;
        float yScale = 1f / transform.parent.localScale.y;
        transform.localScale = new Vector3(xScale, yScale, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        // boxCollider2D.size (depends on camera.orthographicSize, which is controlled in cameraController.Update)
        boxCollider2D.size = new Vector2(1, cameraCamera.orthographicSize * 2f);
    }
}