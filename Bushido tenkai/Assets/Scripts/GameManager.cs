using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Update(){
        if (Input.GetKeyDown(KeyCode.Space)) //si oprimo espacio, abro menu
        {
            HandleGameplayState();
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) //si oprimo
        {
            HandleInitialmenuState();
        }
    }
// dentro del juego se tienen 4 estados:
// el estado para cambiar el personaje
    //void HandleCharacterState{
    
    
// el estado del menu inicial (donde sale la interfaz principal y la de opciones)

// el estado para jugar el round
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
// el estado de muerte del personaje
    // void HandleWinnerState{

    

 //otros estados
    // void LoadGame{

    

    // void SaveGame{

    

}
