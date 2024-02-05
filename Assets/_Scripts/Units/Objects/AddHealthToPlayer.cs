using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealthToPlayer : MonoBehaviour, IPooledObject
{
    [SerializeField]
    private string playerTag = "Player";

    [SerializeField]
    private int upHealth = 1;

    public void OnObjectDisabled()
    {
        this.gameObject.SetActive(false);
    }

    public void OnObjectSpawn()
    {
        return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == playerTag)
        {
            collision.GetComponent<LifeBorder>().AlterLife(-upHealth);
            OnObjectDisabled();
        }
    }
}
