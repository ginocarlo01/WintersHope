using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIAssets : MonoBehaviour
{
    [SerializeField]
    public Image manaBar;

    [SerializeField]
    public Image healthBar;

    public static UIAssets instance;

    private void Awake()
    {
        instance = this;
    }

}
