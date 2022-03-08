using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPackAction : MonoBehaviour
{
    public Transform target;
    public Rigidbody ammoPack;
    public void DropAmmoPack()
    {
        Vector3 pos = new Vector3(0f, 10f, 0f);
        float delay = 0.7f;
        delay -= Time.deltaTime;

        if(delay <= 0)
            {
                Rigidbody rb = Instantiate(ammoPack, target.position + pos, Quaternion.identity);
                delay = 0.7f;
            }
    }
}
