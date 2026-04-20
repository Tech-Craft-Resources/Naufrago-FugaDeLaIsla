using UnityEngine;

public class DeathZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerHealth ph = other.GetComponent<PlayerHealth>();
        if (ph == null) return;

        if (!ph.IsInvincible)
        {
            ph.StartInvincibility();           // parpadeo mientras reaparece
            GameManager.Instance.FallDeath();  // quita 1 vida + respawn
        }
    }
}