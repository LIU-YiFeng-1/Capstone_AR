using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    private bool isAmmoPackCollisionDetected = false;
    private bool isGrenadeCollisionDetected = false;
    public void OnCollisionEnter(Collision collision)
    {   
        if(collision.gameObject.name == "Ammo_box(Clone)")
        {
            //This is example
            Debug.Log("ammo pack collision detected!");
            isAmmoPackCollisionDetected = true;
            //code your thing here
        } else
        {
            Debug.Log("no ammo pack collsion!");
            isAmmoPackCollisionDetected = false;
        }

       if(collision.gameObject.name == "WPN_MK2Grenade 1(Clone)")
       {
           Debug.Log("Grenade collision detected!");
           isGrenadeCollisionDetected = true;
       } else 
       {
            Debug.Log("no grenade collsion!");
            isGrenadeCollisionDetected = false;
       }
    }

    public bool GetAmmoPackCollsionStatus()
    {
        return isAmmoPackCollisionDetected;
    }

    public bool GetGrenadeCollisionStatus()
    {
        return isGrenadeCollisionDetected;
    }
}
