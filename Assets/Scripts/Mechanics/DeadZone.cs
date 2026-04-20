using UnityEngine;

public class DeathZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerJump pj = other.GetComponent<PlayerJump>();
        PlayerHealth ph = other.GetComponent<PlayerHealth>();

        if (!other.CompareTag("Player")) return;

        if (ph == null) return;

        ph.StartInvincibility();           // parpadeo mientras reaparece
        if (pj != null)
            pj.DeactivateFrog();
        GameManager.Instance.FallDeath();  // quita 1 vida + respawn
    }
}