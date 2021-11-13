//Import a bunch of libraries
using UnityEngine;
using UnityEngine.UI;

//This class controls the slider of stamina
public class Stamina : MonoBehaviour
{
    //Find my slider
    [SerializeField]
    private Slider _mySlider;

    //Update is called once per frame
    void Update()
    {
        //Set the slider's value
        _mySlider.value = Player_Controller.Stamina();  
    }
}
