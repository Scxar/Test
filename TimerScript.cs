using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Anv�nder h�ra 2 andra Unity funktioner som har med UI och Scenemanagement att g�ra, beh�vs f�r att �ppna upp fler kommandon relaterat till de
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
            //r�knar tiden som har g�tt sedan spelet starta
            float elapsedTime = Time.time - startTime;

            // g�r string v�rden som �r text till sekunder och minuter och g�r om deras float v�rden till text
            string minutes = Mathf.Floor(elapsedTime / 60).ToString("00");
            string seconds = Mathf.Floor(elapsedTime % 60).ToString("00");

            //Texten som visas i spelet visar Runtime och sedan minuter/sekunder efter spelet har startats
            timerText.text = "Runtime: " + minutes + ":" + seconds;
        }
    }
    // H�mmtar scenen som �r kopplat till en knapp som startar om spelet
    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //S�tter p� gameobject f�r Gameover UI som tidigare doldes och �ndrar v�rdet f�r GameActive till false.
    public void gameOverScreen()
    {
        gameOver.SetActive(true);
        gameActive = false;
    }
}
