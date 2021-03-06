﻿using System.Collections;
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
    protected bool locked;
    public int maxAmmo;
    public int ammoCount;
    public int bulletCount;

    [Header("Burst fire")]
    public int burstSize;

    protected float timer;
    protected float timeSinceFired;
    // Gun sound

    #endregion

    #region Methods

    void Awake()
    {
        ammoCount = maxAmmo;
    }

    override
    public void FireSingle()
    {
        if (!locked)
        {
            timer = Time.time - timeSinceFired;
        }

        if (timer >  1/rateOfFire)
        {
            Projectile bullet = Instantiate(bulletPrefab, this.transform.position + this.transform.forward * offset, this.transform.rotation).GetComponent<Projectile>();
            bullet.SetSpeed(bulletSpeed, damage);
            if (ammoCount > 0)
                ammoCount--;
            timer = 0;
            timeSinceFired = Time.time;
        }
    }

    override
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
            GameObject playerBullet = Instantiate(bulletPrefab, spawnPos, transform.rotation);
            Projectile pScript = playerBullet.GetComponent<Projectile>();
            pScript.SetSpeed(bulletSpeed, damage);
            //playerBullet.SetSpeed(bulletSpeed, damage);

            if (ammoCount > 0)
                ammoCount--;
            yield return new WaitForSeconds(bulletDelay / burstSize);
        }
        timeSinceFired = Time.time;
        locked = false;
    }
    #endregion
}
