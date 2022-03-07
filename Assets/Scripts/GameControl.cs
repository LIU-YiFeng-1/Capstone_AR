using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameControl : MonoBehaviour
{
    public GameObject arCamera;
    public Image oppHp;
    public Image playerHp;

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
    //following is for general shield status
    private int maxShieldHp = 30;
    private float shieldCountDownValue = 10.0f; 
    //following is for opponent shield
    
    public GameObject oppShield;
    public Image oppShieldHp;
    public float oppShieldCountDown; 
    public GameObject oppShield1;
    public GameObject oppShield2;
    public GameObject oppShield3;
    private int oppShieldCounter = 3;
    private int currentOppShieldHp;
    private bool isOppShieldActive;

    //following is for player shield
    public GameObject playerShield;
    public Image playerShieldHp;
    public float playerShieldCountDown; 
    public GameObject playerShield1;
    public GameObject playerShield2;
    public GameObject playerShield3;
    private int playerShieldCounter = 3;
    private int currentPlayerShieldHp;
    private bool isPlayerShieldActive;
    // Start is called before the first frame update
    void Start()
    {
        currentPlayerHp = maxHp;
        currentOppHp = maxHp;
        currentPlayerShieldHp = maxShieldHp;
        currentOppShieldHp = maxShieldHp;
        playerShieldCountDown = shieldCountDownValue;
        oppShieldCountDown = shieldCountDownValue;

        playerShield.SetActive(false);
        isPlayerShieldActive = false;
        oppShield.SetActive(false); //set false if using shield button; set true if testing for shield hp
        isOppShieldActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerShieldAction();
        OpponentShieldAction();
    }
    public void ActivatePlayerSheild()
    {
        Debug.Log("ActivatePlayerSheild function called! but outside if statement");
        if(playerShieldCounter > 0)
        {
            playerShield.SetActive(true);
            isPlayerShieldActive = true;
            playerShieldHp.fillAmount = 1;    
            currentPlayerShieldHp = maxShieldHp;
            playerShieldCounter--;
            Debug.Log("player shield count -1; inside if statement");
        }
    }
    public void ActivateOpponentSheild()
    {
        Debug.Log("ActivateOpponentSheild function called! but outside if statement");
        if(oppShieldCounter > 0)
        {
            oppShield.SetActive(true);
            isOppShieldActive = true;
            oppShieldHp.fillAmount = 1;    
            currentOppShieldHp = maxShieldHp;
            oppShieldCounter--;
            Debug.Log("opponent shield count -1; inside if statement");
        }
    }

    //player takes damage based on the input signal from external comm only, disable ray cast
    //only there is a hit registered, then decrement the playerHp
    public void PlayerTakeBulletDamage() 
    {   
        Debug.Log("opponent fired, gun is being pressed, player gets hit");

        if(isPlayerShieldActive)
        {
                currentPlayerShieldHp -= bulletDamage;
                playerShieldHp.fillAmount = (float)currentPlayerShieldHp / (float)maxShieldHp;           
                Debug.Log("player gets hit but shield is up; update playerShieldHp accordingly");
        } else
        {
            currentPlayerHp -= bulletDamage;
            playerHp.fillAmount = (float)currentPlayerHp / (float)maxHp;           
            Debug.Log("player is hit and is withou shield; update playerHp accordingly");
        }

        //actions for shield
        if(currentPlayerShieldHp==0) {
            playerShield.SetActive(false);
            isPlayerShieldActive = false;
            currentPlayerShieldHp = maxShieldHp;
            Debug.Log("shield is down; shield is down due to damage taken!");
        }

        if(currentPlayerHp==0) {
            Debug.Log("player died!");
        }
    }
    

    public void OpponentTakeBulletDamage() 
    {   
        RaycastHit hit;
        Debug.Log("player fired, gun is being pressed, opponent gets hit");

        if(isOppShieldActive)
        {
            if(Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
            {
                currentOppShieldHp -= bulletDamage;
                oppShieldHp.fillAmount = (float)currentOppShieldHp / (float)maxShieldHp;           
                Debug.Log("Raycast hit shield!");
            }
        } else
        {
            currentOppHp -= bulletDamage;
            oppHp.fillAmount = (float)currentOppHp / (float)maxHp;           
            Debug.Log("Raycast hit player!");
        }

        //actions for shield
        if(currentOppShieldHp==0) {
            oppShield.SetActive(false);
            isOppShieldActive = false;
            currentOppShieldHp = maxShieldHp;
            Debug.Log("shield is down; shield is down due to damage taken!");
        }

        if(currentOppHp==0) {
            Debug.Log("Oppenent died!");
        }
    }
    
    private void PlayerShieldAction()
    {
        //when the shield is activated, the count down timer for the shield is initialized
        if(isPlayerShieldActive) 
        {
            playerShieldCountDown -= Time.deltaTime;
            if(playerShieldCountDown <= 0)
            {
                playerShield.SetActive(false);
                isPlayerShieldActive = false;
                playerShieldCountDown = shieldCountDownValue;
            }
        }
        //this switch statement checks for the shield counter and update the UI accordingly
        //hides the corresponding shield from the status panel
        switch (playerShieldCounter)
        {
        case 2:
            playerShield3.SetActive(false);
            Debug.Log("player Shield number 3 is gone!");
            Debug.Log("current player shieldCounter =" + playerShieldCounter);
            break;
        case 1:
            playerShield2.SetActive(false);
            Debug.Log("player Shield number 2 is gone!");
            Debug.Log("current player shieldCounter =" + playerShieldCounter);
            break;
        case 0:
            playerShield1.SetActive(false);
            Debug.Log("player Shield number 1 is gone!");
            Debug.Log("current player shieldCounter =" + playerShieldCounter);
            break;
        default:
            break;
        }
        Debug.Log("ShieldAction function called");
    }
    private void OpponentShieldAction()
    {
        //when the shield is activated, the count down timer for the shield is initialized
        if(isOppShieldActive) 
        {
            oppShieldCountDown -= Time.deltaTime;
            if(oppShieldCountDown <= 0)
            {
                oppShield.SetActive(false);
                isOppShieldActive = false;
                oppShieldCountDown = shieldCountDownValue;
            }
        }
        //this switch statement checks for the shield counter and update the UI accordingly
        //hides the corresponding shield from the status panel
        switch (oppShieldCounter)
        {
        case 2:
            oppShield3.SetActive(false);
            Debug.Log("opponent Shield number 3 is gone!");
            Debug.Log("current opponent shieldCounter =" + oppShieldCounter);
            break;
        case 1:
            oppShield2.SetActive(false);
            Debug.Log("opponent Shield number 2 is gone!");
            Debug.Log("current opponent shieldCounter =" + oppShieldCounter);
            break;
        case 0:
            oppShield1.SetActive(false);
            Debug.Log("opponent Shield number 1 is gone!");
            Debug.Log("current opponent shieldCounter =" + oppShieldCounter);
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
