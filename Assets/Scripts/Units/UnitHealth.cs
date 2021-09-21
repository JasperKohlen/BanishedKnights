using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UnitHealth : MonoBehaviour
{
    public UnitStats stats;

    #region Healthbar variables
    public float maxHealth;
    public Image healthBar;
    private float elapsed = 0f;
    private float pctHealth;
    private float updateSpeed = 0.2f;
    #endregion
    private void Start()
    {
        maxHealth = stats.health;
    }
    public void TakeDamage(float amount)
    {
        stats.health -= amount;
        StartCoroutine(ChangeHealthBarPct());
    }

    public abstract void Die();

    //Makes the healthbar lose health smoothly
    public IEnumerator ChangeHealthBarPct()
    {
        if (stats.health <= 0f)
        {
            Die();
        }

        float prePctChange = healthBar.fillAmount;

        pctHealth = stats.health / maxHealth;
        while (elapsed < updateSpeed)
        {
            elapsed += Time.deltaTime;
            healthBar.fillAmount = Mathf.Lerp(prePctChange, pctHealth, elapsed / updateSpeed);
            yield return null;
        }
        healthBar.fillAmount = pctHealth;
    }
}
