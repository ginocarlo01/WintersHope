using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Afinity Container", menuName = "ScriptableObject/AfinityContainerObject")]
public class AfinityContainer : ScriptableObject
{
    [SerializeField]
    private float afinity = 0;
    [SerializeField]
    private float maxAfinity = 5;
    [SerializeField]
    private float minAfinity = -5;
    
    private void OnValidate()
    {
        maxAfinity = 5;
        minAfinity = -5;
    }

    public float GetAfinity()
    {
        return afinity;
    }

    public void IncreaseAfinity(float value)
    {
        afinity += value;
        if(afinity>=maxAfinity)
            afinity = maxAfinity;
    }

    public void DecreaseAfinity(float value)
    {
        afinity -= value;
        if (afinity <= minAfinity)
            afinity = minAfinity;
    }
}
