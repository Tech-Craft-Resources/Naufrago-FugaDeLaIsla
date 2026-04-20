using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int lives = 3;
    public int maxLives = 3;

    private Vector3 checkpointPos;
    private GameObject player;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        checkpointPos = player.transform.position;
    }

    // Usado por enemigos y manzana envenenada — solo quita vida, sin respawn
    public void TakeDamage(int amount)
    {
        lives -= amount;
        UIManager.Instance.UpdateLives(lives);

        if (lives <= 0)
            GameOver();
    }

    // Usado por DeathZone (agua/hueco) — quita vida Y hace respawn
    public void FallDeath()
    {
        lives -= 1;
        UIManager.Instance.UpdateLives(lives);

        if (lives <= 0)
            GameOver();
        else
            StartCoroutine(Respawn());
    }

    public void AddLife(int amount)
    {
        lives = Mathf.Min(lives + amount, maxLives);
        UIManager.Instance.UpdateLives(lives);
    }

    System.Collections.IEnumerator Respawn()
    {
        // Resetear el sprite antes de mover (cancela parpadeo colgado)
        PlayerHealth ph = player.GetComponent<PlayerHealth>();
        if (ph != null) ph.ForceResetSprite();

        // Mover al checkpoint — la cámara nunca pierde el target
        player.transform.position = checkpointPos;

        yield return new WaitForSeconds(0.1f);
    }

    public void SetCheckpoint(Vector3 pos)
    {
        checkpointPos = pos;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void Win()
    {
        SceneManager.LoadScene("WinScreen");
    }
}