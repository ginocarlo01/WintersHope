using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IProjectileState
{
    ProjectileController controller;


    Vector3 lookAtPos = Vector3.zero;

    public AttackState(ProjectileController controller)
    {
        this.controller = controller;
    }

    public IProjectileState ChangeState()
    {
        return controller.holdState;
    }

    public void OnBeginState()
    {
        controller.currentDirection = new Vector3(1, 0, 0);

        Vector3 lookDir = lookAtPos - controller.transform.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        controller.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void OnUpdate()
    {
        controller.transform.Translate(controller.baseSpeed  * controller.currentDirection * Time.deltaTime);
    }
}
