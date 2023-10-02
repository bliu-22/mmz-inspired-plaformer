using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI;
    [SerializeField] PlayerDataStorage playerData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void restartGame()
    {
        playerData.ResetToDefault();
        SceneManager.LoadScene(1);
        
    }

    public void ReturnToMenu() 
    {
        SceneManager.LoadScene(0);
    }

    public void triggerGameOver() 
    {
        gameOverUI.SetActive(true);
        
    }
    public void Quit() 
    {
        Application.Quit();
    }
}
