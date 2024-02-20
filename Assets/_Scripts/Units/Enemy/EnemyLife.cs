using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class EnemyLife : MonoBehaviour
{
    [SerializeField]
    public string enemyTag = "Enemy";

    [SerializeField]
    private float baseLife = 10;
    public float currentLife;

    [SerializeField]
    private SpriteRenderer spriteRendererLifeArc;
    [SerializeField]
    public Material materialLifeArc;

    [SerializeField]
    private TypeUtility.Type type;

    [SerializeField]
    private List<TypeUtility.ObjectFromPoolTag> loot;

    [SerializeField]
    private float maxRandomLootQty;

    public static Action<string, Vector3, Quaternion> SpawnLootAction;

    [SerializeField]
    GameObject parent;

    Animator animator;
    private void Start()
    {
        
        //load arc
        spriteRendererLifeArc.material = Instantiate<Material>(spriteRendererLifeArc.material);
        materialLifeArc = spriteRendererLifeArc.material;
        currentLife = baseLife;
    }
    public void TakeDamage(float damage)
    {
        currentLife -= damage;
        //animator.SetTrigger("damaged");
        ChangeHealthArc((1 - currentLife / baseLife) * 360);
        CheckDeath();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag(enemyTag))
        {
            #region DealDamage
            ProjectileController projectile = collision.GetComponent<ProjectileController>();

            if (projectile != null)
            {
                TypeUtility.Type attackerType = projectile.GetProjectileType();
                float attackValue = projectile.GetBaseAttack();

                //Debug.Log("Attacker: " + attackerType.ToString());
                //Debug.Log("Defender: " + type.ToString());
                if (TypeUtility.IsInvincible(attackerType, type))
                {
                    Debug.Log("It is invincible (it wont take damage from the projectile) ");
                    attackValue = 0;
                }
                else if(TypeUtility.HasAdvantage(attackerType, type))
                {
                    Debug.Log("It has advantage (it will take double damage from the projectile");
                    attackValue *= 2; //double attack
                }
                else
                {
                    Debug.Log("It is not invincible and does not have advantage");
                }
                
                TakeDamage(attackValue);
                
            }
            #endregion

            #region DisableProjectile
            IPooledObject objectFromPool = collision.GetComponent<IPooledObject>();

            if(objectFromPool != null)
            {
                objectFromPool.OnObjectDisabled();
            }
            #endregion
        }
    }

    public void ChangeHealthArc(float angle)
    {
        materialLifeArc.SetFloat("_Arc1", angle);
    }

    public void CheckDeath()
    {
        if(currentLife <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        for(int i = 0; i < maxRandomLootQty; i++)
        {
            int indexR = UnityEngine.Random.Range(0, loot.Count);
            //TODO: NOT INSTANTIATE, BUT OBJECT POOLING
            SpawnLootAction?.Invoke(loot[indexR].ToString(), this.transform.position, Quaternion.identity);

        }
        parent.SetActive(false);
        
    }

    public void RestartLife()
    {
        currentLife = baseLife;
        ChangeHealthArc((1 - currentLife / baseLife) * 360);
    }
}
