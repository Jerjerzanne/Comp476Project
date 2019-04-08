using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderProjectile : MonoBehaviour
{
    #region Constants

    private const int wallLayer = 13;
    private const int playerLayer = 9;
    private const int soldierLayer = 10;
    #endregion

    #region Variables

    private float _speed;
    private int _damage;

    #endregion

    #region Methods

    public void SetSpeed(float speed, int damage)
    {
        _speed = speed;
        _damage = damage;
    }

    #endregion

    #region Functions

    void Update()
    {
        this.transform.position += this.transform.forward * _speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == wallLayer)
        {
            Destroy(this.gameObject);
        }
        else if (other.gameObject.layer == playerLayer || other.gameObject.layer == soldierLayer)
        {
            other.gameObject.GetComponent<Destructible>().TakeDamage(this._damage);
            Destroy(this.gameObject);
            //Debug.Log("Projectile hit " + other.gameObject.name);
        }


    }

    #endregion
}
