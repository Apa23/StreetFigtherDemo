using UnityEngine;

public interface IDamageable
{
    public int HealthPoints { get; }
    
    public void TakeHit(int damage);
    public void EndHit();
}