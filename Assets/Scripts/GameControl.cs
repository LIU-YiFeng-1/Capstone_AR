using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static CollisionChecker;
public class GameControl : MonoBehaviour
{
    public CollisionChecker collisionChecker;
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
    //the following is for reload and ammo
    public Text playerAmmoCount;
    public Text oppAmmoCount;
    private int initialAmmoCount = 6;
    private int playerAmmoCountValue;
    private int oppAmmoCountValue;
    public GameObject airCraft;
    public GameObject ammoPack;
    public Rigidbody ammoPackReferencePoint;
    private bool isReloadEnable;
    public float airCraftForce = 55.0f;
    private Vector3 ammoPackInitialLocaiton;
    private bool collisionStatus;

    public void ReStart(){
        //game initialization
        Debug.Log("Game reinitialized!");
        playerShieldCounter = 3;
        oppShieldCounter = 3;
        currentPlayerHp = maxHp;
        currentPlayerShieldHp = maxShieldHp;
        playerShieldCountDown = shieldCountDownValue;
        playerAmmoCountValue = initialAmmoCount;
        currentOppHp = maxHp;
        currentOppShieldHp = maxShieldHp;
        oppShieldCountDown = shieldCountDownValue;
        oppAmmoCountValue = initialAmmoCount;
        playerHp.fillAmount = 1;
        oppHp.fillAmount = 1;

        playerShield1.SetActive(true);
        playerShield2.SetActive(true);
        playerShield3.SetActive(true);
        oppShield1.SetActive(true);
        oppShield2.SetActive(true);
        oppShield3.SetActive(true);

        playerShield.SetActive(false);
        isPlayerShieldActive = false;
        oppShield.SetActive(false); //set false if using shield button; set true if testing for shield hp
        isOppShieldActive = false;
        isReloadEnable = false;
        Debug.Log("game restarted");
    }
    // Start is called before the first frame update
    void Start()
    {
        //game initialization
        Debug.Log("Game initialized!");
        playerShieldCounter = 3;
        oppShieldCounter = 3;
        currentPlayerHp = maxHp;
        currentPlayerShieldHp = maxShieldHp;
        playerShieldCountDown = shieldCountDownValue;
        playerAmmoCountValue = initialAmmoCount;
        currentOppHp = maxHp;
        currentOppShieldHp = maxShieldHp;
        oppShieldCountDown = shieldCountDownValue;
        oppAmmoCountValue = initialAmmoCount;
        //ammoPackInitialLocaiton = ammoPack.transform.position;

        playerShield.SetActive(false);
        isPlayerShieldActive = false;
        oppShield.SetActive(false); //set false if using shield button; set true if testing for shield hp
        isOppShieldActive = false;
        isReloadEnable = false;
    }
    // Update is called once per frame
    void Update()
    {
        PlayerShieldAction();
        OpponentShieldAction();
        PlayerUpdateAmmoCountUI();
        OpponentUpdateAmmoCOuntUI();
        OppenentReloadAction();
        collisionStatus = collisionChecker.GetCollsionStatus();
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
    //all button triger will be replaced by incoming signal from the external comms
    public void PlayerShoots() 
    {   
        RaycastHit hit;
        Debug.Log("player fired, gun is being pressed, opponent gets hit");
        playerAmmoCountValue--;

        if(playerAmmoCountValue < 0)
        {
            return;
        }

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
            if(Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
            {
                currentOppHp -= bulletDamage;
                oppHp.fillAmount = (float)currentOppHp / (float)maxHp;           
                Debug.Log("Raycast hit player!");
            }
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
    public void OpponentShoots() 
    {   
        Debug.Log("opponent fired, gun is being pressed, player gets hit");
        oppAmmoCountValue--;

        if(oppAmmoCountValue < 0)
        {
            return;
        }

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
    public void PlayerReload()
    {
        //just play reloading sound and reloading gun animation
        if(playerAmmoCountValue <= 0)
        {
            Debug.Log("player ammo count is zero, player reloading");
            Debug.Log("player reloading animation playing... ");
            playerAmmoCountValue = initialAmmoCount;
            Debug.Log("player reloaded with 6 ammo");
        }

    }
    public void OpponentReload()
    {   
        if(oppAmmoCountValue <= 0)
        {
            isReloadEnable = true;
        }
    }

    public void OppenentReloadAction()
    {   
        //animate an plane and drop supply
        if(isReloadEnable == true)
        {   
            Debug.Log("opponent ammo count is zero, opponennt reloading");
            Debug.Log("opponent reloading animation playing....");
            GameObject obj = Instantiate(airCraft, transform.position, airCraft.transform.rotation); 
            //GameObject obj = Instantiate(airCraft, transform.position, transform.rotation); 
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            //rb.AddForce(0,10,55,ForceMode.VelocityChange); //this addForce will not allow the aircraft to be instantiate with respect to the cam view
            rb.AddForce(transform.forward * airCraftForce, ForceMode.VelocityChange);
            rb.AddForce(transform.up * 8.0f, ForceMode.VelocityChange);
            oppAmmoCountValue = initialAmmoCount;
            Debug.Log("opponent reloaded with 6 ammo");

            GameObject ammoPackObj = Instantiate(ammoPack, ammoPackReferencePoint.position, ammoPackReferencePoint.rotation);
            Destroy(ammoPackObj, 3.5f);
        }
        
        if(collisionStatus)
        {
            Debug.Log("Gamecontrol module detects a collsion");
        }
            
        isReloadEnable = false;
    }
    private void PlayerUpdateAmmoCountUI()
    {
        switch (playerAmmoCountValue)
        {
            case 6:
                playerAmmoCount.text = "6/6";
                Debug.Log("update UI for ammo count to 6/6");
                break;
            case 5:
                playerAmmoCount.text = "5/6";
                Debug.Log("update UI for ammo count to 5/6");
                break;
            case 4:
                playerAmmoCount.text = "4/6";
                Debug.Log("update UI for ammo count to 4/6");
                break;
            case 3:
                playerAmmoCount.text = "3/6";
                Debug.Log("update UI for ammo count to 3/6");
                break;
            case 2:
                playerAmmoCount.text = "2/6";
                Debug.Log("update UI for ammo count to 2/6");
                break;
            case 1:
                playerAmmoCount.text = "1/6";
                Debug.Log("update UI for ammo count to 1/6");
                break;
            case 0:
                playerAmmoCount.text = "0/6";
                Debug.Log("update UI for ammo count to 0/6");
                break;
            default:
                break;
        }
    }
    private void OpponentUpdateAmmoCOuntUI()
    {
        switch (oppAmmoCountValue)
        {
            case 6:
                oppAmmoCount.text = "6/6";
                Debug.Log("update UI for ammo count to 6/6");
                break;
            case 5:
                oppAmmoCount.text = "5/6";
                Debug.Log("update UI for ammo count to 5/6");
                break;
            case 4:
                oppAmmoCount.text = "4/6";
                Debug.Log("update UI for ammo count to 4/6");
                break;
            case 3:
                oppAmmoCount.text = "3/6";
                Debug.Log("update UI for ammo count to 3/6");
                break;
            case 2:
                oppAmmoCount.text = "2/6";
                Debug.Log("update UI for ammo count to 2/6");
                break;
            case 1:
                oppAmmoCount.text = "1/6";
                Debug.Log("update UI for ammo count to 1/6");
                break;
            case 0:
                oppAmmoCount.text = "0/6";
                Debug.Log("update UI for ammo count to 0/6");
                break;
            default:
                break;
        }
    }
    public void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
        Debug.Log("grenade throwed");
    }

}
