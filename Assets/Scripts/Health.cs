//This class manipulates the health bar
using UnityEngine;
using UnityEngine.UI;

//The main class
public class Health : MonoBehaviour
{
    //Create variables
    private Slider _slider;

    //Start is called before the first frame update
    private void Start()
    {
        //Find the slider and assign maximum value and calls functions
        _slider = gameObject.GetComponent<Slider>();
        _slider.maxValue = Player_Controller.Health();
    }

    //Update is called once per frame
    private void Update()
    {
        //Update the player health
        _slider.value = Player_Controller.Health();
    }
}
