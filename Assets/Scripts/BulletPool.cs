using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance { get; private set; }

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private int initialSize = 10;

    private Queue<Bullet> availableBullets;

    void Awake()
    {
        Instance = this;
        InitializePool();
    }

    private void InitializePool()
    {
        availableBullets = new Queue<Bullet>(initialSize);

        for (int i = 0; i < initialSize; ++i)
        {
            var bullet = Instantiate(bulletPrefab, transform).GetComponent<Bullet>();
            bullet.gameObject.SetActive(false);
            availableBullets.Enqueue(bullet);
        }
    }

    public Bullet GetBullet()
    {
        if (availableBullets.Count == 0)
        {
            ExpandPool();
        }

        var bullet = availableBullets.Dequeue();
        bullet.gameObject.SetActive(true);
        return bullet;
    }

    private void ExpandPool()
    {
        var newBullet = Instantiate(bulletPrefab, transform).GetComponent<Bullet>();
        newBullet.gameObject.SetActive(false);
        availableBullets.Enqueue(newBullet);
    }

    public void ReturnBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        availableBullets.Enqueue(bullet);
    }
}