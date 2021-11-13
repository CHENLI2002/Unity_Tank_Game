//Import important libraries
using System;
using UnityEngine;

//The main class
public class Enemy : MonoBehaviour
{
    //Set up a Func and an Action
    public static Action<Vector3> SwapPosition;

    //This method controls swaping of position
    private void OnMouseDown()
    {
        //Grab the player's position and swap
        Vector3 localPlayerPosition = Player_Controller.PlayerPosition();
        SwapPosition?.Invoke(transform.position);
        gameObject.transform.position = localPlayerPosition;
    } 
}
