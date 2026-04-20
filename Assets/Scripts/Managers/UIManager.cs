using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text scoreText;
    public Image[] lifeIcons;
    public Image powerUpBar;
    public Text timerText;

    private float elapsedTime = 0f;
    private bool timing = true;
    private PlayerJump playerJump;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Ocultar la barra al inicio
        if (powerUpBar != null)
            powerUpBar.gameObject.SetActive(false);

        // Buscar el PlayerJump en la escena
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            playerJump = player.GetComponent<PlayerJump>();
    }

    void Update()
    {
        // Timer
        if (timing && timerText != null)
        {
            elapsedTime += Time.deltaTime;
            int m = (int)(elapsedTime / 60);
            int s = (int)(elapsedTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", m, s);
        }

        // Barra sincronizada directamente con el estado real de PlayerJump
        if (powerUpBar == null || playerJump == null) return;

        if (playerJump.IsFrogActive)
        {
            powerUpBar.gameObject.SetActive(true);
            powerUpBar.fillAmount = playerJump.FrogTimeRemaining / playerJump.FrogDuration;
        }
        else
        {
            powerUpBar.gameObject.SetActive(false);
        }
    }

    public void UpdateScore(int score)
    {
        if (scoreText == null) return;
        scoreText.text = score.ToString("D6");
    }

    public void UpdateLives(int lives)
    {
        if (lifeIcons == null || lifeIcons.Length == 0) return;
        for (int i = 0; i < lifeIcons.Length; i++)
            lifeIcons[i].color = i < lives ? Color.white : new Color(1, 1, 1, 0.2f);
    }

    // Se mantiene para que DeathZone y otros scripts puedan forzar ocultar la barra
    public void HidePowerUpBar()
    {
        if (powerUpBar != null)
            powerUpBar.gameObject.SetActive(false);
    }

    public void StopTimer()
    {
        timing = false;
    }
}