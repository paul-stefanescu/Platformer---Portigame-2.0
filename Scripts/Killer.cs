using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public Vector3 pozPaul = new Vector3(-1.39f,-2.13f,0f);
    public Vector3 pozIoana = new Vector3(0.69f, -2.11f, 0f);
    public GameObject ceva;
    public GameObject cevaI;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Paul")
        {
            FindObjectOfType<GameManager>().HurtP1();
            if (FindObjectOfType<GameManager>().P1Life > 0)
            {
                Destroy(other.gameObject);
                ceva = (GameObject)Instantiate(Resources.Load("Paul"), pozPaul, Quaternion.identity);
                PlayerControl.power = 0.5f;
                Player2Control.power = 0.5f;
                ceva.name = "Paul";
            }

        }
        else
        {
            FindObjectOfType<GameManager>().HurtP2();
            if (FindObjectOfType<GameManager>().P1Life > 0)
            {
                Destroy(other.gameObject);
                cevaI = (GameObject)Instantiate(Resources.Load("Ioana"), pozIoana, Quaternion.identity);
                cevaI.name = "Ioana";
                Player2Control.power = 0.85f;
                PlayerControl.power = 0.5f;
            }

        }



    }
}
