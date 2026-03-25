using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class HuldraFigthExit : MonoBehaviour
{
    [Header("Huldras")]
    [SerializeField] private HuldraScrubby huldraSenior;
    [SerializeField] private HuldraScrubby huldraJunior;
    
    private Collider2D _collider;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = false;
    }

    private void Update()
    {
        if (huldraSenior.isDead && huldraJunior.isDead)
        {
            _collider.isTrigger = true;
        }
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("EmilioMainScenePart2");
        }
    }
}
