using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersPlacement : MonoBehaviour
{

    public GameObject[] CharacterPrefabs;
    public Transform SpawnPoint1;
    public Transform SpawnPoint2;
    private int _player1Index;
    private int _player2Index;

    private void Awake()
    {
        _player1Index = GameManager.player1CharacterIndex;
        _player2Index = GameManager.player2CharacterIndex;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.LogWarning(_player1Index);
        Debug.LogWarning(_player2Index);

        // Instantiate the characters
        GameObject Character1= Instantiate(CharacterPrefabs[_player1Index], SpawnPoint1.position, SpawnPoint1.rotation);
        GameObject Character2 = Instantiate(CharacterPrefabs[_player2Index], SpawnPoint2.position, SpawnPoint2.rotation);

        // Change the name of the characters instances
        Character1.name = "Player1";
        Character2.name = "Player2";

        // Add enemy references component
        Character1.AddComponent<EnemyReference>();
        Character2.AddComponent<EnemyReference>();

        // Set the enemy references 
        Character1.GetComponent<EnemyReference>().Enemy = Character2;
        Character2.GetComponent<EnemyReference>().Enemy = Character1;

        // Set character layer 
        Character1.layer = SpawnPoint1.gameObject.layer;
        Character2.layer = SpawnPoint2.gameObject.layer;

        

    }

   
}
