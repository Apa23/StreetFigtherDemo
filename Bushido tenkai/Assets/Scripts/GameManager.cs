using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static int player1CharacterIndex;
    public static int player2CharacterIndex;
    

    private void Awake(){
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
    public void GoToMenu(){
        Debug.Log("entrex2");
        HandleInitialmenuState();
    }

    public void GoToGame()
    {
        HandleGameplayState();
    }
    void HandleGameplayState()
    {
        Debug.Log(message: "Cargando juego...");
        SceneManager.LoadScene("GameScreen");
    }

    void HandleInitialmenuState()
    {
        Debug.Log(message: "Cargando menú...");
        SceneManager.LoadScene("MenuInicial");
    }

    void HandleSelectionState()
    {
        Debug.Log(message: "Cargando selección de personajes...");
        SceneManager.LoadScene("CharacterSelection");
    }

    

}
