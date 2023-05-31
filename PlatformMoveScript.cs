using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Skript för plattforms prefaben att röra sig
public class PlatformMoveScript : MonoBehaviour
{
    public float moveSpeed = 2;
    private GameObject Lava;

    // Start is called before the first frame update
    void Start()
    {
        // Hittar gameObject som Lavacontroller skriptet är kopplat till, så att detta skript kan jämföra dess positions värden, samma sak som att göra en public referens fast 
        // man gör det i själva koden
        Lava = FindObjectOfType<LavaController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //Positionen på plattform rör sig ner mer en hastighet av moveSpeed som uppdateras efter en tidsintervall och inte per frame, vilket skulle ändra hastigheten beroende på hur många frames man har
        transform.position = transform.position + (Vector3.down * moveSpeed) * Time.deltaTime;

        // ifall plattformens position är lika eller mindre än lavans position så försvinner plattformen. Detta görs för att reducera onödigt lagg. 
        if (transform.position.y <= Lava.transform.position.y)
        {
            Destroy(gameObject);
        }
    }
}
