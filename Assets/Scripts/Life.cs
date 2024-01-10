using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField]
    private float currentLife = 10f;

    private float maxLife;

    [SerializeField]
    private HealthBar healthBar;

    private void Start()
    {
        maxLife = currentLife;
    }

    public void TakeDamage(float damage)
    {
        currentLife -= damage;
        if(healthBar != null )
        {
            healthBar.UpdateHealthBar(currentLife, maxLife);
        }
        

    }
    
}
