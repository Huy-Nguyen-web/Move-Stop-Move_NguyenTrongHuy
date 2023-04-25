using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState 
{
    public abstract void OnStart(Player player);
    public abstract void OnUpdate(Player player);
    public abstract void OnExit(Player player);

}
