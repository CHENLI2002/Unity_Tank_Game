//Import important libraries
using UnityEngine;

//This is a class that represents one bullet beheavior
public class Bullet_Strategy_1 : IDamageable
{
    //Declare variable 1
    private bool initialized = false;

    //First Strategy
    public void BulletStrategy(GameObject bullet)
    {
        //Check if this function is called or not
        if (initialized)
        {
            //Skip the add force part
            return;
        }

        //Move the bullet directly to the player
        bullet.GetComponent<Rigidbody>().AddForce(Helper_Class.Instance.FindANewDirection(Player_Controller.PlayerPosition(), bullet) * 5, ForceMode.Impulse);
        initialized = true;

        //Pick an image from two different img
        bullet.transform.GetChild(0).gameObject.SetActive(true);
        bullet.transform.GetChild(1).gameObject.SetActive(false);
    }
}
