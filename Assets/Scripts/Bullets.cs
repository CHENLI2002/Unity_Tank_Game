//Import some libraries
using UnityEngine;
using System.Collections;

//This class is attached to the bullet and controls its movement
public class Bullets : MonoBehaviour
{
    //Declare a bunch of variables
    private Rigidbody _bulletRB;
    private readonly float _xBoundary = 14.4f;
    private readonly float _yBoundary = 11.5f;
    private IDamageable _IDamageable;
    private readonly int _bulletDamage = 1;
    private bool _enemyHit = false;


    //This function is called awake
    private void Awake()
    {
        //Find my rigid body and damage

        _bulletRB = gameObject.GetComponent<Rigidbody>();
    }

    //This function is called once the gameobject is disabled
    private void OnDisable()
    {
        //Cannot hit the enemy and set speed to zero and reset the strategy
        _enemyHit = false;
        _bulletRB.velocity = Vector3.zero;
    }

    //This function makes my gameObject
    public void Initialize(Vector3 position, IDamageable someBehavior)
    {
        //Set the a new direction toward the player and move toward it
        transform.position = position;
        _IDamageable = someBehavior;
    }

    //Update is called once per frame
    private void Update()
    {
        //Calls a bunch of functions
        SelfDestruction();

        //Check if the interface exists
        if(_IDamageable != null)
            //Call the function
            _IDamageable.BulletStrategy(this.gameObject);
    }

    //What happens after it leaves the enemy
    private void OnTriggerExit(Collider other)
    {
        //Can hit enemy
        if(other.CompareTag("Enemy"))
            //I can hit an enemy
            _enemyHit = true;
    }

    //This function is called once the bullet runs into the player
    private void OnTriggerEnter(Collider other)
    {
        //Call functions
        BulletDamage(other);
    }

    //This declares what happens if an object is hit
    private void BulletDamage(Collider other)
    {   
        //Check which object I collided with
        if(other.CompareTag("Player"))
        {
            //Recycle the bullet and do damage
            Player_Controller.OnHit?.Invoke(_bulletDamage);
            Pool_Manager.Instance.RecycleBullet(gameObject);
        }
        //Else if it hits the enemy
        else if(other.CompareTag("Enemy") && _enemyHit)
        {
            //Destroy the enemy
            other.gameObject.SetActive(false);
            Pool_Manager.Instance.RecycleBullet(gameObject);

            //Set score once
            SetScore();
        }
    }

    //This sets all out of bound bullets disactive
    private void SelfDestruction()
    {
        //Check if the bullet is out of boundary or not
        if(Mathf.Abs(gameObject.transform.position.x) > _xBoundary || Mathf.Abs(gameObject.transform.position.y) > _yBoundary)
            //Set it not active
            Pool_Manager.Instance.RecycleBullet(gameObject);
    }

    //Set the score after an enmy got hit
    private void SetScore()
    {
        //Add one
        Saver_Class.currentScore += 1;
        Saver_Class.UpdateText();
    }
}
