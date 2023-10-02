using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] int targetSceneIndex = 1;
    [SerializeField] Vector2 spawnPos;
    [SerializeField] int facingDirection;
    public PlayerDataStorage playerData;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            playerData.initialPos = spawnPos;
            playerData.initialFacingDirection = facingDirection;

            playerData.initialHealth = collision.gameObject.GetComponent<PlayerStats>().GetHealth();
     
            SceneManager.LoadScene(targetSceneIndex);
        }
        
    }

}
