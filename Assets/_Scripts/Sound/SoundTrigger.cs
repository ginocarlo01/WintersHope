using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public static Action<SFX> exampleActionSFX;
    [SerializeField] protected SFX exampleSFX;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            exampleActionSFX?.Invoke(exampleSFX);
        }
    }
}
