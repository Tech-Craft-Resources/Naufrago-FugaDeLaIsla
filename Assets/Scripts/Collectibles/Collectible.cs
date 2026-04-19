using UnityEngine;

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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerJump playerJump = other.GetComponent<PlayerJump>();

        if (ScoreManager.Instance != null)
            ScoreManager.Instance.AddScore(pointValue);
            
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX(collectSound);

        switch (type)
        {
            case CollectibleType.Banana:
                if (GameManager.Instance != null)
                    GameManager.Instance.AddLife(1);
                    Debug.Log("Puntos: " + pointValue);
                    Debug.Log("Vida: " + GameManager.Instance.lives);
                break;

            case CollectibleType.PoisonApple:
                if (GameManager.Instance != null)
                    GameManager.Instance.TakeDamage(1);
                    Debug.Log("Vida: " + GameManager.Instance.lives);
                break;

            case CollectibleType.FrogPowerUp:
                if (playerJump != null)
                    playerJump.ActivateFrog();
                    Debug.Log("Puntos: " + pointValue);

                break;

            case CollectibleType.Lifesaver:
                // Por ahora solo da puntos, la mecánica de flotación va después
                break;

            case CollectibleType.Chicken:
                // Bonus de puntos, ya sumado arriba
                break;

            case CollectibleType.Chest:
                // Bonus masivo de puntos, ya sumado arriba
                break;
        }

        Destroy(gameObject);
    }
}