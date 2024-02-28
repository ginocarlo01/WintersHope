using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class EnemyLife : MonoBehaviour, IHit
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

    public static Action<SFX> damageActionSFX;
    [SerializeField] protected SFX damageSFX;

    public static Action<SFX> deathActionSFX;
    [SerializeField] protected SFX deathSFX;

    [SerializeField]
    private float timeToShowDamaged = .7f;

    [SerializeField]
    private SpriteRenderer[] lifeSprites;

    [Range(0, 1)]
    [SerializeField]
    private float transparecencyValue = .0f;
    [SerializeField]
    private float transparecencyFadeTime = .4f;

    public event Action OnHit;

    private void Start()
    {
        DisableLifeSprites();
        //load arc
        spriteRendererLifeArc.material = Instantiate<Material>(spriteRendererLifeArc.material);
        materialLifeArc = spriteRendererLifeArc.material;
        currentLife = baseLife;
    }

    public void SetBaseLife(int life)
    {
        baseLife = life;
    }
    public void TakeDamage(float damage)
    {
        currentLife -= damage;
        if(!CheckDeath()) 
        { 
            damageActionSFX?.Invoke(damageSFX); 
            EnableLifeSprites();
            StartCoroutine(WaitToShowDamage());
        }
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

    public bool CheckDeath()
    {
        if(currentLife <= 0)
        {
            
            Die();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Die()
    {
        for(int i = 0; i < loot.Count; i++)
        {
            SpawnLootAction?.Invoke(loot[i].ToString(), this.transform.position, Quaternion.identity);

        }
        OnHit?.Invoke();
        deathActionSFX?.Invoke(deathSFX);
        parent.SetActive(false);
        
    }

    public void RestartLife()
    {
        currentLife = baseLife;
        ChangeHealthArc((1 - currentLife / baseLife) * 360);
    }

    private IEnumerator WaitToShowDamage()
    {
        yield return new WaitForSeconds(timeToShowDamaged);
        DisableLifeSprites();
    }

    private void EnableLifeSprites()
    {
        foreach(SpriteRenderer sprite in lifeSprites)
        {
            // s.enabled = true;
            StartCoroutine(FadeObj(sprite, transparecencyFadeTime, sprite.color.a, 1));
        }
    }
    private void DisableLifeSprites()
    {
        foreach (SpriteRenderer sprite in lifeSprites)
        {
            //s.enabled = false;
            StartCoroutine(FadeObj(sprite, transparecencyFadeTime, sprite.color.a, transparecencyValue));
        }
    }

    private IEnumerator FadeObj(SpriteRenderer _spriteTrans, float fadeTime, float startValue, float targetValue)
    {
        float timeElapsed = 0;

        while (timeElapsed < fadeTime)
        {
            timeElapsed += Time.deltaTime;
            float _newAlpha = Mathf.Lerp(startValue, targetValue, timeElapsed / fadeTime);
            _spriteTrans.color = new Color(_spriteTrans.color.r, _spriteTrans.color.g, _spriteTrans.color.b, _newAlpha);
        }
        yield return null;
    }
}
