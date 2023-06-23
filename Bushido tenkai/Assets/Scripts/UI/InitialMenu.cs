using UnityEngine;
using UnityEngine.SceneManagement;



public class InitialMenu : MonoBehaviour
{
    // On clicking play button
    public void Play()
    {
        // Load the next scene
        GameManager.Instance.GoToSelect();
    }


    // On clicking exit button
    public void Exit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
