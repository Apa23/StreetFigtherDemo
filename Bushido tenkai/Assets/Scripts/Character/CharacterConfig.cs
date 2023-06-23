using UnityEngine;

public class CharacterConfig: MonoBehaviour
{
    // Character properties
    [Header("Character properties")] 
    public int TotalHealthPoints = 100;
    public int MaxCombo = 0;
    public int AttackDamage = 0;
    public float AttackRange = 0.5f;
    public float DashPower = 10f;
    public GameObject Proyectile;

}



