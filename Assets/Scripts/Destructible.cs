using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Destructible game entity
/// </summary>
public class Destructible : MonoBehaviour
{
    #region Constants

    #endregion

    #region Variables

    //Editor variables
    [SerializeField, Header("Destructible")]
    public GameObject foodPrefab;
    public int maxHealth;
    public bool Death;
    #endregion

    #region Properties

    /// <summary>
    /// Current health of the destructible
    /// </summary>
    public int CurrentHealth { get; private set; }
    
    #endregion

    #region Methods

    /// <summary>
    /// reduce the damage from the destructible
    /// </summary>
    /// <param name="damage"></param>
    public virtual void TakeDamage(int damage)
    {
        Debug.Log(this.name + " took " + damage);
        CurrentHealth -= damage;
        if(CurrentHealth <= 0)
        {
            Death = true;
            Die();
            if(gameObject.layer == 9)
            {

            }
            else
            {
            Destroy(gameObject);
            Destructible food = Instantiate(foodPrefab, new Vector3(transform.position.x, transform.position.y - 0.75f, transform.position.z), Quaternion.Euler(new Vector3(180, 90, 90))).GetComponent<Destructible>();
            }
            
        }
    }

    /// <summary>
    /// the destructible changes layer to death layer
    /// </summary>
    protected virtual void Die()
    {
        Debug.Log(this.name + " died.");
    }

    public bool IsDead()
    {
        if (CurrentHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region Functions

    protected void Awake()
    {
        CurrentHealth = maxHealth;
        Death = false;
    }
    #endregion

}
