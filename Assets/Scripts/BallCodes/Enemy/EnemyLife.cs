using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyLife : MonoBehaviour
{
    [SerializeField]
    public string enemyTag = "Enemy";

    [SerializeField]
    private float baseLife = 10;
    private float currentLife;

    [SerializeField]
    public GameObject healthObj;
    private Material healthMat;

    [SerializeField]
    private TypeUtility.Type type;

    private void Start()
    {
        healthObj.GetComponent<SpriteRenderer>().material = Instantiate<Material>(healthObj.GetComponent<SpriteRenderer>().material);
        healthMat = healthObj.GetComponent<SpriteRenderer>().material;
        currentLife = baseLife;
    }
    public void TakeDamage(float damage)
    {
        currentLife -= damage;
        ChangeHealthArc((1 - currentLife / baseLife) * 360);
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

                Debug.Log("Attacker: " + attackerType.ToString());
                Debug.Log("Defender: " + type.ToString());
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
        healthMat.SetFloat("_Arc1", angle);
    }
}
