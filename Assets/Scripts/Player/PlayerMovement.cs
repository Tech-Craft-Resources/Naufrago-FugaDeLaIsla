using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private NewInput newInput;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        newInput = GetComponent<NewInput>();
    }

    void Update()
    {
        float h = newInput.inputX;
        rb.velocity = new Vector2(h * moveSpeed, rb.velocity.y);

        if (h > 0 && !facingRight) Flip();
        if (h < 0 && facingRight) Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(
            -transform.localScale.x,
            transform.localScale.y,
            1f);
    }
}