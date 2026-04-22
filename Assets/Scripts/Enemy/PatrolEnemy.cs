using UnityEngine;
using System.Collections;

public class PatrolEnemy : MonoBehaviour
{
    [Header("Patrulla")]
    public float speed = 1.5f;
    public Transform pointA;
    public Transform pointB;
    private Transform target;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    public int damageScoreValue = 1;

    void Start()
    {
        target = pointB;
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (pointA == null || pointB == null) return;

        Vector2 newPos = Vector2.MoveTowards(
            rb.position,
            new Vector2(target.position.x, rb.position.y),
            speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        sr.flipX = (target != pointB);

        if (Mathf.Abs(rb.position.x - target.position.x) < 0.1f)
            target = target == pointA ? pointB : pointA;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerHealth health = other.GetComponent<PlayerHealth>();
        if (health != null)
            health.TryTakeDamage(1);
            ScoreManager.Instance?.DecreaseScore(damageScoreValue);
        }
}