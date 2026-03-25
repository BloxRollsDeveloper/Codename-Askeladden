using UnityEngine;
using UnityEngine.SceneManagement;

public class HuldraFigthExit : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("EmilioMainScenePart2");
        }
    }
}
