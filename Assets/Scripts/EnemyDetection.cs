using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField]
    private float radius;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, Vector3.forward, radius);
    }
    /*
    private List<GameObject> GetObjectsInsidleRadius()
    {
        List<GameObject> objectsInside = new List<GameObject>();


    }*/
}
