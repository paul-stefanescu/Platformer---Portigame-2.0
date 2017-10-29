using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public void NewGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Rock");
    }

    public void Options()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Controls");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
