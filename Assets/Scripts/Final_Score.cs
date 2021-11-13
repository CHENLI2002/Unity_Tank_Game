using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Final_Score : MonoBehaviour
{
    //Receive a final score
    public static int finalScore;

    //called while start
    private void Start()
    {
        //Display the final score
        gameObject.GetComponent<Text>().text = "Final Score: " + finalScore;
    }
}
