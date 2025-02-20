using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameManager _gameManager;
    private Transform playerTransform;
    private Animator animator;
    private Vector2 EnemyVector = new Vector2(0f, 1f);
    private float angle;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = 0.5f;
        _gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        MoveEnemy();
    }

    public void MoveEnemy()
    {
        if (!_gameManager.isPaused)
        {
            if (playerTransform != null)
            {
                agent.destination = playerTransform.position;
                Vector3 direction = (playerTransform.position - transform.position).normalized;
                angle = Vector2.SignedAngle(EnemyVector, direction);
                animator.SetFloat("AngleToTarget", angle);
            }
        }
    }
}