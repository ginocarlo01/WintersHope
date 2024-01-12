using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldState : IProjectileState
{
    float timer = 0;

    float speedTimer = 0;

    float newSpeed = 0;

    float intervalToUpSpeed = 1f;

    ProjectileController controller;

    public HoldState(ProjectileController controller)
    {
        this.controller = controller;
    }

    public IProjectileState ChangeState()
    {
        UpdateCurrentSpeed();
        FullManaBar();
        return controller.releaseState;
    }

    public void OnBeginState()
    {
        timer = controller.holdProjectileTime;
        newSpeed = controller.baseSpeed;
    }

    public void OnUpdate()
    {
        timer -= Time.deltaTime;
        speedTimer += Time.deltaTime;

        if (speedTimer >= intervalToUpSpeed)
        {
            speedTimer = 0;
            newSpeed += controller.upSpeedPerSecond;
        }

        if(timer <= 0)
        {
            controller.ChangeState();
        }

        UpdateManaBar();
    }

    public void UpdateCurrentSpeed()
    {
        controller.currentSpeed = newSpeed;
    }

    public void UpdateManaBar()
    {
        UIAssets.instance.manaBar.fillAmount = timer / controller.holdProjectileTime;
    }

    public void FullManaBar()
    {
        UIAssets.instance.manaBar.fillAmount = 1; //full
    }
}