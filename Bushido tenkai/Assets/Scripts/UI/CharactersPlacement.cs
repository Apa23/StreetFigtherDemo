using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersPlacement : MonoBehaviour
{

    public GameObject[] characterPrefabs;
    public Transform spawnPoint1;
    public Transform spawnPoint2;

    private void Awake()
    {
        int Player1Index = GameManager.player1CharacterIndex;
        int Player2Index = GameManager.player1CharacterIndex;

        // Instantiate the characters
        GameObject Character1= Instantiate(characterPrefabs[Player1Index], spawnPoint1.position, spawnPoint1.rotation);
        GameObject Character2 = Instantiate(characterPrefabs[Player2Index], spawnPoint2.position, spawnPoint2.rotation);

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
        Character1.layer = spawnPoint1.gameObject.layer;
        Character2.layer = spawnPoint2.gameObject.layer;


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
