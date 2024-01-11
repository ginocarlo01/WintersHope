using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseState : IProjectileState
{
    Vector3 movementDirection;

    ProjectileController controller;

    public ReleaseState(ProjectileController controller)
    {
        this.controller = controller;
    }

    public IProjectileState ChangeState()
    {
        throw new System.NotImplementedException();
    }

    public void OnBeginState()
    {
        movementDirection = new Vector3(-1, 0, 0);
    }

    public void OnUpdate()
    {
        controller.transform.Translate(controller.currentSpeed * movementDirection * Time.deltaTime);
    }
}
