using UnityEngine;

public class TestController : MonoBehaviour
{

    [SerializeField] private float speed = 5.0f;
    private Vector2 direction = Vector2.zero;
    private Rigidbody2D rb;
    private InputBinder inputBinder;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputBinder = GetComponent<InputBinder>();
    }

    void Update()
    {
        direction = Vector2.zero;

        if(inputBinder.GetMoveLeftInput())
        {
            direction += Vector2.left;
        }
        if(inputBinder.GetMoveUpInput())
        {
            direction += Vector2.up;
        }
        if(inputBinder.GetMoveRightInput())
        {
            direction += Vector2.right;
        }
        if(inputBinder.GetMoveDownInput())
        {
            direction += Vector2.down;
        }

        direction = direction.normalized;
        // Debug.Log($"direction: {direction}");
    }

    void FixedUpdate()
    {
        if (direction != Vector2.zero)
        {
            rb.AddForce(direction * speed);
        }
    }
}
