using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room : MonoBehaviour
{
    private List<EnemyController> enemies = new List<EnemyController>();

    private List<ActivateByDeath> activateByDeaths = new List<ActivateByDeath>();

    [SerializeField]
    bool startInactive = false;

    public GameObject virtualCamera;

    private void Start()
    {
        enemies = GetComponentsInChildren<EnemyController>().Cast<EnemyController>().ToList();

        //activateByDeaths = GetComponentsInChildren<ActivateByDeath>().Cast<ActivateByDeath>().ToList();

        if (startInactive)
        {
            foreach (EnemyController enemy in enemies)
                ChangeActivation(enemy, false);

            //foreach (ActivateByDeath activateByDeath in activateByDeaths)
            //    ChangeActivation(activateByDeath, false);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            foreach (EnemyController enemy in enemies)
                ChangeActivation(enemy, true);

            //foreach (ActivateByDeath activateByDeath in activateByDeaths)
            //    ChangeActivation(activateByDeath, true);

            virtualCamera.SetActive(true);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            foreach (EnemyController enemy in enemies)
                ChangeActivation(enemy, false);

           // foreach (ActivateByDeath activateByDeath in activateByDeaths)
            //    ChangeActivation(activateByDeath, false);

            virtualCamera.SetActive(false);
        }
    }

    private void ChangeActivation(Component component, bool activate)
    {
        component.gameObject.SetActive(activate);
    }
}
