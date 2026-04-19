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

    public void TakeDamage(int amount)
    {
        lives -= amount;
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
        player.SetActive(false);
        yield return new WaitForSeconds(1f);
        player.transform.position = checkpointPos;
        player.SetActive(true);
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