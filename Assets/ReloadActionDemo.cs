using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadActionDemo : MonoBehaviour
{
    public Rigidbody airCraft;
    public Transform startingReferencePoint;
    public Rigidbody ammoPack;
    public float throwForce = 55.0f;

    private float countDown = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {   
            Rigidbody obj = Instantiate(airCraft, startingReferencePoint.position, airCraft.transform.rotation); 
            obj.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);

            if(countDown <= 0)
            {
                Rigidbody rb = Instantiate(ammoPack, airCraft.position, Quaternion.identity);
            }
        }       
    }
}
