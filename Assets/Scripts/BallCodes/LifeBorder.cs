using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBorder : MonoBehaviour
{
    [SerializeField]
    public string enemyTag = "Enemy";

    [SerializeField]
    private float baseLife = 10;

    [SerializeField]
    private float currentLife;

    public static LifeBorder instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentLife = baseLife;
    }
    public void TakeDamage(int damage)
    {
        currentLife -= damage;
        UpdateHealthBar();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(enemyTag))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }

    public void UpdateHealthBar()
    {
        if(currentLife > 0)
        {
            UIAssets.instance.healthBar.fillAmount = currentLife / baseLife;
        }
    }
}
