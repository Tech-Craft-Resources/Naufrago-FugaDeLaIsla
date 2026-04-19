using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    [Header("Patrulla")]
    public float speed = 1.5f;
    public Transform pointA;
    public Transform pointB;

    private Transform target;
    private SpriteRenderer sr;

    void Start()
    {
        target = pointB;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (pointA == null || pointB == null) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime);

        // Voltear el sprite según la dirección
        if (target == pointB)
            sr.flipX = false;
        else
            sr.flipX = true;

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
            target = target == pointA ? pointB : pointA;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            GameManager.Instance.TakeDamage(1);
    }
}