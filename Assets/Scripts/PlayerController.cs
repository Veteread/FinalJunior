using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip stepSound;
    public AudioClip reloadSound;
    public Transform firePoint;
    public float moveSpeed = 5f;
    private WeaponReload _weaponReload;

    private Vector3 targetPosition;
    private Vector3 moveDirection;
    private Vector2 mousePos;
    private Vector2 direction;
    private Animator _animator;
    private AudioSource _audioSource;
    private GameManager _gameManager;
    private Vector2 HeroVector = new Vector2(0f, 1f);
    private float angleMove;
    private float angleBullet;
    private int currentAmmo = 30;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _gameManager = FindObjectOfType<GameManager>();
        _weaponReload = GetComponent<WeaponReload>();
    }

    public void Update()
    {
        Shoot();
        Move();
        HandReload();
        if (_gameManager.isPaused)
        {
            return;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            _animator.SetTrigger("Death");
        }
    }

    public void Move()
    {
        if (Input.GetButtonDown("Fire2"))
        {            
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = transform.position.z;
            moveDirection = (targetPosition - (Vector3)transform.position);
            angleMove = Vector2.SignedAngle(HeroVector, moveDirection);
            _audioSource.PlayOneShot(stepSound);
            _animator.SetFloat("Angle", angleMove);
            _animator.SetFloat("Speed", moveDirection.magnitude);
        }
        if (targetPosition != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        }
        if (targetPosition == transform.position)
        {
            _animator.SetFloat("Speed", 0f);
        } 
    }

    public void Shoot()
    {
        if (!_gameManager.isPaused)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                currentAmmo = _weaponReload.currentAmmoInMagazine;
                if (currentAmmo > 0 && _weaponReload.isReloading == false)
                {
                    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    direction = (mousePos - (Vector2)firePoint.position).normalized;
                    var bullet = BulletPool.Instance.GetBullet();
                    bullet.transform.position = firePoint.position;
                    bullet.angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    angleBullet = bullet.angle;
                    bullet.gameObject.SetActive(true);
                    _animator.SetBool("Fire", true);
                    _animator.SetFloat("Speed", 0f);
                    _animator.SetFloat("BulletAngle", bullet.angle);
                    targetPosition = transform.position;
                    _weaponReload.currentAmmoInMagazine --;
                    _weaponReload.ReloadText();
                }
                else
                {
                    _audioSource.PlayOneShot(reloadSound);
                    _weaponReload.Reload();
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {           
            _animator.SetBool("Fire", false);
        }
    }

    public void HandReload()
    {
        if (!_gameManager.isPaused)
        {
            if (Input.GetButtonDown("Fire3"))
            {
                _audioSource.PlayOneShot(reloadSound);
                _weaponReload.Reload();
            }
        }
    }
}