using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer[] baseSprites;

    [SerializeField]
    Sprite filledCollectible;

    void FillCollectible(int index)
    {
        baseSprites[index].sprite = filledCollectible;
    }

    private void OnEnable()
    {
        CollectableInteraction.collectibleInteractAction += FillCollectible;     
    }

    private void OnDisable()
    {
        CollectableInteraction.collectibleInteractAction -= FillCollectible;
    }

}
