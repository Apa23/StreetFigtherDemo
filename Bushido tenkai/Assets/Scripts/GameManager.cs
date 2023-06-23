using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static int player1CharacterIndex = 0;
    public static int player2CharacterIndex = 3;


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
