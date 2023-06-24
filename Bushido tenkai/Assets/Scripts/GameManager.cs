using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Objects variables
    public static GameManager Instance;
    public static GameObject WinnerScreen;
    public static Slider HealthPlayer1;
    public static Slider HealthPlayer2;

    // Player and character variables
    public static int player1CharacterIndex = 0;
    public static int player2CharacterIndex = 3;
    public static int Player1Health = 0;
    public static int Player2Health = 0;


    private void Awake() // Set up instance
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GoToSelect()
    {
        HandleSelectionState();
    }
    public void GoToMenu()
    {
        HandleInitialmenuState();
    }

    public void GoToGame()
    {
        HandleGameplayState();

    }

    public void setInitialHealth() // Set initial values on health bars
    {
        HealthPlayer1.maxValue = Player1Health;
        HealthPlayer1.value = Player1Health;
        HealthPlayer2.maxValue = Player2Health;
        HealthPlayer2.value = Player2Health;
    }

    public void ChangeHealth(int health, string player) // Update health bars values
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

    public void SetWinner(string player) // Set the winner name and show winner screen
    {
        WinnerScreen.GetComponent<WinnerScreen>().winner = player;
        WinnerScreen.SetActive(true);
    }

    void HandleGameplayState() // Go to game screen
    {
        SceneManager.LoadScene("GameScreen");
    }

    void HandleInitialmenuState() // Go to main menu screen
    {
        SceneManager.LoadScene("InitialMenu");
    }

    void HandleSelectionState() // Go to character selection screen
    {
        SceneManager.LoadScene("CharacterSelection");
    }




}
