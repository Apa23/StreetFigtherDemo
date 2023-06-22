using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class InitialMenu : MonoBehaviour
{
    
    public void Jugar()
    {
        GameManager.Instance.GoToSelect();
        //Acá toca poner el nombre de la escena de elegir jugador, aun en construccion

    }

    // Update is called once per frame
    public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }
}
