using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusicBg : MonoBehaviour
{
    public static Action<SFX> newMusicActionSFX;
    [SerializeField] protected SFX newMusicSFX;

    private bool alreadyChanged = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !alreadyChanged)
        {
            alreadyChanged = true;
            newMusicActionSFX?.Invoke(newMusicSFX);
        }
    }
}
