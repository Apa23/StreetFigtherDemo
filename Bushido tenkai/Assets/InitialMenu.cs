using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;
//Acá toca poner el nombre de la escena de elegir jugador


public class InitialMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void Jugar()
    {
<<<<<<< HEAD:Bushido tenkai/Assets/InitialMenu.cs
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //Acá toca poner el nombre de la escena de elegir jugador, aun en construccion
=======
        //SceneManagement.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //Acá toca poner el nombre de la escena de elegir jugador
>>>>>>> a7e672678c663f47b8a536b2342fb132a5078397:Bushido tenkai/Assets/MenuInicial.cs
    }

    // Update is called once per frame
    public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }
}
