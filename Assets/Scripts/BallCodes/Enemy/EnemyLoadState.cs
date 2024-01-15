using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoadState : IEnemyState
{

    EnemyController controller;

    float loadTimer = 0;

    Vector3 lookAtPos = Vector3.zero;

    public EnemyLoadState(EnemyController controller)
    {
        this.controller = controller;
    }

    public IEnemyState ChangeState()
    {
        ChangeLoadArc(0);
        return controller.disappearState;
    }

    public void OnBeginState()
    {
        UpdateRotation();
        ChangeLoadArc(0);
    }

    public void OnUpdate()
    {
        loadTimer += Time.deltaTime;
        ChangeLoadArc(loadTimer / controller.loadTime * 360);

        if(loadTimer >= controller.loadTime)
        {
            loadTimer = 0;
            controller.SpawnProjectile();
            controller.ChangeState();
        }

    }

    public void ChangeLoadArc(float angle)
    {
        controller.materialLoadArc.SetFloat("_Arc1", angle);
    }

    public void UpdateRotation()
    {
        Vector3 lookDir = lookAtPos - controller.transform.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        controller.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }


}
