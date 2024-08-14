using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy1StateBase 
{
    public virtual void OnEnter(Enemy1 owner, Enemy1StateBase prevState) { }
    public virtual void OnUpdate(Enemy1 owner) { }
    public virtual void OnFixedUpdate(Enemy1 owner) { }
    public virtual void OnExit(Enemy1 owner, Enemy1StateBase nextState) { }

}
