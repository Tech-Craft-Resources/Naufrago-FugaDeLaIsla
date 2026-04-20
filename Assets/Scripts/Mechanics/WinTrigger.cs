using System.Collections;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    [SerializeField] private float delay = 2f;
    private bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || triggered) return;

        triggered = true;
        StartCoroutine(WinWithDelay());
    }

    private IEnumerator WinWithDelay()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1f;
        GameManager.Instance.Win();
    }
}