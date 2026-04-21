using UnityEngine;
using System.Collections;

public enum CollectibleType
{
    Banana,
    PoisonApple,
    FrogPowerUp,
    Lifesaver,
    Chicken,
    Chest
}

public class Collectible : MonoBehaviour
{
    [Header("Tipo y Puntos")]
    public CollectibleType type;
    public int pointValue;

    [Header("Audio")]
    public AudioClip collectSound;

    // Solo la manzana necesita este flag
    private bool appleOnCooldown = false;

    // ── Coleccionables normales (entrar al trigger los destruye) ──────────
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (type == CollectibleType.PoisonApple) return; // La manzana usa Stay

        PlayerJump  playerJump   = other.GetComponent<PlayerJump>();
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        switch (type)
        {
            case CollectibleType.Banana:
                ScoreManager.Instance?.AddScore(pointValue);
                AudioManager.Instance?.PlaySFX(collectSound);
                GameManager.Instance?.AddLife(1);
                GetComponent<Animator>().SetTrigger("Collect");
                Destroy(gameObject, 0.5f);
                break;

            case CollectibleType.FrogPowerUp:
                ScoreManager.Instance?.AddScore(pointValue);
                AudioManager.Instance?.PlaySFX(collectSound);
                playerJump?.ActivateFrog();
                GetComponent<Animator>().SetTrigger("Collect");
                Destroy(gameObject, 0.5f);
                break;

            case CollectibleType.Lifesaver:
            case CollectibleType.Chicken:
            case CollectibleType.Chest:
                ScoreManager.Instance?.AddScore(pointValue);
                AudioManager.Instance?.PlaySFX(collectSound);
                Destroy(gameObject);
                break;
        }
    }

    // ── Manzana envenenada: detecta mientras el jugador esté adentro ──────
    void OnTriggerStay2D(Collider2D other)
    {
        if (type != CollectibleType.PoisonApple) return;
        if (!other.CompareTag("Player")) return;
        if (appleOnCooldown) return;

        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth == null) return;

        bool damaged = playerHealth.TryTakeDamage(1);
        if (damaged)
        {
            AudioManager.Instance?.PlaySFX(collectSound);
            GetComponent<Animator>().SetTrigger("Collect");
            Destroy(gameObject, 0.5f);
        }
        else
        {
            // Jugador invencible: esperar a que termine y reintentar
            StartCoroutine(AppleCooldown(playerHealth));
        }
    }

    IEnumerator AppleCooldown(PlayerHealth playerHealth)
    {
        appleOnCooldown = true;

        // Espera hasta que el jugador deje de ser invencible
        yield return new WaitUntil(() => !playerHealth.IsInvincible);

        // Un frame extra para que el motor registre el Stay correctamente
        yield return null;

        appleOnCooldown = false;
        // OnTriggerStay2D se encargará del resto en el siguiente frame
    }
}