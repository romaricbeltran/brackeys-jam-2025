using UnityEngine;

public class TestController : MonoBehaviour
{

    [SerializeField] private float speed = 5.0f;
    private Vector2 direction = Vector2.zero;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("left");
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            direction += Vector2.right;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            direction += Vector2.down;
        }

        direction = direction.normalized;
        Debug.Log($"direction: {direction}");
    }

    void FixedUpdate()
    {
        if (direction != Vector2.zero)
        {
            rb.AddForce(direction * speed);
        }
    }
}
