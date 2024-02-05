using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IProjectileState
{
    ProjectileController controller;


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
        
    }

    public void OnUpdate()
    {
        controller.transform.Translate(controller.baseSpeed  * controller.currentDirection * Time.deltaTime);
    }
}
