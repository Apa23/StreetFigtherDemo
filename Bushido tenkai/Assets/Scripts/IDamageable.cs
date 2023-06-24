using UnityEngine;

public interface IDamageable 
{
    // Attributes
    public int HealthPoints { get; }
    public void TakeHit(int damage);
    public void EndHit();
}