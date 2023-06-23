using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Animations;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CharacterSelection : MonoBehaviour
{
    private PlayerControls _playerControls;
    private string _characterName;
    private int _player1Index = 0;
    private int _player2Index = 4;

    private bool _player1Submited = false;
    private bool _player2Submited = false;
    [SerializeField] private List<string> characterNames;
    [SerializeField] private List<AnimatorController> _animations;
    [Header("TextNames")]
    [SerializeField] private TMP_Text _character1Name;
    [SerializeField] private TMP_Text _character2Name;
    [Header("Characters")]
    [SerializeField] private Animator _character1Anim;
    [SerializeField] private Animator _character2Anim;
    [Header("ReadyPlayer")]
    [SerializeField] private GameObject _readyPlayer1;
    [SerializeField] private GameObject _readyPlayer2;





    private void Awake()
    {
        _playerControls = new PlayerControls();

        _playerControls.UiNavigation.ChangeCharacterRight.performed += ctx => ChangeCharacter1(1);
        _playerControls.UiNavigation.ChangeCharacterLeft.performed += ctx => ChangeCharacter1(-1);
        _playerControls.UiNavigation.Submit.performed += ctx => Submit1();
        _playerControls.UiNavigation.Cancel.performed += ctx => Cancel1();



        _playerControls.UiNavigation2.ChangeCharacterRight.performed += ctx => ChangeCharacter2(1);
        _playerControls.UiNavigation2.ChangeCharacterLeft.performed += ctx => ChangeCharacter2(-1);
        _playerControls.UiNavigation2.Submit.performed += ctx => Submit2();
        _playerControls.UiNavigation2.Cancel.performed += ctx => Cancel2();
    }


    private void OnEnable() //Enable input controls
    {

        _playerControls.UiNavigation.Enable();
        _playerControls.UiNavigation2.Enable();

    }

    private void OnDisable() //Disable input controls
    {
        _playerControls.UiNavigation.Disable();
        _playerControls.UiNavigation2.Disable();

    }
    void Update()
    {
        if (_player1Submited && _player2Submited)
        {
            GameManager.Instance.GoToGame();
        }
    }

    private void ChangeCharacter1(int factor)
    {
        if (!_player1Submited)
        {
            _player1Index = _player1Index + factor;
            if (_player1Index > 3)
            {
                _player1Index = 0;
            }
            else if (_player1Index < 0)
            {
                _player1Index = 3;
            }
            _character1Name.text = characterNames[_player1Index];
            _character1Anim.runtimeAnimatorController = _animations[_player1Index];
            GameManager.player1CharacterIndex = _player1Index;
        }
    }


    private void ChangeCharacter2(int factor)
    {
        if (!_player2Submited)
        {
            _player2Index = _player2Index + factor;
            if (_player2Index > 3)
            {
                _player2Index = 0;
            }
            else if (_player2Index < 0)
            {
                _player2Index = 3;
            }
            _character2Name.text = characterNames[_player2Index];
            _character2Anim.runtimeAnimatorController = _animations[_player2Index];
            GameManager.player2CharacterIndex = _player2Index;
        }
    }
    private void Submit1()
    {
        _player1Submited = true;
        _readyPlayer1.SetActive(true);


    }
    private void Submit2()
    {
        _player2Submited = true;
        _readyPlayer2.SetActive(true);
    }



    private void Cancel1()
    {
        if (!_player1Submited && !_player2Submited)
        {
            GameManager.Instance.GoToMenu();
        }
        _player1Submited = false;
        _readyPlayer1.SetActive(false);
    }
    private void Cancel2()
    {
        if (!_player1Submited && !_player2Submited)
        {
            GameManager.Instance.GoToMenu();
        }
        _player2Submited = false;
        _readyPlayer2.SetActive(false);
    }



}


