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

    [SerializeField]
    public GameObject healthObj;

    private Material healthMat;

    Animator animator;

    private void Start()
    {
        healthObj.GetComponent<SpriteRenderer>().material = Instantiate<Material>(healthObj.GetComponent<SpriteRenderer>().material);
        healthMat = healthObj.GetComponent<SpriteRenderer>().material;

        currentLife = baseLife;

        animator = GetComponent<Animator>();    
    }
    public void AlterLife(int damage)
    {
        currentLife -= damage;
        if(currentLife > baseLife)
        {
            currentLife = baseLife;
        }

        UpdateHealthBar();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(enemyTag))
        {
            AlterLife(1);

            #region DisableProjectile
            IPooledObject objectFromPool = collision.GetComponent<IPooledObject>();

            if (objectFromPool != null)
            {
                objectFromPool.OnObjectDisabled();
            }
            #endregion
        }
    }

    public void UpdateHealthBar()
    {
        if(currentLife > 0 && currentLife < baseLife)
        {
            ChangeHealthArc((1 - currentLife / baseLife) * 360);
            animator.SetTrigger("damaged");
        }
    }

    public void ChangeHealthArc(float angle)
    {
        healthMat.SetFloat("_Arc1", angle);
    }
}
