using UnityEngine;
using UnityEngine.SceneManagement;



public class InitialMenu : MonoBehaviour
{
    
    public void Play() // On clicking play button
    {
        // Load the character selection scene
        GameManager.Instance.GoToSelect();
    }


    
    public void Exit() // On clicking exit button stop game
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
