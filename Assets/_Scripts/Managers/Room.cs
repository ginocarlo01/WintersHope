using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Room : MonoBehaviour
{
    private List<EnemyController> enemies = new List<EnemyController>();

    [SerializeField]
    bool startInactive = false;

    private void Start()
    {
        enemies = GetComponentsInChildren<EnemyController>().Cast<EnemyController>().ToList();
        if(startInactive)
        {
            foreach (EnemyController enemy in enemies)
                ChangeActivation(enemy, false);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            foreach (EnemyController enemy in enemies)
                ChangeActivation(enemy, true);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            foreach (EnemyController enemy in enemies)
                ChangeActivation(enemy, false);
        }
    }

    private void ChangeActivation(Component component, bool activate)
    {
        component.gameObject.SetActive(activate);
    }
}
