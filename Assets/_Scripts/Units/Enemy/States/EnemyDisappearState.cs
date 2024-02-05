using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDisappearState : IEnemyState
{
    EnemyController controller;

    float disappearTimer;

    public EnemyDisappearState(EnemyController controller)
    {
        this.controller = controller;
    }

    public IEnemyState ChangeState()
    {
        ChangeDisappearArc(0);
        controller.ChangePosition();
        return controller.loadState;
    }

    public void OnBeginState()
    {
        ChangeDisappearArc(0);
    }

    public void OnUpdate()
    {
        disappearTimer += Time.deltaTime;
        ChangeDisappearArc(disappearTimer / controller.disappearTime * 360);

        if (disappearTimer > controller.disappearTime)
        {
            disappearTimer = 0;
            controller.ChangeState();
        }
    }

    public void ChangeDisappearArc(float angle)
    {
        controller.materialDisappearArc.SetFloat("_Arc1", angle);
    }

}
