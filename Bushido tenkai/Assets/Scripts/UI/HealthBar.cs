using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider HealthPlayer1;
    public Slider HealthPlayer2;

    public GameObject WinnerScreen;

    private void OnEnable() {
        GameManager.WinnerScreen = WinnerScreen;
    }

    public void SetMaxHealth1(int health)
    {
        HealthPlayer1.maxValue = health;
        HealthPlayer1.value = health;

    }
    public void SetMaxHealth2(int health)
    {
        HealthPlayer2.maxValue = health;
        HealthPlayer2.value = health;
    }

    public void SetHealth(int health, string player)
    {
        if (player == "Player1")
        {

            HealthPlayer1.value = health;
        }
        else if (player == "Player2")
        {

            HealthPlayer2.value = health;
        }

    }

}