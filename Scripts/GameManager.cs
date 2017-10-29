using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    public int P1Life;
    public int P2Life;

    public Text OverText;
    public GameObject gameOver;

    private int cnt;

    //public GameObject txtObj;
    // Use this for initialization
    void Start()
    {
        cnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //txtObj = GameObject.Find("OverText");
        if (P1Life <= 0)
        {
            //player1.SetActive(false);
            gameOver.SetActive(true);
            //txtObj.SetActive(true);
            OverText.text = "Player 1 Wins!";
            cnt++;
            if (cnt >= 70)
                Time.timeScale = 0;

        }
        if (P2Life <= 0)
        {
            //player2.SetActive(false);
            gameOver.SetActive(true);
            //txtObj.SetActive(true);
            OverText.text = "Player 2 Wins!";
            cnt++;
            if (cnt >= 70)
                Time.timeScale = 0;
        }
    }

    public void HurtP1()
    {
        P1Life -= 1;
    }
    public void HurtP2()
    {
        P2Life -= 1;
    }
}
