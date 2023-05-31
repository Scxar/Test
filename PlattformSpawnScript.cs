using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Skript f�r att spawna plattformar, kopplas till en prefab av plattformen
public class PlattformSpawnScript : MonoBehaviour
{
    // public GameObject skapar en referens till ett spelobjekt som syns i Unity och anv�nds f�r att dra ett spelobjekt till referensen s� dess v�rden som tex positions v�rde kan anv�ndas i skriptet
    // float ger ett v�rde p� ett namn, g�r man den public kan v�rdet �ndras i Unity och sparas f�r att l�ttare kunna speltesta och �ndra v�rden utan att g� in i koden
    public GameObject Platformar;
    public float spawnYDistance = 4f;
    public float widthOffset = 10;

    // private Vector 3 g�r en icke synlig Vector3 variabel, som definerar positionen f�r variabeln p� x-, y och z-axeln. Z axeln anv�nds inte i 2d development.
    private Vector3 lastSpawnedPosition = new Vector3(0f, 0f, 0f);
    private GameObject lastSpawnedPlatform;
    public GameObject PlatformSpawner;
    public float minimumXDistance = 0f;

    // Start is called before the first frame update
    void Update()
    {
        // K�r spawnPlatformar funktion varje frame
        spawnPlatformar();
    }

    // Update is called once per frame

    // En funktion som inte returnerar n�got v�rde, utan utf�r endast koden inom parametrarna
    void spawnPlatformar()
    {
        // Definerar punkten l�ngst till h�ger och punkten l�ngst till v�nster som plattformen kan spawna p� l�ngst x-axeln.
        float leftestPoint = PlatformSpawner.transform.position.x - widthOffset;
        float rightestPoint = PlatformSpawner.transform.position.x + widthOffset;

        // Ifall det inte finns en referens till en plattform eller distansen mellan den senaste spawnade plattformens position och den n�sta som ska spawna = eller �verstiger spawnYDistance s� k�rs koden
        if (lastSpawnedPlatform == null || Vector3.Distance(lastSpawnedPlatform.transform.position, lastSpawnedPosition) >= spawnYDistance)
        {
            // En referens som best�r av en random.range vilket inkluderar alla x positioner mellan den punkten l�ngst �t h�ger och �t v�nster.
            float spawnXPosition = Random.Range(leftestPoint, rightestPoint);

            // Positionen av spawnPosition �r den f�rra platformen som spawnade + spawnYDistance som ger d� ett regelbundet mellanrum p� y-axeln och + en random v�rde p� x-axeln
            Vector3 spawnPosition = lastSpawnedPosition + new Vector3(0f, spawnYDistance, 0f) + new Vector3(spawnXPosition, 0f, 0f);

            // Variable f�r den platformen som spawnas med hj�lp av Instantiate som skapar kopior av newPlatform medan spelet k�rs i Unity d�r den spawnar en prefab Platformar som refereras h�gst upp o koden, ger den spawnPosition d�r den kommer spawnas och som beh�ller samma rotation.
            GameObject newPlatform = Instantiate(Platformar, spawnPosition, transform.rotation);

            // St�ller om v�rden p� lastSpawnedPlatform till den senaste newPLatform och spawnPosition s� att den n�sta plattform j�mf�r sina v�rden med den nya och inte den gamla,
            // d� det skulle spawna plattformer konstant utan mellanrum p� y-axeln
            lastSpawnedPlatform = newPlatform;
            lastSpawnedPosition = spawnPosition;
        }
    }
}