using UnityEngine;

public class BabyTrollSpawner : MonoBehaviour
{
    [SerializeField] private GameObject babyTrollPrefab;
    [SerializeField] private Transform[] spawnPoints;
    public static void Spawn()
    {
        Debug.Log("Spawned BabyTrolls");
    }
}
