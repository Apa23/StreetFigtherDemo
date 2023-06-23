using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static GameObject WinnerScreen;
    public static int player1CharacterIndex = 0;
    public static int player2CharacterIndex = 3;

    public static int Player1Health = 0;
    public static int Player2Health = 0;


    private void Awake()
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

    public void setInitialHealth()
    {
        gameObject.GetComponent<HealthBar>().SetMaxHealth1(Player1Health);
        gameObject.GetComponent<HealthBar>().SetMaxHealth2(Player2Health);
    }

    public void ChangeHealth(int health, string player)
    {
        gameObject.GetComponent<HealthBar>().SetHealth(health, player);
    }

    public void SetWinner(string player){
        WinnerScreen.GetComponent<WinnerScreen>().winner = player;
        WinnerScreen.SetActive(true);
    }

    void HandleGameplayState()
    {
        SceneManager.LoadScene("GameScreen");
    }

    void HandleInitialmenuState()
    {
        SceneManager.LoadScene("InitialMenu");
    }

    void HandleSelectionState()
    {
        SceneManager.LoadScene("CharacterSelection");
    }




}
