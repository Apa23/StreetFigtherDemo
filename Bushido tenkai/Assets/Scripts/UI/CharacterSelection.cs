using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CharacterSelection : MonoBehaviour
{
    private PlayerControls _playerControls;
    string CharacterName;
    private int Player1Index = 0;
    private int Player2Index = 4;
    
    private bool Player1Submited = false;//TEMPORAL//TEMPORAL
    private bool Player2Submited = false;
    [SerializeField] List<string> characterNames;
    [SerializeField] List<Sprite> Images;
    [Header("TextNames")]
    [SerializeField] TMP_Text Character1;
    [SerializeField] TMP_Text Character2;
    [Header("Characters")]
    [SerializeField] Image Sprite1;
    [SerializeField] Image Sprite2;
    [Header("ReadyPlayer")]
    [SerializeField] GameObject Sprite3;
    [SerializeField] GameObject Sprite4;

    
   


    private void Awake()
    {
        _playerControls = new PlayerControls();

        //CONFIGURAR Y CAMBIAR AL UiNavigation2 

        // _playerControls.UiNavigation2.ChangeCharacterRight.performed += ctx => ChangeCharacterRigh1t();//PLAYER1
        //_playerControls.UiNavigation2.ChangeCharacterLeft.performed += ctx => ChangeCharacterLef1t1();//PLAYER1
        //_playerControls.UiNavigation2.Submit.performed += ctx => Submit1();
       // _playerControls.UiNavigation2.Cancel.performed += ctx => Cancel();



        _playerControls.UiNavigation.ChangeCharacterRight.performed += ctx => ChangeCharacterRight2();//PLAYER2
        _playerControls.UiNavigation.ChangeCharacterLeft.performed += ctx => ChangeCharacterLeft2();//PLAYER2
        _playerControls.UiNavigation.Submit.performed += ctx => Submit2();
        _playerControls.UiNavigation.Cancel.performed += ctx => Cancel();
    }
    void Start()
    {

    }


    private void OnEnable() //Enable input controls
    {

        _playerControls.UiNavigation.Enable();



    }

    private void OnDisable() //Disable input controls
    {
        _playerControls.UiNavigation.Disable();

    }
    void Update()
    {
        if (Player1Submited == true && Player2Submited == true)
        {

            GameManager.player1CharacterIndex = Player1Index;
            GameManager.player2CharacterIndex = Player2Index;
            GameManager.Instance.GoToGame();

        }
    }

    private void ChangeCharacterRight1()
    {

        Player1Index = Player1Index + 1;
        if (Player1Index > 3)
        {
            Player1Index = 0;

        }
        Character1.text = characterNames[Player1Index];
        Sprite1.sprite = Images[Player1Index];
    }

    private void ChangeCharacterLeft1()
    {


        Player1Index = Player1Index - 1;
        if (Player1Index < 0)
        {
            Player1Index = 3;
            

        }

        Character1.text = characterNames[Player1Index];
        Sprite1.sprite = Images[Player1Index];

    }

    private void ChangeCharacterRight2()
    {

        Player2Index = Player2Index + 1;
        if (Player2Index > 3)
        {
            Player2Index = 0;

        }
        Character2.text = characterNames[Player2Index];
        Sprite2.sprite = Images[Player2Index];
        Sprite2.rectTransform.localScale = new Vector3(-1f, 1f, 1f);
    }

    private void ChangeCharacterLeft2()
    {


        Player2Index = Player2Index - 1;
        if (Player2Index < 0)
        {
            Player2Index = 3;
          

        }

        Character2.text = characterNames[Player2Index];
        Sprite2.sprite = Images[Player2Index];
        Sprite2.rectTransform.localScale = new Vector3(-1f, 1f, 1f);

    }

    private void Submit1()
    {   //ACTIVAR
       // _playerControls.UiNavigation2.ChangeCharacterLeft.Disable();
        //_playerControls.UiNavigation2.ChangeCharacterRight.Disable();
        Player1Submited = true;
        Sprite3.SetActive(true);



    }
    private void Submit2()
    {
        _playerControls.UiNavigation.ChangeCharacterLeft.Disable();
        _playerControls.UiNavigation.ChangeCharacterRight.Disable();
        Player2Submited = true;
        Sprite4.SetActive(true);

    }



    private void Cancel()
    {
        if (Player1Submited)
        {
            _playerControls.UiNavigation.ChangeCharacterLeft.Enable();
            _playerControls.UiNavigation.ChangeCharacterRight.Enable();
            Player1Submited = false;
            Sprite3.SetActive(false);

        }

        else if (Player2Submited)
        {
            _playerControls.UiNavigation.ChangeCharacterLeft.Enable();
            _playerControls.UiNavigation.ChangeCharacterRight.Enable();
            Player2Submited = false;
            Sprite4.SetActive(false);
        }


        else
        {
            Debug.Log("entre");
            GameManager.Instance.GoToMenu();

            
        }
    }

}


