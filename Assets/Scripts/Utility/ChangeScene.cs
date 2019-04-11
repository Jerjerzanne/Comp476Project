using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
    
    public void gameLevel()
    {
        SceneManager.LoadScene("Level");
    }
    public void exitGame()
    {
        Debug.Log("Player quit the game");
        Application.Quit();
    }

    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "GameOver")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("GameMenu");
            }
        }
    }

}
