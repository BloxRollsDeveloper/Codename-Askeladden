using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [Header("DeathScreen")]
    [SerializeField] private GameObject deathScreen;
    
    public void UponDeath()
    {
        deathScreen.SetActive(true);
    }
    
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
