using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int score = 0;
    GUIStyle myStyle = new GUIStyle();
    [SerializeField] PlayerMovement playerMovement;

    //public static bool gameOver;
    public GameObject gameOverPanel;
    public GameObject startingText;

    public static GameManager inst;

    private bool GameStarted;

    public static bool isGameStarted()
    {
        return inst.GameStarted;
    }

    public void IncrementScore() 
    {
        score++;
        playerMovement.IncrementSpeed();

    }

    private void Awake() 
    {
        inst = this;
        gameOverPanel.SetActive(false);
        GameStarted = false;
        Time.timeScale = 1;
    }

    void Update() 
    {
        if (!playerMovement.IsAlive()) 
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }

        if (SwipeManager.tap) 
        {
            GameStarted = true;
            Destroy(startingText);
        }

    }
 
    void OnGUI()
    {
        if (playerMovement.IsAlive() & GameStarted)
        {
            myStyle.fontSize = 30;
            GUI.Label(new Rect(20, 0, 1000, 1000), "Score: " + score, myStyle);
        }
    }

}
