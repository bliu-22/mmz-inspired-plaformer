using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    [SerializeField] PlayerDataStorage playerData;
    // Start is called before the first frame update


    public void StartGame()
    {
        playerData.ResetToDefault();
        SceneManager.LoadScene(1);

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
