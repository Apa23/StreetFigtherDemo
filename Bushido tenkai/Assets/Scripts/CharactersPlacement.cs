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


        GameObject Player1= Instantiate(characterPrefabs[Player1Index], spawnPoint1.position, spawnPoint1.rotation);
        GameObject Player2 = Instantiate(characterPrefabs[Player2Index], spawnPoint2.position, spawnPoint2.rotation);
        Player2.transform.localScale = new Vector3(-1f, 1f, 1f);
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
