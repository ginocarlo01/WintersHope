using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentObjects : MonoBehaviour
{
    [SerializeField]
    string playerTag = "Player";

    [Range(0, 1)]
    [SerializeField]
    private float transparecencyValue = .7f;
    [SerializeField]
    private float transparecencyFadeTime = .4f;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == playerTag && !collision.isTrigger)
        {
            StartCoroutine(FadeTree(spriteRenderer, transparecencyFadeTime, spriteRenderer.color.a, transparecencyValue));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StartCoroutine(FadeTree(spriteRenderer, transparecencyFadeTime, spriteRenderer.color.a, 1));
    }

    private IEnumerator FadeTree(SpriteRenderer _spriteTrans, float fadeTime, float startValue, float targetValue)
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
