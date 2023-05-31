using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Använder hära 2 andra Unity funktioner som har med UI och Scenemanagement att göra, behövs för att öppna upp fler kommandon relaterat till de
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    private float startTime;
    public Text timerText;
    public GameObject gameOver;
    private bool gameActive = true;

    void Start()
    {
        timerText = GameObject.Find("Text").GetComponent<Text>();
        startTime = Time.time;
    }
  
    public void Update()
    {
        if (gameActive)
        {
            //räknar tiden som har gått sedan spelet starta
            float elapsedTime = Time.time - startTime;

            // gör string värden som är text till sekunder och minuter och gör om deras float värden till text
            string minutes = Mathf.Floor(elapsedTime / 60).ToString("00");
            string seconds = Mathf.Floor(elapsedTime % 60).ToString("00");

            //Texten som visas i spelet visar Runtime och sedan minuter/sekunder efter spelet har startats
            timerText.text = "Runtime: " + minutes + ":" + seconds;
        }
    }
    // Hämmtar scenen som är kopplat till en knapp som startar om spelet
    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //Sätter på gameobject för Gameover UI som tidigare doldes och ändrar värdet för GameActive till false.
    public void gameOverScreen()
    {
        gameOver.SetActive(true);
        gameActive = false;
    }
}
