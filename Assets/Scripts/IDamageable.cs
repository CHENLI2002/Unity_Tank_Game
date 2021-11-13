using UnityEngine;
//This interface controls the damage methods
public interface IDamageable
{
    //Declare what needs to happen in strategy classes
    void BulletStrategy(GameObject something);
}