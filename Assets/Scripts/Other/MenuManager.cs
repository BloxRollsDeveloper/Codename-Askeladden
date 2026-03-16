using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("EmilioMainScene");
    }

    public void CreditsGame()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
