//Import important libraries
using UnityEngine;

//This class controls spwaning of enemies
public class Spawn_Manager : MonoBehaviour
{
    //Declare variables
    private readonly float _xBoundary = 14.4f;
    private readonly float _yBoundary = 11.5f;
    private const int _ZERO = 0;
    private const int _ONE = 1;
    private const int _THREE = 3;

    //Start is called before the first frame update
    void Start()
    {
        //Invoke spawning
        InvokeRepeating(nameof(GenerateEnemies), _ONE, _THREE);
        Saver_Class.currentScore = 0;
        Saver_Class.UpdateText();
    }

    //Generate a bunch of bullets
    private void GenerateEnemies()
    {
        //Spawn a bunch of enemies  
        for(int i = 0; i < Random.Range(3, 5); i++)
        {
            //Set random position of all enemies
            GameObject enemy = Enemy_Pool_Manager.Instance.ReturnEnemy();
            enemy.transform.position = new Vector3(Random.Range(-_xBoundary, _xBoundary), Random.Range(-_yBoundary, _yBoundary), _ZERO); 
        }    
    }
}
