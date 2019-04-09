using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    #region Variables

    [SerializeField, Header("Gun")]
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float rateOfFire;
    public float offset;
    private bool locked;

    [Header("Burst fire")]
    public int burstSize;

    [Header("shotgun fire")]
    public int angleDeviation;
    public int bulletCount;

    private float timer;
    private float timeSinceFired;
    // Gun sound

    #endregion

    #region Methods

    override
    public void FireSingle()
    {
        if (!locked)
        {
            timer = Time.time - timeSinceFired;
        }

        if (timer >  1/rateOfFire)
        {
            Projectile bullet = Instantiate(bulletPrefab, this.transform.position + this.transform.forward * offset, this.transform.localRotation).GetComponent<Projectile>();
            bullet.SetSpeed(bulletSpeed, damage);
            timer = 0;
            timeSinceFired = Time.time;
        }

        
    }

    public void FireBurst()
    {
        if (!locked)
        {
        timer = Time.time - timeSinceFired;
        }

        if (timer > 1 / rateOfFire)
        {
            timer = 0;
            locked = true;
            StartCoroutine("Burst");
        }
    }

    public IEnumerator Burst()
    {
        float bulletDelay = 1 / rateOfFire;

        for (int i = 0; i < burstSize; i++)
        {
            Vector3 spawnPos = this.transform.position + this.transform.forward * offset;
            GameObject playerBullet = Instantiate(bulletPrefab, spawnPos, transform.localRotation);
            Projectile pScript = playerBullet.GetComponent<Projectile>();
            pScript.SetSpeed(bulletSpeed, damage);
            //playerBullet.SetSpeed(bulletSpeed, damage);
            yield return new WaitForSeconds(bulletDelay / burstSize);

        }
        timeSinceFired = Time.time;
        locked = false;


    }

    public void FireSpray()
    {

    }
    #endregion
}
