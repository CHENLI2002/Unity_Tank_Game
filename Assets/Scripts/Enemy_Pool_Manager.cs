//Import important libraries
using UnityEngine;
using System.Collections.Generic;

//This class generates a list of enemies
public class Enemy_Pool_Manager : MonoBehaviour
{
    //Declare variables
    private static Enemy_Pool_Manager _localInstance = null;
    [SerializeField]
    private GameObject _enemy;
    private const int _ZERO = 0;
    private List<GameObject> _enemyList;

    //This function is called once the program is called
    private void Awake()
    {
        //Create a list of enemies
        int initialLength = 10;
        _enemyList = GenerateEnemies(initialLength);
    }

    //Return an instance of
    public static Enemy_Pool_Manager Instance
    {
        //Get
        get
        {
            //Lazy instantiation
            if (_localInstance == null)
            {
                //Set the local instance to a new pool manager
                _localInstance = FindObjectOfType<Enemy_Pool_Manager>();
            }

            //Return this local instance
            return _localInstance;
        }
    }

    //Generate a list of enemies
    private List<GameObject> GenerateEnemies(int number)
    {
        //Creat a local list of enemies
        List<GameObject> newEnemies = new List<GameObject>();

        //Add several bullets to the list
        for(int i = _ZERO; i < number; i++)
        {
            //Add bullets
            GameObject enemy = Instantiate(_enemy);
            enemy.SetActive(false);
            newEnemies.Add(enemy);
        }

        //Return a list of bullets
        return newEnemies;
    }
    
    //Return an enemy prefab once requested
    public GameObject ReturnEnemy()
    {
        //Search the list
        foreach (var enemy in _enemyList)
        {
            //Check if the bullet is avaliable or not
            if (enemy.activeInHierarchy == false)
            {
                //Set active and return the bullet
                enemy.SetActive(true);
                return enemy;
            }
        }

        //If there's no avaliable bullets, create one and return it
        GameObject newEnemy = Instantiate(_enemy);
        newEnemy.SetActive(true);
        _enemyList.Add(newEnemy);
        return newEnemy;
    }
}
