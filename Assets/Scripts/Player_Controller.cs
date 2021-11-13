//Import libraries
using UnityEngine;
using System;
using System.Collections;

//This class controls the player
public class Player_Controller : MonoBehaviour
{
    //Set up a bunch of variables
    private Camera _currentCam;
    private Vector3 _currentWorldMousePos;
    private readonly int _speed = 3;
    private readonly int _boostedSpeed = 8;
    private int _currentSpeed;
    private readonly int _boostInterval = 2;
    public static Func<Vector3> PlayerPosition;
    private int _health = 5;
    public static Action<int> OnHit;
    private const int _ZERO = 0;
    private BoxCollider _myCollider;
    public static Func<int> Health;
    private float _stamina;
    private readonly float _maximumStamina = 3;
    private bool _runOrNot = true;
    private bool _ifIGotHit = false;
    private bool _dashable = true;
    public static Func<float> Stamina;

    [SerializeField]
    private AudioClip[] _myAudios;
    private AudioSource _myAudio;

    //An action initiate the end of the game
    public static Action<int> EndOfGame;

    //This method is called at the beginning of a game
    private void Awake()
    {
        //Assign variables and funcs and actions\
        _myAudio = gameObject.GetComponent<AudioSource>();
        Health = ReturnHealth;
        _myCollider = gameObject.GetComponent<BoxCollider>();
        _currentSpeed = _speed;
        OnHit += OnHitEffect;
        _currentCam = Camera.main;
        PlayerPosition = ThisPosition;
        Enemy.SwapPosition += SwapThePosition;
        _stamina = _maximumStamina;
        Stamina = ReturnStamina;
        DontDestroyOnLoad(this.gameObject);
    }

    //Update is called once per frame
    private void Update()
    {
        //Call a bunch of function
        LookAtTheMosue();
        PlayerMovement();
        Death();
        Boost();
        Dash();
    }

    //This method allows the player to rotate with the mouse
    private void LookAtTheMosue()
    {
        //Get the world position of our mouse
        _currentWorldMousePos = Helper_Class.Instance.ReturnMouseWorldPosition(_currentCam);
        Helper_Class.Instance.LookTowardAnotherObject(_currentWorldMousePos, gameObject);
    }

    //This method controls the player's movement
    private void PlayerMovement()
    {
        //Moves the player toward the mouse
        Vector3 newDirection = Helper_Class.Instance.FindANewDirection(_currentWorldMousePos, gameObject);
        gameObject.transform.position += newDirection * _currentSpeed * Time.deltaTime * Input.GetAxis("Vertical");
    }

    //This method returns the player position
    Vector3 ThisPosition()
    {
        //Returns my player's position
        return gameObject.transform.position;
    }

    //This method controls how to swap position
    private void SwapThePosition(Vector3 enemyPosition)
    {
        //Set the gameobejct position to the enemy's
        _myAudio.PlayOneShot(_myAudios[2]);
        gameObject.transform.position = enemyPosition;
    }

    //This is what happens once I just got hit
    private void OnHitEffect(int damage)
    {
        //Damage my player and have a temporary boost of speed
        _myAudio.PlayOneShot(_myAudios[0]);
        _health -= damage;
        _currentSpeed = _boostedSpeed;
        _myCollider.enabled = false;
        _ifIGotHit = true;
        StartCoroutine(PauseSeconds());
    }

    //Coroutine
    IEnumerator PauseSeconds()
    {
        //Put a limit to the boost
        yield return new WaitForSeconds(_boostInterval);
        _ifIGotHit = false;
        _currentSpeed = _speed;
        _myCollider.enabled = true;
    }

    //This method controls the player's death
    private void Death()
    {
        //If i have no health
        if (_health <= _ZERO)
        {
            //Invoke the end of game
            _myAudio.PlayOneShot(_myAudios[4]);
            this.gameObject.SetActive(false);
            EndOfGame?.Invoke(2);
        }
    }

    //This method returns the current health of the player
    private int ReturnHealth()
    {
        //Return the health
        return _health;
    }

    //This function constrols boosting of speed
    private void Boost()
    {
        //If we can run or not
        if (_stamina >= _maximumStamina)
            //Set the bool
            _runOrNot = true;

        //Allow the player to run for a limited amount of time
        if (Input.GetKey(KeyCode.LeftShift) && _stamina > _ZERO && _runOrNot && !_ifIGotHit)
        {
            //Set the speed and diminish the stamina per frame
            _currentSpeed = _boostedSpeed;
            _stamina -= Time.deltaTime;
        }
        //Else
        else if (!_ifIGotHit)
        {
            //Set the speed and time interval
            _currentSpeed = _speed;
            _runOrNot = false;

            //Recover the stamina if it is not maximum yet
            if (_stamina < _maximumStamina)
                //Recover the stamina
                _stamina += Time.deltaTime;
        }
    }

    //This method controls the dashing to our player
    private void Dash()
    {
        //Check if the space is pressed or not 
        if (Input.GetKeyDown(KeyCode.Space) && _dashable)
        {
            //Find a new vector pointing toward the pointer, dash, and cool down
            _myAudio.PlayOneShot(_myAudios[1]);
            Vector3 newVector = Helper_Class.Instance.FindANewDirection(_currentWorldMousePos, gameObject);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + newVector.x * 3, gameObject.transform.position.y + newVector.y * 3, 0);
            _dashable = false;
            StartCoroutine(Dashable());
        }
    }

    //This is just a cooldown
    private IEnumerator Dashable()
    {
        //Wait 5 seconds then you can dash
        yield return new WaitForSeconds(5);
        _dashable = true;
    }

    //This function returns my player's stamina
    private float ReturnStamina()
    {
        //Return stamina    
        return _stamina;
    }
}

