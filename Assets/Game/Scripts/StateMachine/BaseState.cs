using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState 
{
    public abstract void OnStart(Character character);
    public abstract void OnUpdate(Character character);
    public abstract void OnExit(Character character);
}
