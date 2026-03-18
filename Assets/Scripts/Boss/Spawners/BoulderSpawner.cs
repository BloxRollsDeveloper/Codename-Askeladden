using UnityEngine;

public class BoulderSpawner : MonoBehaviour
{
    [SerializeField] private GameObject boulderPrefab;
    [SerializeField] private Transform spawnPointLeft;
    [SerializeField] private Transform spawnPointRight;

    public void Launch()
    {
        // Randomly picks a side
        bool fromLeft = Random.value > 0.5f;
        Transform spawnPoint = fromLeft ? spawnPointLeft : spawnPointRight;
        float direction = fromLeft ? 1f : -1f;
        float targetX = fromLeft ? spawnPointRight.position.x : spawnPointLeft.position.x;
        
        GameObject boulder = Instantiate(boulderPrefab, spawnPoint.position, Quaternion.identity);
        boulder.GetComponent<BoulderRoll>().Roll(targetX, direction);
    }
}
