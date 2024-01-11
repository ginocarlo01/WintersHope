using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDisappearState : IEnemyState
{
    EnemyController controller;

    float disappearTimer = 0;

    public EnemyDisappearState(EnemyController controller)
    {
        this.controller = controller;
    }

    public IEnemyState ChangeState()
    {
        throw new System.NotImplementedException();
    }

    public void OnBeginState()
    {
        throw new System.NotImplementedException();
    }

    public void OnUpdate()
    {
        disappearTimer += Time.deltaTime;

        if(disappearTimer > controller.disappearTime)
        {
            disappearTimer = 0;
        }
    }

    public void ChangeDisappearArc(float angle)
    {
        controller.materialDisappearArc.SetFloat("_Arc1", angle);
    }

    public void FindNewPosition()
    {

    }


}
