using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public static healthBar Instance {get; private set;}
    private void Awake() {
        Instance = this;
    }
    public bool CurrentHealthMin() {
        return currHealth <= 2;
    }
    public Image healtbarImg;
    private float currHealth;
    private const int maxHealth = 10;
    private const int minHealth = 0;

    void Start() {
        currHealth = maxHealth;
    }

    public void takeDamage(int dmg) 
    {
        currHealth -= dmg;
        healtbarImg.fillAmount = currHealth / maxHealth;
    }

    public void addHealth(int extraHealth) {
        currHealth += extraHealth;

        if(currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }
        
        healtbarImg.fillAmount = currHealth / maxHealth;
    }

}
