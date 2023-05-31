using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Skript för att spawna plattformar, kopplas till en prefab av plattformen
public class PlattformSpawnScript : MonoBehaviour
{
    // public GameObject skapar en referens till ett spelobjekt som syns i Unity och används för att dra ett spelobjekt till referensen så dess värden som tex positions värde kan användas i skriptet
    // float ger ett värde på ett namn, gör man den public kan värdet ändras i Unity och sparas för att lättare kunna speltesta och ändra värden utan att gå in i koden
    public GameObject Platformar;
    public float spawnYDistance = 4f;
    public float widthOffset = 10;

    // private Vector 3 gör en icke synlig Vector3 variabel, som definerar positionen för variabeln på x-, y och z-axeln. Z axeln används inte i 2d development.
    private Vector3 lastSpawnedPosition = new Vector3(0f, 0f, 0f);
    private GameObject lastSpawnedPlatform;
    public GameObject PlatformSpawner;
    public float minimumXDistance = 0f;

    // Start is called before the first frame update
    void Update()
    {
        // Kör spawnPlatformar funktion varje frame
        spawnPlatformar();
    }

    // Update is called once per frame

    // En funktion som inte returnerar något värde, utan utför endast koden inom parametrarna
    void spawnPlatformar()
    {
        // Definerar punkten längst till höger och punkten längst till vänster som plattformen kan spawna på längst x-axeln.
        float leftestPoint = PlatformSpawner.transform.position.x - widthOffset;
        float rightestPoint = PlatformSpawner.transform.position.x + widthOffset;

        // Ifall det inte finns en referens till en plattform eller distansen mellan den senaste spawnade plattformens position och den nästa som ska spawna = eller överstiger spawnYDistance så körs koden
        if (lastSpawnedPlatform == null || Vector3.Distance(lastSpawnedPlatform.transform.position, lastSpawnedPosition) >= spawnYDistance)
        {
            // En referens som består av en random.range vilket inkluderar alla x positioner mellan den punkten längst åt höger och åt vänster.
            float spawnXPosition = Random.Range(leftestPoint, rightestPoint);

            // Positionen av spawnPosition är den förra platformen som spawnade + spawnYDistance som ger då ett regelbundet mellanrum på y-axeln och + en random värde på x-axeln
            Vector3 spawnPosition = lastSpawnedPosition + new Vector3(0f, spawnYDistance, 0f) + new Vector3(spawnXPosition, 0f, 0f);

            // Variable för den platformen som spawnas med hjälp av Instantiate som skapar kopior av newPlatform medan spelet körs i Unity där den spawnar en prefab Platformar som refereras högst upp o koden, ger den spawnPosition där den kommer spawnas och som behåller samma rotation.
            GameObject newPlatform = Instantiate(Platformar, spawnPosition, transform.rotation);

            // Ställer om värden på lastSpawnedPlatform till den senaste newPLatform och spawnPosition så att den nästa plattform jämför sina värden med den nya och inte den gamla,
            // då det skulle spawna plattformer konstant utan mellanrum på y-axeln
            lastSpawnedPlatform = newPlatform;
            lastSpawnedPosition = spawnPosition;
        }
    }
}