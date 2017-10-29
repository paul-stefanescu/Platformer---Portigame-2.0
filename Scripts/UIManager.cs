using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    //public Text ScoreText;
    public Slider healthBar1;
    public Slider healthBar2;
    public Text powerText1;
    public Text powerText2;
    //public PlayerHealthManager playerHealth;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        healthBar1.maxValue = 6f;
        healthBar2.maxValue = 6f;
        healthBar1.value = PlayerControl.power;
        healthBar2.value = Player2Control.power;
        powerText1.text = "Player 1: ";// + player1.powerCopy;
        powerText2.text = "Player 2: ";// + player2.powerCopy;
    }
}
