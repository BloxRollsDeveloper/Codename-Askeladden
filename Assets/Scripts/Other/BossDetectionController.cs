using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDetectionController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
            SceneManager.LoadScene("BossStage");
        Debug.Log("Player Detected");
    }
}
