using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Control : MonoBehaviour
{

    public float jumpForce = 250f;
    public float moveSpeed = 3f;

    public static KeyCode leftKey = KeyCode.K;
    public static KeyCode rightKey = KeyCode.Semicolon;
    public static KeyCode jumpKey = KeyCode.O;
    public static KeyCode fireKey = KeyCode.Slash;

    public static Rigidbody2D rb2d;
    public static BoxCollider2D bc2d;
    public static string lastMove = "any";
    public static float power = 0.5f;
    public static float minpower = 0.5f;
    public float powerCopy;
    public static bool hit = false;
    public AudioClip shoot;

    private bool isDead = false;
    private int jumpCount = 0;
    private string lastDir;
    private bool rotate = false;
    private float degrees = 0f;
    private float hitTick = 0f;
    private float timer = 0f;

    public GameObject fireBall;
    public Transform throwPointRight;
    public Transform throwPointLeft;

    private Animator anim;

    public bool isGrounded = false;
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
            if (bc2d.IsTouchingLayers(1 << 8)) // vertical velocity is 0 ( GroundLayer is touched )
            {
                jumpCount = 0; // player can jump again
                isGrounded = true;
            }
            // rotate 360
            if (rotate && degrees < 360)
            {
                degrees += 15f;
                rb2d.MoveRotation(degrees);
            }
            else if (rotate && degrees == 360) // rotate complete
            {
                degrees = 0f;
                rotate = false;
                rb2d.rotation = 0;
            }
            if (!rotate)
                rb2d.rotation = 0;

            // if any key is pressed
            if (Input.anyKey)
            {
                // if space is pressed JUMP ( only twice )
                if (Input.GetKeyDown(jumpKey) && jumpCount < 1)
                {
                    jumpCount++; // count jumps
                    isGrounded = false;
                    rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                    rb2d.AddForce(new Vector2(rb2d.velocity.x, jumpForce)); // add force ( the second jump has a lot higher velocity
                    if (jumpCount == 1 && !bc2d.IsTouchingLayers(1 << 8)) // spin at second jump
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
                    if (!Input.GetKey(leftKey) && !Input.GetKey(rightKey))
                        rb2d.velocity = new Vector2(0f, rb2d.velocity.y);
                    if (Input.GetKey(rightKey) && (lastDir == "right" || lastDir == "any"))
                    {
                        rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
                        lastMove = "right";
                    }
                    if (Input.GetKey(leftKey) && (lastDir == "left" || lastDir == "any"))
                    {
                        rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
                        lastMove = "left";
                    }

                    if (Input.GetKeyDown(fireKey) && timer < 0f)
                    {
                        AudioSource sound = gameObject.GetComponent<AudioSource>();
                        sound.Play();
                        if (lastMove == "right" || lastMove == "any")
                            Instantiate(fireBall, throwPointRight.position, throwPointRight.rotation);
                        else
                            Instantiate(fireBall, throwPointLeft.position, throwPointLeft.rotation);
                        anim.SetTrigger(name: "Throw");
                        timer = 0.5f;
                    }
                }
            }
            else // no key pressed
            {
                if(!hit)
                    rb2d.velocity = new Vector2(0f, rb2d.velocity.y);
            }
            anim.SetTrigger("Throw");
            timer -= Time.deltaTime;
        }
        anim.SetFloat("Speed",rb2d.velocity.x);
        //anim.SetBool("Grounded", isGrounded);
        powerCopy = power;
    }
}
