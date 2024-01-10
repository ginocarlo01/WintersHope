using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBorder : MonoBehaviour
{
    [SerializeField]
    private string enemyTag = "Enemy";

    [SerializeField]
    public int life;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(enemyTag))
        {
            
            Destroy(this.gameObject);
        }
    }
}
