using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectileState
{
    public IProjectileState ChangeState();
    public void OnBeginState();
    public void OnUpdate();
}
