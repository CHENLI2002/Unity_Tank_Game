using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _buttons;
    [SerializeField]
    private Button[] _components;

    private AudioSource _myAudioSource;
    [SerializeField]
    private AudioClip _button;

    // Start is called before the first frame update
    void Start()
    {
        FindButtons();
        _myAudioSource = gameObject.GetComponent<AudioSource>();
        _components[0].onClick.AddListener(EndGame);
        _components[1].onClick.AddListener(ReturnToMainMenu);
        _components[2].onClick.AddListener(Retry);
    }

    private void FindButtons()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            _components[i] = _buttons[i].GetComponent<Button>();
        }
    }

    private void EndGame()
    {
        _myAudioSource.PlayOneShot(_button);
        Application.Quit();
    }

    private void ReturnToMainMenu()
    {
        _myAudioSource.PlayOneShot(_button);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    private void Retry()
    {
        _myAudioSource.PlayOneShot(_button);
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }
}

