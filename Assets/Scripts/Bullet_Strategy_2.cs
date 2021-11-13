//Import Important libraries
using UnityEngine;

//This class defines second kind of bullet behavior
public class Bullet_Strategy_2 : IDamageable
{
    //Create variables
    Rigidbody _rb;
    readonly int _maximumSpeed = 2;
    double _currentTime = 0;

    //A variable check if the strategy's running for the first time
    private bool _firstTime = true;

    //Second Strategy
    public void BulletStrategy(GameObject bullet)
    {
        //Check if we have a null or not
        if(_rb == null)
        {
            //Find the bullet rigidbody
            _rb = bullet.GetComponent<Rigidbody>();
        }

        //Move Toward the player
       _currentTime += Time.deltaTime;
       _rb.AddForce(Helper_Class.Instance.FindANewDirection(Player_Controller.PlayerPosition(), bullet) * 0.5f, ForceMode.Impulse);

        //Check if the speed is over maximum
        if(_rb.velocity.sqrMagnitude > _maximumSpeed * _maximumSpeed)
            //Set the maximum speed
            _rb.velocity = _rb.velocity.normalized * _maximumSpeed;

        //Check if the bullet is over its life span
        if(_currentTime > 8)
        {
            //Turn it off and reset variables 
            Pool_Manager.Instance.RecycleBullet(bullet);
            _currentTime = 0;
        }

        //Check if this is the first time that the strategy is running
        if(_firstTime)
        {
            //Pick an image from two different img and declare the first time is off
            bullet.transform.GetChild(0).gameObject.SetActive(false);
            bullet.transform.GetChild(1).gameObject.SetActive(true);
            _firstTime = false;
        }
    }
}