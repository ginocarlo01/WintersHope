using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallWhenDead : MonoBehaviour, IHit
{
    public event Action OnHit;

    private void OnDisable()
    {
        OnHit?.Invoke();
    }
}
