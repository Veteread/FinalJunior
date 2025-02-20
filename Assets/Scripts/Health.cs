using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Timeline;

public class Health : MonoBehaviour
{   
	public float MaxHealth;
	public float currentHealth;
	public bool isAlive;

    private AudioSource audioKick;

    void Awake()
    {
        currentHealth = MaxHealth;
        isAlive = true;
        audioKick = GetComponent<AudioSource>();
    }    

   public void TakeDamage (float Damage)
    {
    	currentHealth -= Damage;
        Invoke("RecoverFromHit", 0.5f);
        // audioKick.Play();
        CheckisAlive();
    }

    private void CheckisAlive()
    {
    	if (currentHealth > 0)
    		isAlive = true;
    	else
    	{
    		isAlive = false;
    	}
    }
    public void RecoverFromHit()
    {
        Animator anim = gameObject.GetComponent<Animator>();
        anim.SetBool("Hurt", false);
        if (gameObject.CompareTag("Enemy"))
        {
            NavMeshAgent navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
            navMeshAgent.speed = 0.5f;
        }
    }
}
