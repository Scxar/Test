using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Skript för karaktärens rörelse funktioner
public class MovementScript : MonoBehaviour
{
    // Olika float värden, referens till spelets kamera, referenser till spelobjekt i Unity
    public float horizontal;
    public float speed = 8f;
    public float jumpStrength = 16f;
    private bool isFacingRight = true;
    public Camera mainCamera;
    public TimerScript logic;

    //Bool ger antingen ett True eller False värde och används här för att se ifall karaktären lever eller inte
    public bool isAlive = true;
    public GameObject lava;
    public GameObject background;

    //Public referens till karaktärens Rigidbody, referens till positionen av groundCheck som finns vid karaktärens botten och är till för att se ifall karaktären står på något eller inte.
    //Layermask gör ett referens till ett layer i Unity som plattformarna också har
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;


    private void Start()
    {
        // Hittar Timer skriptet som är taggat med Logic för att kunna ta funktioner från skripet till detta skript
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<TimerScript>();
    }
    // Update is called once per frame
    void Update()
    {
        //Gör så att man kan röra sig genom att använda A,D och pilarna till höger och vänster.
        horizontal = Input.GetAxisRaw("Horizontal");
        
        //Ifall man trycker spacebar och karaktären står på en plattform så får karaktärens rigidbody samma hastighet på x-axelns men jumpStrenght värdet som hastiget uppåt på y-axeln
        // Karaktären behöver även ha sin groundCheck som placerades i Unity vid längst ner att röra plattformen för att man ska kunna hoppa
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
        }

        //Ifall spacebar är nertryckt och rigidbody hastighet på y-axelns är över 0 så saktas hopp hastighetet ner med 0,5 varje frame.
        //Detta gör att man fortfarande rör sig lite högre ifall man håller ner knappen och hoppar då kortare ifall man endast trycker
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        //Kamerans position = cameraPos. cameraPos position uppdateras hela tiden med samma värde som karaktärens position så att den följer med karaktären på x- och y-axeln.
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.x = transform.position.x;
        cameraPos.y = transform.position.y;
        //Uppdaterar cameraPos position till dens nya så att den jämför nästa uppdatering/frame med den nya positionen och inte den gamla
        mainCamera.transform.position = cameraPos;

        //Samma sak som kameran fast backgrunden så att den följer med karaktären och alltid syns i sin helhet
        Vector3 backGroundPos = background.transform.position;
        backGroundPos.x = transform.position.x;
        backGroundPos.y = transform.position.y;
        background.transform.position = backGroundPos;
    }
    //En update funktion som är fixerad på ett och samma värde och inte på tex time.Deltatime som andra funktioner görs på
    private void FixedUpdate()
    {
        //ifall karaktären lever så körs Flip() funktionen och hastigheten på rigidbody av karaktären är speed på x-axeln och oförändrad på y-axeln.
        if (isAlive == true)
        {
            Flip();
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }

        //Om karaktären inte lever så är dens hastighet 0 på x- och y-axelns så att man inte kan röra sig ifall man dör
        else
        {
            rb.velocity = new Vector2(0,0);
        }
    }

    private bool isGrounded()
    {
        //Skapar en onsynlig cirkel vid karaktärens fötter som som jämför karaktären groundCheck hitbox med plattformens groundLayer.
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        //ifall karaktärens horizontal är mindre än noll, alltså rör sig vänster så flipar karaktären åt det håller, eller ifall den kollar vänster och rör sig höger så flipar den tillbaka
        //Detta behövdes inte till slut då vi inte gick med att person som karaktär men syns ändå i spelet
        if (horizontal > 0 && isFacingRight || horizontal < 0 && !isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    // ifall en kollision sker med karaktären och gameObject lava, så körs gameOverScreen funktion från Timer skriptet och karaktärens isAlive värde ändras till false
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            logic.gameOverScreen();
            isAlive = false;
        }
    }   
}