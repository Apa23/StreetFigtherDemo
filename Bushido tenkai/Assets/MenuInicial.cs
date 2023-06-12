using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Acá toca poner el nombre de la escena de elegir jugador


public class MenuInicial : MonoBehaviour
{
    // Start is called before the first frame update
    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //Acá toca poner el nombre de la escena de elegir jugador
    }

    // Update is called once per frame
    public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }
}
