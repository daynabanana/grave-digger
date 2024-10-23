using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public float attackRange = 0.5f;
    public Transform attackPoint;

    public int attackDamage = 1;
    public LayerMask Layer;

    bool OnCooldown;
    public float AttackCooldownTime = 1f;

    //Attack Cooldown
    IEnumerator AttackCooldown()
    {
        OnCooldown = true;
        yield return new WaitForSeconds(AttackCooldownTime);

        OnCooldown = false;
        StopCoroutine(AttackCooldown());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!OnCooldown)
            {
                Attack();
                StartCoroutine(AttackCooldown());
            }
        }
    }

    void Attack()
    {
        //Play an attack animation 
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, Layer);

        foreach (Collider2D enemy in hitEnemies)
        {
            print(enemy.name);
            if (enemy.GetComponent<Enemy_Health>())
            {
                enemy.GetComponent<Enemy_Health>().TakeDamage(attackDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

