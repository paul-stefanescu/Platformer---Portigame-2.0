using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float ballSpeed;
    private Rigidbody2D theRB;
    public GameObject fireballEffect;
    private float xAngle;
    private float yAngle;
    // Use this for initialization
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        if (PlayerControl.rb2d.rotation != 0)
        {
            xAngle = Mathf.Cos(Mathf.Deg2Rad * PlayerControl.rb2d.rotation);
            yAngle = Mathf.Sin(Mathf.Deg2Rad * PlayerControl.rb2d.rotation);
        }
        else
        {
            if (PlayerControl.lastMove == "left")
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

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other == Player2Control.bc2d)
        {
            PlayerControl.power += 0.15f;
            Player2Control.rb2d.velocity = new Vector2(ballSpeed * xAngle * PlayerControl.power * 0.5f,
                                                      ballSpeed * yAngle * PlayerControl.power * 0.5f);
            Player2Control.hit = true;
        }
        if (other != PlayerControl.bc2d)
        {
            Destroy(gameObject); //distruge fireballul
            Instantiate(fireballEffect, transform.position, transform.rotation); //apare efectul
        }
    }
}
