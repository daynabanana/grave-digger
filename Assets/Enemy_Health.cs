using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{

    public int maxHealth = 3; 
    public int currentHealth; 

    // Start is called before the first frame update
    void Start()
    {

        currentHealth = maxHealth;

    }

    IEnumerator EraseNPC()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die(); 
        }
    }

    void Die()
    {
        //Destroy(gameObject);
        gameObject.transform.Find("DeathParticles").gameObject.GetComponent<ParticleSystem>().Play();
        gameObject.transform.Find("Zombie_Visuals").gameObject.SetActive(false);

        StartCoroutine(EraseNPC());
        Debug.Log("Enemy died!"); 
    }
}
