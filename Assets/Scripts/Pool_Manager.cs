//Import libraries
using UnityEngine;
using System.Collections.Generic;

//This manager manages bullet generating
public class Pool_Manager : MonoBehaviour
{
    //Declare variables
    private static Pool_Manager _localInstance = null;
    private const int _ZERO = 0;
    [SerializeField]
    private GameObject _newBullet;
    private List<GameObject> _bulletList;

    //Return one Instance of this pool manager
    public static Pool_Manager Instance
    {
        //Get
        get
        {
            //Lazy instantiation
            if(_localInstance == null)
            {
                //Set the local instance to a new pool manager
                _localInstance = FindObjectOfType<Pool_Manager>();
            }

            //Return this local instance
            return _localInstance;
        }
    }

    //This method generate a list of bullets
    private void Awake()
    {
        //A list of bullets
        int _initialListLength = 10;
        _bulletList = GenerateBullets(_initialListLength);
    }

    //Creat a list of bullets avaliable for future usage
    private List<GameObject> GenerateBullets(int amount)
    {
        //Creat a local list of bullets
        List<GameObject> bulletPool = new List<GameObject>();

        //Insert a certain number of bullets in my list
        for(int i = _ZERO; i < amount; i++)
        {
            //Instantiate a bulete and add it to my list
            GameObject bullet = Instantiate(_newBullet);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }

        //Return the list
        return bulletPool;
    }

    //This method allows a client class hava access to a bullet
    public GameObject ReturnBullet()
    { 
        //Search through the list
        foreach(var bullet in _bulletList)
        {
            //Check if the bullet is avaliable or not
            if(bullet.activeInHierarchy == false)
            {
                //Set active and return the bullet
                bullet.SetActive(true);
                return bullet;
            }
        }

        //If there's no avaliable bullets, create one and return it
        GameObject newBullet = Instantiate(_newBullet);
        newBullet.SetActive(true);
        _bulletList.Add(newBullet);
        return newBullet;
    }

    //This method controls the recycling of bullets
    public void RecycleBullet(GameObject bullet)
    {
        //Set bullet to the origin and turn it off
        bullet.transform.position = Vector3.zero;
        bullet.SetActive(false);
    }
}
