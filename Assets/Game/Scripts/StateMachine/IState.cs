using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState<T>
{
    void OnStart( T t );
    void OnUpdate( T t );
    void OnExit( T t );
}
