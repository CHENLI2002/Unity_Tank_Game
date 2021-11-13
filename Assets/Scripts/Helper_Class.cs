//Import important libraries
using UnityEngine;

//This is a helper class containing generally applicable methods and variables
public class Helper_Class
{
    //A local instance of this class and a bunch of variables
    private static Helper_Class _localInstance = null;
    private const int _TWENTY = 20;
    private const int _ZERO = 0;

    //Provides global access
    public static Helper_Class Instance
    {
        //Get
        get
        {
            //Lazy instantiation
            if(_localInstance == null)
            {
                //Instantiate a new object of this particular class
                _localInstance = new Helper_Class();
            }

            //Return this object
            return _localInstance;
        }
    }

    //Allow one object to turn toward another object
    public void LookTowardAnotherObject(Vector3 targetObject, GameObject origin)
    {
        //Set a new direction and rotate the origin
        Vector3 directionToFace = targetObject - origin.transform.position;
        origin.transform.rotation = Quaternion.LookRotation(directionToFace);
    }

    //This method returns the mouse position in world view
    public Vector3 ReturnMouseWorldPosition(Camera cam)
    {
        //Create a new Vector3 with particular mouse position in world view
        Vector3 returnPoint = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _TWENTY));
        returnPoint = new Vector3(returnPoint.x, returnPoint.y, _ZERO);
        return returnPoint;
    }

    //This creates a normalized vector pointing from the origin to the target
    public Vector3 FindANewDirection(Vector3 target, GameObject origin)
    {
        //Toward the target
        Vector3 newDirection = target - origin.transform.position;
        newDirection = newDirection.normalized;
        return newDirection;
    }
}