using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField]
    public string enemyTag = "Enemy";

    [SerializeField]
    public string projectileName = "NewProjectile";

    [SerializeField]
    private float baseLife = 10;

    private float currentLife;

    public static EnemyLife instance;

    [SerializeField]
    public GameObject healthObj;

    private Material healthMat;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        healthObj.GetComponent<SpriteRenderer>().material = Instantiate<Material>(healthObj.GetComponent<SpriteRenderer>().material);
        healthMat = healthObj.GetComponent<SpriteRenderer>().material;
        currentLife = baseLife;
    }
    public void TakeDamage(int damage)
    {
        currentLife -= damage;
        ChangeHealthArc((1 - currentLife / baseLife) * 360);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(enemyTag))
        {
            
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }

    public void ChangeHealthArc(float angle)
    {
        healthMat.SetFloat("_Arc1", angle);
    }
}
