using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameControl : MonoBehaviour
{
    public GameObject shield;
    public GameObject shieldHp;
    //following is for Hp
    public int maxHp = 100;
    private int currentHp;
    //following is for bullet
    private int bulletDamage = 10;
    //following is for grenade
    private int grenadeDamage = 30;
    public float grenadeCountDown = 2.0f;
    //following is for shield
    private bool isShieldActive;
    public float shieldCountDown = 3.0f; //this is to be updated later to 10sec, 3sec is for testing purpose
    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        shield.SetActive(false);
        isShieldActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isShieldActive) 
        {
            shieldCountDown -= Time.deltaTime;
            if(shieldCountDown < 0)
            {
                shield.SetActive(false);
                isShieldActive = false;
                shieldCountDown = 3.0f;

            }
        }
    }

    public void ActivateSheild()
    {
        shield.SetActive(true);
        Debug.Log("shield count -1");
        isShieldActive = true;
    }



}
