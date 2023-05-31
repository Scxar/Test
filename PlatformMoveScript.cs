using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Skript f�r plattforms prefaben att r�ra sig
public class PlatformMoveScript : MonoBehaviour
{
    public float moveSpeed = 2;
    private GameObject Lava;

    // Start is called before the first frame update
    void Start()
    {
        // Hittar gameObject som Lavacontroller skriptet �r kopplat till, s� att detta skript kan j�mf�ra dess positions v�rden, samma sak som att g�ra en public referens fast 
        // man g�r det i sj�lva koden
        Lava = FindObjectOfType<LavaController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //Positionen p� plattform r�r sig ner mer en hastighet av moveSpeed som uppdateras efter en tidsintervall och inte per frame, vilket skulle �ndra hastigheten beroende p� hur m�nga frames man har
        transform.position = transform.position + (Vector3.down * moveSpeed) * Time.deltaTime;

        // ifall plattformens position �r lika eller mindre �n lavans position s� f�rsvinner plattformen. Detta g�rs f�r att reducera on�digt lagg. 
        if (transform.position.y <= Lava.transform.position.y)
        {
            Destroy(gameObject);
        }
    }
}
