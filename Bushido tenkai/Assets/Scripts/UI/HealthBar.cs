using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    // UI variables
    public Slider HealthPlayer1;
    public Slider HealthPlayer2;
    public GameObject WinnerScreen;

    private void OnEnable() {
        // Set up winner screen
        GameManager.WinnerScreen = WinnerScreen; 
        // Set up health bars
        GameManager.HealthPlayer1 = HealthPlayer1;
        GameManager.HealthPlayer2 = HealthPlayer2;
        
    }
}