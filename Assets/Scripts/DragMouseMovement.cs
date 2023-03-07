using UnityEngine;

public class DragMouseMovement : MonoBehaviour
{

    private Vector3 mousePosition;
    private Rigidbody2D rb;
    private Vector2 direction;
    private float moveSpeed = 100f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = (mousePosition - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
