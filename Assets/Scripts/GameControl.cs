using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameControl : MonoBehaviour
{
    public GameObject arCamera;
    public GameObject shield;
    public Image oppHp;
    public Image playerHp;
    public Image shieldHp;    
    //following is for Hp
    public int maxHp = 100;
    private int currentOppHp;
    private int currentPlayerHp;
    //following is for bullet
    private int bulletDamage = 10;
    //following is for grenade
    public GameObject grenadePrefab;
    public float throwForce = 40f;
    private int grenadeDamage = 30;
    //following is for shield
    public GameObject oppShield1;
    public GameObject oppShield2;
    public GameObject oppShield3;
    private int shieldCounter = 3;
    private int maxShieldHp = 30;
    private int currentShieldHp;
    private bool isShieldActive;
    public float shieldCountDown = 10.0f; //this is to be updated later to 10sec, 3sec is for testing purpose
    // Start is called before the first frame update
    void Start()
    {
        currentPlayerHp = maxHp;
        currentOppHp = maxHp;
        currentShieldHp = maxShieldHp;
        shield.SetActive(false); //set false if using shield button; set true if testing for shield hp
        isShieldActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        OpponentShieldAction();
    }

    public void ActivateOpponentSheild()
    {
        Debug.Log("shield button clicked! but outside if statement");
        if(shieldCounter > 0)
        {
            shield.SetActive(true);
            isShieldActive = true;
            shieldHp.fillAmount = 1;    
            currentShieldHp = maxShieldHp;
            shieldCounter--;
            Debug.Log("shield count -1; inside if statement");
        }
    }
    public void TakeBulletDamage() 
    {   
        RaycastHit hit;
        Debug.Log("player fired, gun is being pressed");

        if(isShieldActive && Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
        {
            currentShieldHp -= bulletDamage;
            shieldHp.fillAmount = (float)currentShieldHp / (float)maxShieldHp;           
            Debug.Log("Raycast hit shield!");
        }
        //Raycast hit will be replaced by data input from external comms
        if((!isShieldActive) && Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
        {
            currentOppHp -= bulletDamage;
            oppHp.fillAmount = (float)currentOppHp / (float)maxHp;           
            Debug.Log("Raycast hit player!");
        } 

        //actions for shield
        if(currentShieldHp==0) {
            shield.SetActive(false);
            isShieldActive = false;
            currentShieldHp = maxShieldHp;
            Debug.Log("shield is down; shield is down due to damage taken!");
        }

        if(currentOppHp==0) {
            Debug.Log("Oppenent died!");
        }
    }
    
    // //the following is the origianl working code for shieldHp
    // public void TakeBulletDamage() 
    // {   
    //     RaycastHit hit;
    //     Debug.Log("player fired, gun is being pressed");

    //     if(Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
    //     {
    //         currentShieldHp -= bulletDamage;
    //         shieldHp.fillAmount = (float)currentShieldHp / (float)maxShieldHp;           
    //         Debug.Log("Raycast hit robot!");
    //     }
    //     if(currentShieldHp==0) {
    //         shield.SetActive(false);
    //         isShieldActive = false;
    //         currentShieldHp = maxShieldHp;
    //     }
    // }
    private void OpponentShieldAction()
    {
        //when the shield is activated, the count down timer for the shield is initialized
        if(isShieldActive) 
        {
            shieldCountDown -= Time.deltaTime;
            if(shieldCountDown <= 0)
            {
                shield.SetActive(false);
                isShieldActive = false;
                shieldCountDown = 5.0f;
            }
        }
        //this switch statement checks for the shield counter and update the UI accordingly
        //hides the corresponding shield from the status panel
        switch (shieldCounter)
        {
        case 2:
            oppShield3.SetActive(false);
            Debug.Log("Shield number 3 is gone!");
            Debug.Log("current shieldCounter =" + shieldCounter);
            break;
        case 1:
            oppShield2.SetActive(false);
            Debug.Log("Shield number 2 is gone!");
            Debug.Log("current shieldCounter =" + shieldCounter);
            break;
        case 0:
            oppShield1.SetActive(false);
            Debug.Log("Shield number 1 is gone!");
            Debug.Log("current shieldCounter =" + shieldCounter);
            break;
        default:
            break;
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
