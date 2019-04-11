using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Gun
{
    #region Variables


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
        // Currently does the same as FireSingle()
        if (!locked)
        {
            timer = Time.time - timeSinceFired;
        }

        if (timer > 1 / rateOfFire)
        {
            Projectile bullet = Instantiate(bulletPrefab, this.transform.position + this.transform.forward * offset, this.transform.rotation).GetComponent<Projectile>();
            bullet.SetSpeed(bulletSpeed, damage);
            if (ammoCount > 0)
                ammoCount--;
            timer = 0;
            timeSinceFired = Time.time;
        }
    }
    #endregion
}