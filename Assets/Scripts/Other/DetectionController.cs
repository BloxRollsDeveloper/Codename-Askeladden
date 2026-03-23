using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DetectionController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("HuldraBattleScene");
            Debug.Log("Player Detected");
        }
    }
}
