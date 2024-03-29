using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursorAttackState : IProjectileState
{
    ProjectileController controller;

    public FollowCursorAttackState(ProjectileController controller)
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
        LookToCursor();
    }

    public void OnUpdate()
    {
        controller.transform.Translate(controller.baseSpeed * controller.currentDirection * Time.deltaTime);
    }

    private void LookToCursor()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector3 lookDir = mousePos - controller.transform.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        controller.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
