using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseTrigger : MonoBehaviour
{
    [SerializeField] Signal pauseSignal;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseSignal.Raise();
        }
    }
}
