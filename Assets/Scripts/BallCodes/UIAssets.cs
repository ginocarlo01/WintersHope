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

    [SerializeField]
    public GameObject healthObj;

    public static UIAssets instance;

    private Material healthMat;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //the only way to change is by doing this:

        healthObj.GetComponent<SpriteRenderer>().material = Instantiate<Material>(healthObj.GetComponent<SpriteRenderer>().material);
        healthMat = healthObj.GetComponent<SpriteRenderer>().material;
        //healthObj.GetComponent<SpriteRenderer>().material.SetFloat("_Arc1", 90f);
        //ChangeHealthArc(180);
    }

    public void ChangeHealthArc(float angle)
    {
        healthMat.SetFloat("_Arc1", angle);
    }
}
