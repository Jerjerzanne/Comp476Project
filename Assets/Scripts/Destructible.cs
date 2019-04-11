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

    public enum Sizes { Small, Medium, Large };

    private GameObject droppedFood;

    //Editor variables
    [SerializeField, Header("Destructible")]
    public GameObject foodPrefab;
    public int maxHealth;
    public Sizes size;

    [SerializeField, Header("HealthBar")]
    public Image healthBar;

    #endregion

    #region Properties

    /// <summary>
    /// Current health of the destructible
    /// </summary>
    public int CurrentHealth { get; protected set; }

    /// <summary>
    /// Current growth level of the player or AI entity
    /// </summary>
    public int CurrentGrowth { get; set; }
    public bool damaged { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// reduce the damage from the destructible
    /// </summary>
    /// <param name="damage"></param>
    public virtual void TakeDamage(int damage, Vector3 origin = default(Vector3))
    {
        damaged = true;
        CurrentHealth -= damage;
        Debug.Log(this.name + " took " + damage);
        //Debug.Log(this.name + " has " + CurrentHealth);
        //Debug.Log(this.name + " has max " + maxHealth);
        if (healthBar != null)
        {
            healthBar.fillAmount = (float) CurrentHealth / maxHealth;
            //Debug.Log(this.name + " has " + healthBar.fillAmount);
        }
        
        if (IsDead())
        {
            Die();
        }
    }

    /// <summary>
    /// the destructible changes layer to death layer
    /// </summary>
    protected virtual void Die()
    {
        //Debug.Log(this.name + " died.");
        if (gameObject.layer != 9 && gameObject.layer != 11)
        {
            if(droppedFood == null)
                droppedFood = Instantiate(foodPrefab, new Vector3(transform.position.x, 0.50f, transform.position.z), Quaternion.Euler(new Vector3(180, 90, 90)));

            Destroy(gameObject);
        }
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
        CurrentGrowth = (int)size;
        //Debug.Log(this.name + " has " + CurrentHealth);
    }
    #endregion

    public void HealToFull()
    {
        CurrentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)CurrentHealth / maxHealth;
        }
    }

}
