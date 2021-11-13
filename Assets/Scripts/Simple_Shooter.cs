//Import libraries
using UnityEngine;

//This is a class controlls a simple shooting mechanism
public class Simple_Shooter : MonoBehaviour
{
    //Declare variable
    private readonly int _shootingInterval = 2;
    private IDamageable _someStrategy;
    private const float _ZERO = 0f;
    private const float _ONE = 1;
    private readonly float _probability = 0.3f;

    private AudioSource _myAudio;
    [SerializeField]
    private AudioClip _shoot;

    //This method is called upon the beginning of the game
    private void Awake()
    {
        //Repetivively shoot toward the player and create funcs
        _myAudio = gameObject.GetComponent<AudioSource>();
        InvokeRepeating(nameof(ShootBullet), _shootingInterval, _shootingInterval);
    }

    //This method is called once per frame
    private void Update()
    {
        //Call functions
        Helper_Class.Instance.LookTowardAnotherObject(Player_Controller.PlayerPosition(), gameObject);
    }

    //This method controls bullet shooting
    private void ShootBullet()
    {
        if (gameObject.activeInHierarchy)
        {
            _myAudio.PlayOneShot(_shoot);
        }
        //Pick one of two strategies literallyh
        PickAStrategy();

        //Check i the enemy's active or not
        if(gameObject.activeInHierarchy)
        {
            //Spawn
            GameObject bullet = Pool_Manager.Instance.ReturnBullet();
            bullet.GetComponent<Bullets>().Initialize(transform.position, _someStrategy);
        }       
    }

    //Pick a random strategy
    private void PickAStrategy()
    {
        //Check if the possibilty
        if(Random.Range(_ZERO, _ONE) <= _probability)
        {
            //Return 2
            _someStrategy = new Bullet_Strategy_2();
        }
        //Else
        else
            //Strategy 1
            _someStrategy = new Bullet_Strategy_1();
    }
}
