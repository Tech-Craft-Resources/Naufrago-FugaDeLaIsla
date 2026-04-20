using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Invencibilidad")]
    public float invincibleDuration = 3f;
    public float blinkInterval = 0.15f;

    [Header("Layers")]
    public string enemyLayerName = "Enemy";       // Layer de enemigos/triggers
    public string playerLayerName = "Player";     // Layer del jugador

    private bool isInvincible = false;
    private SpriteRenderer sr;
    private int playerLayer;
    private int enemyLayer;

    public bool IsInvincible => isInvincible;     // Para que Collectible pueda consultarlo

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        playerLayer = LayerMask.NameToLayer(playerLayerName);
        enemyLayer  = LayerMask.NameToLayer(enemyLayerName);
    }

    public bool TryTakeDamage(int amount)
    {
        if (isInvincible) return false;

        GameManager.Instance.TakeDamage(amount);
        Debug.Log("Vida: " + GameManager.Instance.lives);
        StartCoroutine(InvincibilityRoutine());
        return true;
    }

    public bool TryFallDeath()
    {
        if (isInvincible) return false;

        GameManager.Instance.FallDeath();
        StartCoroutine(InvincibilityRoutine());
        return true;
    }

    IEnumerator InvincibilityRoutine()
    {
        isInvincible = true;

        // Ignorar colisiones físicas y triggers entre Player y Enemy mientras dura
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);

        float timer = 0f;
        while (timer < invincibleDuration)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }

        sr.enabled = true;
        isInvincible = false;

        // Restaurar colisiones
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
    }

    public void StartInvincibility()
    {
        if (!isInvincible)
            StartCoroutine(InvincibilityRoutine());
    }

    public void ForceResetSprite()
    {
        StopAllCoroutines();       // cancela cualquier parpadeo en curso
        sr.enabled = true;         // garantiza que el sprite es visible
        isInvincible = false;      // limpia el estado
        // Restaurar colisiones por si quedaron ignoradas
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
    }
}