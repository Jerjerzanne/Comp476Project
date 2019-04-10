using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    #region Variables

    [Header("Shotgun Fire")]
    public float spreadAngle;
    public int bulletCount;
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

        if (timer > 1 / rateOfFire)
        {
            FireSpray();
            timer = 0;
            timeSinceFired = Time.time;
        }
    }

    override
    public void FireBurst()
    {
        // Currently does the same as FireSingle()
        if (!locked)
        {
            timer = Time.time - timeSinceFired;
        }

        if (timer > 1 / rateOfFire)
        {
            FireSpray();
            timer = 0;
            timeSinceFired = Time.time;
        }
    }

    public void FireSpray()
    {
        float angleDeviation = spreadAngle / (bulletCount + 1);
        Vector3 currentAngle = new Vector3(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y - spreadAngle / 2, this.transform.rotation.eulerAngles.z);

        for (int i = 0; i < bulletCount; i++)
        {
            currentAngle.y += angleDeviation;
            Projectile bullet = Instantiate(bulletPrefab, this.transform.position + this.transform.forward * offset, Quaternion.Euler(currentAngle)).GetComponent<Projectile>();
            bullet.SetSpeed(bulletSpeed, damage);
        }
    }
    #endregion
}