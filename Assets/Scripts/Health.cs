using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

public int maxHealth = 3; //creates a value for our max health which is 3
public int currentHealth; //creates a value for our current health
public GameObject player; //grabs reference to our player 
public HealthBar healthBar; //grabs reference to our health bar 


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth); //sets our health bar at full 
    }

    // Update is called once per frame
    void Update()
    {
      
        if (currentHealth <= 0) //an if statment which checks whether or not or health is equal to or less than zero
        {
            Destroy(player);
        }
    }

    public void TakeDamage(int damage) //allows us to take damage by creating a take damage function
    {
        currentHealth -= damage; //allows our current health to be subtracted by whatever damage is dealt to us 
        healthBar.SetHealth(currentHealth);
    }

    IEnumerator ColliderCooldown(Collider2D Bob) //The damage collider of the player, which has a cooldown so that they don't instantly die
    {
        yield return new WaitForSeconds(1.0f);

        if (Bob)
        {
            Bob.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D Bob)
    {
        if (Bob.CompareTag("Enemy") && Bob.enabled && Bob.GetComponent<Enemy_Health>().currentHealth != 0)
        {
            Bob.enabled = false;
            StartCoroutine(ColliderCooldown(Bob));
            TakeDamage(1);
        }
       
    }
}
