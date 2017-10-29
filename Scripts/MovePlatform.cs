using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D platformBody;
    
    public float moveTime;
    private float i;
    private float direction = -1;
    // Use this for initialization
    void Start ()
    {
        platformBody = GetComponent<Rigidbody2D>();
        i = moveTime;
        platformBody.velocity = new Vector2(0, moveSpeed);
    }
	
	// Update is called once per frame
	void Update ()
    {
        i -= Time.deltaTime;
        //Debug.Log("i = " + i);
        if (i < 0)
        {
            platformBody.velocity = new Vector2(0, 0);
            platformBody.velocity = new Vector2(0, moveSpeed * direction);
            direction *= -1;
            i = moveTime;
        }
	}
}
