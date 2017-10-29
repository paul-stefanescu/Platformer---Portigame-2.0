using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall2 : MonoBehaviour
{
    public float ballSpeed;
    private Rigidbody2D theRB;
    public GameObject fireballEffect;
    private float angle = 0;
    private float xAngle;
    private float yAngle;
    // Use this for initialization
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        if (Player2Control.rb2d.rotation != 0)
        {
            xAngle = Mathf.Cos(Mathf.Deg2Rad * Player2Control.rb2d.rotation);
            yAngle = Mathf.Sin(Mathf.Deg2Rad * Player2Control.rb2d.rotation);
        }
        else
        {
            if (Player2Control.lastMove == "left")
                xAngle = -1;
            else
                xAngle = 1;
            yAngle = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = new Vector2(ballSpeed * xAngle, ballSpeed * yAngle);
        angle += 10;
        theRB.MoveRotation(angle);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other == PlayerControl.bc2d)
        {
            Player2Control.power += 0.15f;
            PlayerControl.rb2d.velocity = new Vector2(ballSpeed * xAngle * Player2Control.power * 0.5f, 
                                                      ballSpeed * yAngle * Player2Control.power * 0.5f);
            PlayerControl.hit = true;
        }
        if (other != Player2Control.bc2d)
        { 
            Destroy(gameObject); //distruge fireballul
            Instantiate(fireballEffect, transform.position, transform.rotation); //apare efectul
        }
    }
}
