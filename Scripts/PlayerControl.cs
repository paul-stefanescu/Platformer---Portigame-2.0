using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float jumpForce = 250f;
    public float moveSpeed = 3f;

    public static KeyCode leftKey = KeyCode.A;
    public static KeyCode rightKey = KeyCode.D;
    public static KeyCode jumpKey = KeyCode.W;
    public static KeyCode fireKey = KeyCode.C;
    
    public static Rigidbody2D rb2d;
    public static string lastMove = "any";
    public static float power = 0.5f;
    public float minpower = 0.5f;
    public float powerCopy;
    public static bool hit = false;
    public AudioClip shoot;

    private bool isDead = false;
    public static BoxCollider2D bc2d;
    private int jumpCount = 0;
    private string lastDir;
    private bool rotate = false;
    private float degrees = 0f;
    private float hitTick = 0;
    private float timer = 0f;
   

    public GameObject fireBall;
    public Transform throwPointRight;
    public Transform throwPointLeft;

    private Animator anim;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        // if player is alive
        if (!isDead)
        {
            if (hit)
                hitTick += Time.deltaTime;
            if (hit && hitTick >= 0.5f)
            {
                hit = false;
                hitTick = 0;
            }
            if (bc2d.IsTouchingLayers(1<<8)) // vertical velocity is 0 ( GroundLayer is touched )
                jumpCount = 0; // player can jump again
            // rotate 360
            if (rotate && degrees < 720)
            {
                degrees += 15f;
                rb2d.MoveRotation(degrees);
            }
            else if (rotate && degrees == 720) // rotate complete
            {
                degrees = 0f;
                rotate = false;
                rb2d.rotation = 0;
            }
            if(!rotate)
                rb2d.rotation = 0;

            // if any key is pressed
            if (Input.anyKey)
            {
                // if space is pressed JUMP ( only twice )
                if (Input.GetKeyDown(jumpKey) && jumpCount < 1)
                {
                    
                    jumpCount++; // count jumps
                    rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                    rb2d.AddForce(new Vector2(rb2d.velocity.x, jumpForce)); // add force ( the second jump has a lot higher velocity
                    if(jumpCount == 1 && !bc2d.IsTouchingLayers(1 << 8)) // spin at second jump
                    {
                        rb2d.AddForce(new Vector2(rb2d.velocity.x, jumpForce * 0.3f));
                        rotate = true;
                    }
                }
                if (!hit)
                {

                    // left and right movement
                    if (Input.GetKeyDown(rightKey))
                        lastDir = "right";
                    if (Input.GetKeyDown(leftKey))
                        lastDir = "left";
                    if (Input.GetKeyUp(rightKey) && lastDir == "right")
                        lastDir = "any";
                    if (Input.GetKeyUp(leftKey) && lastDir == "left")
                        lastDir = "any";
                    if (!Input.GetKey(rightKey) && !Input.GetKey(leftKey))
                        rb2d.velocity = new Vector2(0f, rb2d.velocity.y);
                    if (Input.GetKey(rightKey) && (lastDir == "right" || lastDir == "any"))
                    {
                        rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
                        lastMove = "right";
                        anim.SetFloat("LastMove", 1f);
                    }
                    if (Input.GetKey(leftKey) && (lastDir == "left" || lastDir == "any"))
                    {
                        rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
                        lastMove = "left";
                        anim.SetFloat("LastMove", -1f);
                    }

                    
                    if (Input.GetKeyDown(fireKey) && timer < 0f)
                    {
                        AudioSource sound = gameObject.GetComponent<AudioSource>();
                        sound.Play();
                        if (lastMove == "right" || lastMove == "any")
                            Instantiate(fireBall, throwPointRight.position, throwPointRight.rotation);
                        else
                            Instantiate(fireBall, throwPointLeft.position, throwPointLeft.rotation);
                        timer = 0.3f;
                    }
                }
            }
            else // no key pressed
            {
                if(!hit)
                    rb2d.velocity = new Vector2(0f, rb2d.velocity.y);
            }
            anim.SetFloat("LastMove", rb2d.velocity.x);
            powerCopy = power;
            power = powerCopy;
            if (timer >= 0f)
                timer -= Time.deltaTime;
        }
    }
    /*void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "EndZone")
        {
            FindObjectOfType<GameManager>().HurtP1();
        }
        
    }*/
}
