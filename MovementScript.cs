using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Skript f�r karakt�rens r�relse funktioner
public class MovementScript : MonoBehaviour
{
    // Olika float v�rden, referens till spelets kamera, referenser till spelobjekt i Unity
    public float horizontal;
    public float speed = 8f;
    public float jumpStrength = 16f;
    private bool isFacingRight = true;
    public Camera mainCamera;
    public TimerScript logic;

    //Bool ger antingen ett True eller False v�rde och anv�nds h�r f�r att se ifall karakt�ren lever eller inte
    public bool isAlive = true;
    public GameObject lava;
    public GameObject background;

    //Public referens till karakt�rens Rigidbody, referens till positionen av groundCheck som finns vid karakt�rens botten och �r till f�r att se ifall karakt�ren st�r p� n�got eller inte.
    //Layermask g�r ett referens till ett layer i Unity som plattformarna ocks� har
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;


    private void Start()
    {
        // Hittar Timer skriptet som �r taggat med Logic f�r att kunna ta funktioner fr�n skripet till detta skript
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<TimerScript>();
    }
    // Update is called once per frame
    void Update()
    {
        //G�r s� att man kan r�ra sig genom att anv�nda A,D och pilarna till h�ger och v�nster.
        horizontal = Input.GetAxisRaw("Horizontal");
        
        //Ifall man trycker spacebar och karakt�ren st�r p� en plattform s� f�r karakt�rens rigidbody samma hastighet p� x-axelns men jumpStrenght v�rdet som hastiget upp�t p� y-axeln
        // Karakt�ren beh�ver �ven ha sin groundCheck som placerades i Unity vid l�ngst ner att r�ra plattformen f�r att man ska kunna hoppa
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
        }

        //Ifall spacebar �r nertryckt och rigidbody hastighet p� y-axelns �r �ver 0 s� saktas hopp hastighetet ner med 0,5 varje frame.
        //Detta g�r att man fortfarande r�r sig lite h�gre ifall man h�ller ner knappen och hoppar d� kortare ifall man endast trycker
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        //Kamerans position = cameraPos. cameraPos position uppdateras hela tiden med samma v�rde som karakt�rens position s� att den f�ljer med karakt�ren p� x- och y-axeln.
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.x = transform.position.x;
        cameraPos.y = transform.position.y;
        //Uppdaterar cameraPos position till dens nya s� att den j�mf�r n�sta uppdatering/frame med den nya positionen och inte den gamla
        mainCamera.transform.position = cameraPos;

        //Samma sak som kameran fast backgrunden s� att den f�ljer med karakt�ren och alltid syns i sin helhet
        Vector3 backGroundPos = background.transform.position;
        backGroundPos.x = transform.position.x;
        backGroundPos.y = transform.position.y;
        background.transform.position = backGroundPos;
    }
    //En update funktion som �r fixerad p� ett och samma v�rde och inte p� tex time.Deltatime som andra funktioner g�rs p�
    private void FixedUpdate()
    {
        //ifall karakt�ren lever s� k�rs Flip() funktionen och hastigheten p� rigidbody av karakt�ren �r speed p� x-axeln och of�r�ndrad p� y-axeln.
        if (isAlive == true)
        {
            Flip();
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }

        //Om karakt�ren inte lever s� �r dens hastighet 0 p� x- och y-axelns s� att man inte kan r�ra sig ifall man d�r
        else
        {
            rb.velocity = new Vector2(0,0);
        }
    }

    private bool isGrounded()
    {
        //Skapar en onsynlig cirkel vid karakt�rens f�tter som som j�mf�r karakt�ren groundCheck hitbox med plattformens groundLayer.
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        //ifall karakt�rens horizontal �r mindre �n noll, allts� r�r sig v�nster s� flipar karakt�ren �t det h�ller, eller ifall den kollar v�nster och r�r sig h�ger s� flipar den tillbaka
        //Detta beh�vdes inte till slut d� vi inte gick med att person som karakt�r men syns �nd� i spelet
        if (horizontal > 0 && isFacingRight || horizontal < 0 && !isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    // ifall en kollision sker med karakt�ren och gameObject lava, s� k�rs gameOverScreen funktion fr�n Timer skriptet och karakt�rens isAlive v�rde �ndras till false
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            logic.gameOverScreen();
            isAlive = false;
        }
    }   
}