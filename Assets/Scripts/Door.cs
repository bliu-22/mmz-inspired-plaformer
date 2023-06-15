using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] int targetSceneIndex = 0;
    [SerializeField] Vector2 spawnPos;
    [SerializeField] int facingDirection;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            RoomSwitch.SpawnPos = spawnPos;
            RoomSwitch.FacingDirection = facingDirection;
            //var player = collision.gameObject.GetComponent<PlayerStats>();
            //RoomSwitch.SpawnHealth = player.GetHealth();
            SceneManager.LoadScene(targetSceneIndex);
        }
        
    }

}
