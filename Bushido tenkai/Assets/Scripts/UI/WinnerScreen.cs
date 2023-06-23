using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



public class WinnerScreen : MonoBehaviour
{
    public string winner;
    public TMP_Text WinnerLabel;
    private void OnEnable()
    {
        WinnerLabel.text = winner;
    }

    public void Rematch()
    {
        // Load the next scene
        GameManager.Instance.GoToGame();
    }

    public void Reselect()
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