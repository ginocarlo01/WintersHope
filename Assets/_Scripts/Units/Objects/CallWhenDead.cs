using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallWhenDead : MonoBehaviour
{
    public event Action OnDead;

    private void OnDisable()
    {
        OnDead?.Invoke();
    }
}
