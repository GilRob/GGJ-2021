using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{

    public void changeScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void endGame()
    {
        Application.Quit();
        Debug.Log("Game is closed");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void backToMain()
    {
        SceneManager.LoadScene("MenuFINAL");
    }

    public void playSound()
    {

    }
}
