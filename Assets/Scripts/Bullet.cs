using UnityEngine;
using UnityEngine.AI;

public class Bullet : MonoBehaviour
{ 
    private Rigidbody2D _rigidbody;
    private GameManager _gameManager;

    public float speed = 5;
    public float angle;
    public float Damage;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    void OnEnable()
    {
        Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
       // _rigidbody.velocity = direction.normalized * speed;
    }

    void OnDisable()
    {
       // _rigidbody.velocity = Vector2.zero;
        BulletPool.Instance.ReturnBullet(this);
    }

    void FixedUpdate()
    {
        if (!_gameManager.isPaused)
        {
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
            _rigidbody.velocity = direction.normalized * speed;
        }
    }

    public void StopMovement()
    {
        _rigidbody.velocity = Vector2.zero;
    }

    private void OnBecameInvisible()
    {
        Invoke("Destroy", 0.25f);
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Health>().TakeDamage(Damage);
            Animator anim = collision.gameObject.GetComponent<Animator>();
            if (collision.gameObject.GetComponent<Health>().isAlive == false)
            {
                // anim.SetTrigger("Dead");
                if (collision.CompareTag("Enemy"))
                {
                    Enemy enemy = collision.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        EnemyPool.Instance.ReturnEnemy(enemy);
                    }
                    Destroy();
                }
            }
            else
            {
                anim.SetBool("Hurt", true);
                NavMeshAgent navMeshAgent = collision.GetComponent<NavMeshAgent>();
                if (navMeshAgent != null)
                {
                    navMeshAgent.speed = 0;
                }
                Destroy();
            }
        }
    }
}