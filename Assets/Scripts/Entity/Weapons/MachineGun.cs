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
            timer = 0;
            timeSinceFired = Time.time;
        }
    }

    public void FireSpray()
    {

    }
    #endregion
}