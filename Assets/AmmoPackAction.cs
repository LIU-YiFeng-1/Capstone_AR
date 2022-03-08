using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPackAction : MonoBehaviour
{
    public Transform target;
    public GameObject ammoPack;
    public void DropAmmoPack()
    {
        Vector3 pos = new Vector3(0f, 10f, 0f);
        float delay = 1f;
        delay -= Time.deltaTime;
        Debug.Log("DropAmmoPack function called!");
        Rigidbody ammoPackRb = ammoPack.GetComponent<Rigidbody>();
       // if(delay <= 0)
            //{
                Rigidbody rb = Instantiate(ammoPackRb, target.position + pos, Quaternion.identity);
                Debug.Log("ammo pack dropped");
           // }
    }
}
