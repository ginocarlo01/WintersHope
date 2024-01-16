using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class FollowAttackState : IProjectileState
{
    ProjectileController controller;

    Vector3 movementDirection;

    Vector3 lookAtPos = Vector3.zero;

    public FollowAttackState(ProjectileController controller)
    {
        this.controller = controller;
    }

    public IProjectileState ChangeState()
    {
        return controller.holdState;
    }

    public void OnBeginState()
    {
        movementDirection = new Vector3(1, 0, 0);

        LookToPlayer();
    }

    public void OnUpdate()
    {
        controller.transform.Translate(controller.baseSpeed  * movementDirection * Time.deltaTime);
    }

    public void LookToPlayer()
    {
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        playerPos.z = 0f;

        Vector3 lookDir = playerPos - controller.transform.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        controller.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
