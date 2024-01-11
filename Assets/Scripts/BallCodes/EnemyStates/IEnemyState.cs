using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState 
{
    public IEnemyState ChangeState();
    public void OnBeginState();
    public void OnUpdate();
}
