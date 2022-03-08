using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    private bool isCollsionDetected = false;
    public void OnCollisionEnter(Collision collision)
    {   
        if(collision.gameObject.name == "Ammo_box(Clone)")
        {
            //This is example
            Debug.Log("collision detected");
            isCollsionDetected = true;
            //code your thing here
        } else 
        {
            Debug.Log("no collsion!");
            isCollsionDetected = false;
        }
    }

    public bool GetCollsionStatus()
    {
        return isCollsionDetected;
    }
}
