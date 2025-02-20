using System;
using UnityEngine;
using UnityEngine.AI;

public class DamageDealer : MonoBehaviour
{
    public float Damage;
    public Animator anim;


    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(Damage);
            Animator anim = collision.gameObject.GetComponent<Animator>();
            if (collision.gameObject.GetComponent<Health>().isAlive == false)
            {
                anim.SetTrigger("Dead");               
            }
            else
            {
                anim.SetBool("Hurt", true);
            }
        }
        //if (collision != null)
        //{
        //    //
        //}

}


}
