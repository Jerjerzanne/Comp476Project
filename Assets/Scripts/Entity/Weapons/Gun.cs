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

    private float timer;
    private float timeSinceFired;
    // Gun sound

    #endregion

    #region Methods

    public override  void FireSingle()
    {
        timer += Time.time - timeSinceFired; 
        if (timer >  1/rateOfFire && !locked)
        {
            Projectile bullet = Instantiate(bulletPrefab, this.transform.position + this.transform.forward * offset, this.transform.localRotation).GetComponent<Projectile>();
            bullet.SetSpeed(bulletSpeed, damage);
            timer = 0;
        }

        timeSinceFired = Time.time;
    }

    public override void FireBurst()
    {
        if (!locked)
        {
            locked = true;
            StartCoroutine("Burst");
        }
    }

    public override IEnumerator Burst()
    {
        float bulletDelay = 1 / rateOfFire;

        for (int i = 0; i < burstSize; i++)
        {
            Vector3 spawnPos = this.transform.position + this.transform.forward * offset;
            GameObject playerBullet = Instantiate(bulletPrefab, spawnPos, transform.localRotation);
            Projectile pScript = playerBullet.GetComponent<Projectile>();
            pScript.SetSpeed(bulletSpeed, damage);
            //playerBullet.SetSpeed(bulletSpeed, damage);
            yield return new WaitForSeconds(bulletDelay);
        }
        locked = false;
    }
    #endregion
}
