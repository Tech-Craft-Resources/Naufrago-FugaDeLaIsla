using UnityEngine;
using UnityEngine.SceneManagement;

public class InitGameManager : MonoBehaviour
{
    public void InitGame()
    {
        SceneManager.LoadScene("Level01");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
