using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private PlayerJump playerJump;
    private PlayerHealth playerHealth;

    // Guardamos el estado de invencibilidad anterior para detectar el golpe
    private bool wasInvincible = false;

    void Start()
    {
        anim         = GetComponent<Animator>();
        rb           = GetComponent<Rigidbody2D>();
        playerJump   = GetComponent<PlayerJump>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        // Speed: valor absoluto de la velocidad horizontal
        float speed = Mathf.Abs(rb.velocity.x);
        anim.SetFloat("Speed", speed);

        // IsGrounded y VelocityY: leídos desde PlayerJump
        bool grounded = playerJump != null
            ? Physics2D.OverlapCircle(
                playerJump.groundCheck.position,
                playerJump.checkRadius,
                playerJump.groundLayer)
            : true;

        anim.SetBool("IsGrounded", grounded);
        anim.SetFloat("VelocityY", rb.velocity.y);

        // Hit: se dispara cuando PlayerHealth recién se vuelve invencible
        if (playerHealth != null)
        {
            bool nowInvincible = playerHealth.IsInvincible;
            if (nowInvincible && !wasInvincible)
            {
                anim.SetTrigger("IsHit");
            }
            wasInvincible = nowInvincible;
        }
    }
}