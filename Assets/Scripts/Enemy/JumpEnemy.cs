using UnityEngine;
using System.Collections;

public class JumpEnemy : MonoBehaviour
{
    [Header("Salto")]
    public float jumpForce = 8f;
    public float jumpInterval = 2f;
    public float maxHeight = 4f;

    [Header("Daño")]
    public int damage = 1;
    public int damageScoreValue = 1;

    private Vector2 originPosition;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originPosition = transform.position;

        // Delay aleatorio para que varios tiburones no salten sincronizados
        float randomDelay = Random.Range(0f, jumpInterval);
        StartCoroutine(JumpLoop(randomDelay));
    }

    IEnumerator JumpLoop(float initialDelay)
    {
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            // --- FASE 1: Subir ---
            rb.velocity = new Vector2(0f, jumpForce);

            // Espera hasta alcanzar la altura máxima o hasta que empiece a bajar
            yield return new WaitUntil(() =>
                transform.position.y >= originPosition.y + maxHeight ||
                rb.velocity.y <= 0f
            );

            // --- FASE 2: Bajar ---
            rb.velocity = new Vector2(0f, -jumpForce);

            // Espera hasta regresar al origen
            yield return new WaitUntil(() =>
                transform.position.y <= originPosition.y
            );

            // --- FASE 3: Reset limpio ---
            rb.velocity = Vector2.zero;
            transform.position = originPosition;

            // --- FASE 4: Esperar antes del próximo salto ---
            yield return new WaitForSeconds(jumpInterval);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TryTakeDamage(damage);
                ScoreManager.Instance?.DecreaseScore(damageScoreValue);
            }
        }
    }
}