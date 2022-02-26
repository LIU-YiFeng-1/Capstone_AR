using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeEffect : MonoBehaviour
{
    public GameObject explosionEffect;
    public float delay = 2f;
    float countdown;
    bool hasExploded;
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
        hasExploded = false;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;

        }
    }

    void Explode()
    {
        Debug.Log("BOOM!");
        //show explosion effect
       
        Instantiate(explosionEffect, transform.position, transform.rotation);
        //get nearby objects
            //add force
            //do damage

        //remove grenade
        //Destroy(gameObject);
    }
}
