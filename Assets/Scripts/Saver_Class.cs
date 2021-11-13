//Import multiple libraries
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Saver_Class : MonoBehaviour
{
    //Set a current score and a highest score   
    private int highestScore;
    public static int currentScore;
    private static string  _textDisplay;
    [SerializeField]
    private static GameObject _display;

    private void ScoreSaving()
    {
        if(PlayerPrefs.GetInt("score") == 0 || PlayerPrefs.GetInt("score") < currentScore)
            PlayerPrefs.SetInt("score", currentScore);
    }

    //This method is called the begining of the game
    private void Awake()
    {
        _display = GameObject.FindGameObjectWithTag("Score");
        _textDisplay = "Current Score: ";
        UpdateText();

        //Assign the EndofGame Action with a method
        Player_Controller.EndOfGame += EndOfTheGame;
    }

    //This method updates the score
    public static void UpdateText()
    {
        _display.GetComponent<Text>().text = _textDisplay + currentScore.ToString();
    }

    //This is a method that will be sent to EndOFGame Event
    private void EndOfTheGame(int levelIndex)
    {
        //Call a bunch of functions
        SetScore();
        ScoreSaving();
        SceneManager.LoadSceneAsync(levelIndex, LoadSceneMode.Single);
    }

    //This method sets the final score
    private void SetScore()
    {
        //Set the final score in Final_Score script
        Final_Score.finalScore = currentScore;
    }
}
