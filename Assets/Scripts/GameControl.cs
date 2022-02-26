using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameControl : MonoBehaviour
{
    public GameObject shield;
    public GameObject shieldHp;
    public int maxHp = 100;
    private int currentHp;
    private int bulletDamage = 10;
    private int grenadeDamage = 30;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        shield.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateSheild()
    {
        shield.SetActive(true);
        Debug.Log("shield count -1");
    }

    

}
