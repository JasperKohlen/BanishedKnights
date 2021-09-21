using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UnitHealth : MonoBehaviour
{
    public UnitStats stats;
    private float currentHealth;

    #region Healthbar variables
    [SerializeField] private Image healthBar;
    private float pctHealth;
    private float updateSpeed = 0.2f;
    #endregion
    private void Awake()
    {
        currentHealth = stats.maxHealth;
    }
    public void TakeDamage(float amount)
    {
        if (amount - stats.armor <= 0)
        {
            amount = 1;
        }
        else
        {
            amount -= stats.armor;
        }
        Debug.Log(gameObject.name + " Taking " + amount + " dmg");
        Debug.Log(gameObject.name + " now has " + currentHealth + " hp left");

        currentHealth -= amount;
        StartCoroutine(ChangeHealthBarPct());
    }

    public abstract void Die();

    //Makes the healthbar lose health smoothly
    public IEnumerator ChangeHealthBarPct()
    {
        if (currentHealth <= 0f)
        {
            Die();
        }

        float prePctChange = healthBar.fillAmount;
        float elapsed = 0f;

        pctHealth = currentHealth / stats.maxHealth;
        while (elapsed < updateSpeed)
        {
            elapsed += Time.deltaTime;
            healthBar.fillAmount = Mathf.Lerp(prePctChange, pctHealth, elapsed / updateSpeed);
            yield return null;
        }
        healthBar.fillAmount = pctHealth;
    }
}
