using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameControl : MonoBehaviour
{
    public GameObject arCamera;
    public GameObject shield;
    public Image shieldHp;
    //following is for Hp
    public int maxHp = 100;
    private int currentHp;
    //following is for bullet
    private int bulletDamage = 10;
    //following is for grenade
    public GameObject grenadePrefab;
    public float throwForce = 40f;
    private int grenadeDamage = 30;
    //public float grenadeCountDown = 2.0f;
    //following is for shield
    private int maxShieldHp = 30;
    private int currentShieldHp;
    private bool isShieldActive;
    public float shieldCountDown = 3.0f; //this is to be updated later to 10sec, 3sec is for testing purpose
    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        currentShieldHp = maxShieldHp;
        shield.SetActive(false); //set false if using shield button; set true if testing for shield hp
        isShieldActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        ShieldAction();
    }

    public void ActivateSheild()
    {
        //if(currentShieldHp > 0 && shieldCountDown >0)
        //{
            shield.SetActive(true);
            isShieldActive = true;
            shieldHp.fillAmount = 1;    
            currentShieldHp = maxShieldHp;
            Debug.Log("shield count -1");
        //}
    }
    public void TakeBulletDamage() 
    {   
        RaycastHit hit;
        Debug.Log("player fired, gun is being pressed");

        if(Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
        {
            currentShieldHp -= bulletDamage;
            shieldHp.fillAmount = (float)currentShieldHp / (float)maxShieldHp;           
            Debug.Log("Raycast hit robot!");
        }
        if(currentShieldHp==0) {
            shield.SetActive(false);
            isShieldActive = false;
            currentShieldHp = maxShieldHp;
        }
    }
    private void ShieldAction()
    {
        if(isShieldActive) 
        {
            shieldCountDown -= Time.deltaTime;
            if(shieldCountDown <= 0)
            {
                shield.SetActive(false);
                isShieldActive = false;
                shieldCountDown = 3.0f;
            }
        }
        Debug.Log("ShieldAction function called");
    }
    public void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
        Debug.Log("grenade throwed");
    }

}
