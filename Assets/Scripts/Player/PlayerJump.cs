using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump")]
    public float jumpForce = 10f;
    private float jumpForceBase;

    [Header("Frog Power-Up")]
    public float frogMultiplier = 1.8f;
    public float frogDuration = 8f;
    private bool frogActive = false;
    private float frogTimer = 0f;

    // Propiedades públicas para que UIManager lea el estado real
    public bool IsFrogActive => frogActive;
    public float FrogTimeRemaining => frogTimer;
    public float FrogDuration => frogDuration;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float checkRadius = 0.1f;
    public LayerMask groundLayer;

    private bool isGrounded;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpForceBase = jumpForce;
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position, checkRadius, groundLayer);

        if (frogActive)
        {
            frogTimer -= Time.deltaTime;
            if (frogTimer <= 0f)
                DeactivateFrog();
        }
    }

    public void Jump()
    {
        if (isGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void ActivateFrog()
    {
        frogActive = true;
        frogTimer = frogDuration;
        jumpForce = jumpForceBase * frogMultiplier;
    }

    public void DeactivateFrog()
    {
        frogActive = false;
        jumpForce = jumpForceBase;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        }
    }

    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpForceBase = jumpForce;
    }
}