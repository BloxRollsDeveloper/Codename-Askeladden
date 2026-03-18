using UnityEngine;
using System.Collections;

public class BoulderRoll : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float stepInterval = 0.2f;

    private float targetX;
    private float direction;

    public void Roll(float target, float dir)
    {
        targetX = target;
        direction = dir; // 1 = roll to the right, -1 = roll to the left
        StartCoroutine(RollBoulder());
    }

    IEnumerator RollBoulder()
    {
        while (Mathf.Sign(targetX - transform.position.x) == direction)
        {
            transform.position += new Vector3(moveSpeed * stepInterval * direction, 0f, 0f);
            transform.Rotate(0f, 0f, -45f * direction); // Rotates boulder by 45 degrees at an instance
            yield return new WaitForSeconds(stepInterval);
        }
        
        Destroy(gameObject); // Deletes Boulder
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Boulder Hit");
        }
    }
}
