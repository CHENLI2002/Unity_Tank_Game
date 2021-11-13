//Import importante libraries
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//This class controls all buttons
public class Button_Manager : MonoBehaviour
{
    //Get buttons
    [SerializeField]
    private GameObject[] _buttons;
    [SerializeField]
    private Button[] _buttonsComponents;
    [SerializeField]
    private GameObject Setting;
    private AudioSource _myAudio;
    [SerializeField]
    private AudioClip _sound;


    //Start is called before the first frame update
    void Start()
    {
        //Add listeners and get components and call function
        SetUpButtons();
        _buttonsComponents[0].onClick.AddListener(TurnOnGame);
        _buttonsComponents[2].onClick.AddListener(Quit);
        _buttonsComponents[1].onClick.AddListener(SettingMenu);
        _buttonsComponents[3].onClick.AddListener(OffSettingMenu);
        _myAudio = gameObject.GetComponent<AudioSource>();
    }

    //Turn on the real game
    private void TurnOnGame()
    {
        //Load Scene
        _myAudio.PlayOneShot(_sound);
        SceneManager.LoadScene("mian");
    }

    //Quit the program
    private void Quit()
    {
        //Quit the game once the button is pressed.
        _myAudio.PlayOneShot(_sound);
        Application.Quit();
    }
    
    //Turn on the Setting menu
    private void SettingMenu()
    {
        _myAudio.PlayOneShot(_sound);
        //A for each loop to access
        for (int i = 0; i < _buttons.Length - 1; i++)
        {
            //Set them to not active
            _buttons[i].SetActive(false);
        }

        //Set the setting to active
        Setting.SetActive(true);

    }

    //This method sets up both arrays
    private void SetUpButtons()
    {
        //A for loop to access the button
        for(int i = 0; i < _buttons.Length; i++)
            //Find components
            _buttonsComponents[i] = _buttons[i].GetComponent<Button>();
    }

    //Turn on the Setting menu
    private void OffSettingMenu()
    {
        //A for each loop to access
        _myAudio.PlayOneShot(_sound);   
        for (int i = 0; i < _buttons.Length - 1; i++)
        {
            //Set each button to true
            _buttons[i].SetActive(true);
        }

        //Set the setting to not active
        Setting.SetActive(false);
    }
}
