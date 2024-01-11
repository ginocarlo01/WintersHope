using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint 
{
    bool occupied;

    public bool GetState()
    {
        return occupied;
    }

    public void SetState(bool state)
    {
        occupied = state;
    }
}
