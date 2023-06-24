using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



public class WinnerScreen : MonoBehaviour
{
    // Winner variable
    public string winner;

    // Ui variable
    public TMP_Text WinnerLabel;

    private void OnEnable() // Set up winner name
    {
        WinnerLabel.text = winner; 
    }

    public void Reselect() // Load the character selection scene
    {   
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