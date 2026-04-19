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

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (!timing) return;
        if (timerText == null) return;

        elapsedTime += Time.deltaTime;
        int m = (int)(elapsedTime / 60);
        int s = (int)(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", m, s);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString("D6");
    }

    public void UpdateLives(int lives)
    {
        for (int i = 0; i < lifeIcons.Length; i++)
            lifeIcons[i].color = i < lives ? Color.white : new Color(1, 1, 1, 0.2f);
    }

    public void ShowPowerUpBar(float duration)
    {
        if (powerUpBar != null)
        {
            powerUpBar.gameObject.SetActive(true);
            StartCoroutine(DrainBar(duration));
        }
    }

    IEnumerator DrainBar(float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            powerUpBar.fillAmount = 1f - (t / duration);
            yield return null;
        }
        HidePowerUpBar();
    }

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