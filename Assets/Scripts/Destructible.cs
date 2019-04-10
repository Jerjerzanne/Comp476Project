using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField, Header("HealthBar")]
    public Image healthBar;

    #endregion

    #region Properties

    /// <summary>
    /// Current health of the destructible
    /// </summary>
    public int CurrentHealth { get; protected set; }

    #endregion

    #region Methods

    /// <summary>
    /// reduce the damage from the destructible
    /// </summary>
    /// <param name="damage"></param>
    public virtual void TakeDamage(int damage, Vector3 origin = default(Vector3))
    {
       
        CurrentHealth -= damage;
        //Debug.Log(this.name + " took " + damage);
        //Debug.Log(this.name + " has " + CurrentHealth);
        //Debug.Log(this.name + " has max " + maxHealth);
        if (healthBar != null)
        {
            healthBar.fillAmount = (float) CurrentHealth / maxHealth;
            //Debug.Log(this.name + " has " + healthBar.fillAmount);
        }
        
        if (CurrentHealth <= 0)
        {
            Die();
            if (gameObject.layer != 9 && gameObject.layer != 11)
            {
                Destroy(gameObject);
                Destructible food = Instantiate(foodPrefab, new Vector3(transform.position.x,0.50f, transform.position.z), Quaternion.Euler(new Vector3(180, 90, 90))).GetComponent<Destructible>();
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
        //Debug.Log(this.name + " has " + CurrentHealth);
    }
    #endregion

}
