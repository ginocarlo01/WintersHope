using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToProjectileQty : MonoBehaviour, IPooledObject
{
    [SerializeField]
    TypeUtility.Type type;

    [SerializeField]
    int qty = 1;

    [SerializeField]
    SpriteRenderer colorSpriteRender;

    [SerializeField]
    Sprite[] colorTypes;

    [SerializeField]
    string playerTag = "Player";

    public static Action<TypeUtility.Type, int> collectedAction;

    private void Start()
    {
        colorSpriteRender.sprite = colorTypes[((int)type)];
    }

    public void OnObjectDisabled()
    {
        this.gameObject.SetActive(false);
    }

    public void OnObjectSpawn()
    {
        colorSpriteRender.sprite = colorTypes[((int)type)];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == playerTag)
        {
            collectedAction?.Invoke(type, qty);
            OnObjectDisabled();
        }
    }
}
