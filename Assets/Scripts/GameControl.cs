using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static CollisionChecker;
//using static MainCanvas;
public class GameControl : MonoBehaviour
{
    public CollisionChecker ammoPackCollisionChecker;
    public CollisionChecker grenadeCollisionChecker;
    //public MainCanvas tutorialStatus;
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
    private bool grenadeCollisionStatus;
    private int playerGrenadeCounter = 2;
    private int oppGrenadeCounter = 2;
    public GameObject grenadePrefab;
    public GameObject playerGrenade1;
    public GameObject playerGrenade2;
    public GameObject oppGrenade1;
    public GameObject oppGrenade2;
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
    private bool ammoPackCollisionStatus;

    public void ReStart()
    {
        //game initialization
        Debug.Log("Game reinitialized!");
        playerShieldCounter = 3;
        oppShieldCounter = 3;
        playerGrenadeCounter = 2;
        oppGrenadeCounter = 2;
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
        playerGrenade1.SetActive(true);
        playerGrenade2.SetActive(true);
        oppGrenade1.SetActive(true);
        oppGrenade2.SetActive(true);

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
        playerGrenadeCounter = 2;
        oppGrenadeCounter = 2;
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
        //if(tutorialStatus.StartTutorial()){
        PlayerShieldAction();
        OpponentShieldAction();
        PlayerUpdateAmmoCountUI();
        OpponentUpdateAmmoCOuntUI();
        OppenentReloadAction();
        ammoPackCollisionStatus = ammoPackCollisionChecker.GetAmmoPackCollsionStatus();
        grenadeCollisionStatus = grenadeCollisionChecker.GetGrenadeCollisionStatus();

        PlayerGrenadeUIUpdate();
        OpponentGrenadeUIUpdate();
        //}
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
        if(currentOppShieldHp <= 0) {
            oppShield.SetActive(false);
            isOppShieldActive = false;
            currentOppShieldHp = maxShieldHp;
            Debug.Log("shield is down; shield is down due to damage taken!");
        }

        if(currentOppHp <= 0) {
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
        if(currentPlayerShieldHp <=0 ) {
            playerShield.SetActive(false);
            isPlayerShieldActive = false;
            currentPlayerShieldHp = maxShieldHp;
            Debug.Log("shield is down; shield is down due to damage taken!");
        }

        if(currentPlayerHp <= 0) {
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
        
        if(ammoPackCollisionStatus)
        {
            Debug.Log("Gamecontrol module detects an ammo pack collsion");
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
    public void PlayerGrenadeUIUpdate()//this is a normal throw
    {
        // if(Input.GetMouseButtonDown(0) && playerGrenadeCounter > 0)
        // {
        //     GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        //     Rigidbody rb = grenade.GetComponent<Rigidbody>();
        //     rb.AddForce(transform.forward * throwForce*2, ForceMode.VelocityChange);
        //     Debug.Log("grenade throwed");
        //     playerGrenadeCounter--;
        // }
        switch (playerGrenadeCounter)
        {
            case 1:
                playerGrenade2.SetActive(false);
                Debug.Log("playerGrenade2 gone, playerGrenadeCounter = 1");
                break;
            case 0:
                playerGrenade1.SetActive(false);
                Debug.Log("playerGrenade1 gone, playerGrenadeCounter = 0");
                break;
            default:
                break;
        }
    }

    public void OpponentGrenadeUIUpdate()
    {
        switch (oppGrenadeCounter)
        {
            case 1:
                oppGrenade2.SetActive(false);
                Debug.Log("oppGrenade2 gone, oppGrenadeCounter = 1");
                break;
            case 0:
                oppGrenade1.SetActive(false);
                Debug.Log("oppGrenade1 gone, oppGrenadeCounter = 0");
                break;
            default:
                break;
        }    
    }

    Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        //define the distance x and y direction first
        Vector3 distance = target - origin;
        Vector3 distnaceXZ = distance;
        distnaceXZ.y = 0f; //set Y force to zero, and only keeping the X and Z component

        //creat a float variable to represent the distance
        float horizontalDistance = distnaceXZ.magnitude;
        float verticalDistance = distance.y;

        //calculating velocity using projectil formula
        //
        float horizontalVelocity = horizontalDistance / time;
        float verticalVelocity = verticalDistance / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        //result to be returned
        Vector3 result = distnaceXZ.normalized;
        result *= horizontalVelocity;
        result.y = verticalVelocity;

        return result;
    }

    public void PlayerLaunchGrenade()
    {
        //Ray camRay = arCamera.ScreenPointToRay(transform.position);
        RaycastHit hit;

        //must implement plane detection then set the plane so that the raycast can work
        if(Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit) && playerGrenadeCounter > 0)
        {
            //cursor.SetActive(true);
            //cursor.transform.position = hit.point + Vector3.up * 0.1f;

            //target vector = Raycast hit point
            //start vector = shoopint position
            //delay for grenade set to 2 sec
            Vector3 initialVelocity = CalculateVelocity(hit.point, transform.position, 2f);
   
            Rigidbody grenadePrefabRb = grenadePrefab.GetComponent<Rigidbody>();
            Rigidbody obj = Instantiate(grenadePrefabRb, transform.position, Quaternion.identity);
            obj.velocity = initialVelocity;
            Debug.Log("PlacementDemo.LaunchGrenade.ifStatement has run");
            playerGrenadeCounter--;


            if(isOppShieldActive)
            {
                currentOppShieldHp -= grenadeDamage;
                oppShieldHp.fillAmount = (float)currentOppShieldHp / (float)maxShieldHp;           
                Debug.Log("opponent gets hit by grenade but shield is up; update oppShieldHp accordingly");
            } else
            {
                currentOppHp -= grenadeDamage;
                oppHp.fillAmount = (float)currentOppHp / (float)maxHp;           
                Debug.Log("opponent is hit by grenade and is without shield; update oppHp accordingly");
            }

            //actions for shield
            if(currentOppShieldHp <= 0) 
            {
                oppShield.SetActive(false);
                isOppShieldActive = false;
                currentOppShieldHp = maxShieldHp;
                Debug.Log("opponent shield is down; shield is down due to damage taken!");
            }

            if(currentOppHp <= 0) {
                Debug.Log("opponent died!");
            }

        } else
        {
            //cursor.SetActive(false);
            if(playerGrenadeCounter > 0)
            {
                GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
                Rigidbody rb = grenade.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * throwForce*2, ForceMode.VelocityChange);
                Debug.Log("grenade throwed");
                playerGrenadeCounter--;
            }
        }
        Debug.Log("PlacementDemo.LaunchGrenade has run");
    }

    //for grenade throw by opponent, alwasy assume it is a hit
    //will be adjust later when external comms comes in
    public void OpponentLaunchGrenade()
    {
        oppGrenadeCounter--;
        Debug.Log("opponent throws a grenade");


        if(oppGrenadeCounter < 0)
        {
            return;
        }

        if(isPlayerShieldActive)
        {
                currentPlayerShieldHp -= grenadeDamage;
                playerShieldHp.fillAmount = (float)currentPlayerShieldHp / (float)maxShieldHp;           
                Debug.Log("player gets hit by grenade but shield is up; update playerShieldHp accordingly");
        } else
        {
            currentPlayerHp -= grenadeDamage;
            playerHp.fillAmount = (float)currentPlayerHp / (float)maxHp;           
            Debug.Log("player is hit by grenade and is without shield; update playerHp accordingly");
        }

        //actions for shield
        if(currentPlayerShieldHp <= 0) {
            playerShield.SetActive(false);
            isPlayerShieldActive = false;
            currentPlayerShieldHp = maxShieldHp;
            Debug.Log("shield is down; shield is down due to damage taken!");
        }

        if(currentPlayerHp <= 0) {
            Debug.Log("player died!");
        }
    }

}
