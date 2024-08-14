using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy3StateBase
{
    public virtual void OnEnter(Enemy3 owner, Enemy3StateBase prevState) { }
    public virtual void OnUpdate(Enemy3 owner) { }
    public virtual void OnFixedUpdate(Enemy3 owner) { }
    public virtual void OnExit(Enemy3 owner, Enemy3StateBase nextState) { }

}
