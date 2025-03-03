using System;
using UnityEngine;
using UnityEngine.AI;

public class DamageDealer : MonoBehaviour
{
    public float Damage;
    public float AttackRate = 1f; // Частота ударов в секунду
    public Animator anim;
    private Animator animatorEnemy;
    private NavMeshAgent navMeshAgent;
    private Health playerHealth;
    private bool isAttacking = false;

    void Start()
    {
        animatorEnemy = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isAttacking)
        {
            playerHealth = collision.gameObject.GetComponent<Health>();
            StartAttacking(collision);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopAttacking();
        }
    }

    private void StartAttacking(Collider2D collision)
    {
        isAttacking = true;
        animatorEnemy.SetBool("Atack", true);
        navMeshAgent.speed = 0;
        InvokeRepeating(nameof(TakeDamage), 0f, 1 / AttackRate);
    }

    private void TakeDamage()
    {
        if (playerHealth != null && playerHealth.isAlive)
        {
            playerHealth.TakeDamage(Damage);
            Animator anim = playerHealth.gameObject.GetComponent<Animator>();
            if (playerHealth.isAlive == false)
            {
                anim.SetBool("Death", true);
                StopAttacking(); // Останавливаем атаку, если игрок умер
            }
            else
            {
                anim.SetBool("Hurt", true);
            }
        }
        else
        {
            StopAttacking(); // Останавливаем атаку, если игрок умер или health не найден
        }
    }

    private void StopAttacking()
    {
        isAttacking = false;
        CancelInvoke(nameof(TakeDamage));
        animatorEnemy.SetBool("Atack", false);
        navMeshAgent.speed = 0;
    }
}