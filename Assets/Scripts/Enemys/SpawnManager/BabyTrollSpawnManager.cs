using UnityEngine;

public class BabyTrollSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject babyTrollPrefab;
    [SerializeField] private Transform[] babyTrollSpawnPoints;

    private const float SpawnInterval = 20f;
    private float spawnTimer;
    private DialogueManager dialogueManager;

    void Start()
    {
        spawnTimer = SpawnInterval;
        dialogueManager = DialogueManager.GetInstance();
    }

    void Update()
    {
        if (dialogueManager.dialogueIsPlaying) return;
        
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnBabyTroll();
            spawnTimer = SpawnInterval;
        }
    }

    private void SpawnBabyTroll()
    {
        if (babyTrollPrefab == null || babyTrollSpawnPoints.Length == 0) return;

        Transform spawnPoint = babyTrollSpawnPoints[Random.Range(0, babyTrollSpawnPoints.Length)];
        Instantiate(babyTrollPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}