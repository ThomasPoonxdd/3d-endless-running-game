using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class Events : MonoBehaviour
{

    public void ReplayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
