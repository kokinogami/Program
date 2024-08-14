using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum phase
{
    pahse1=0,
    pahse2=1,
    pahseFinal=2
}
public enum step
{
    step1,
    step2,
    step3,
    step4
}
public partial class BossEnemy : MonoBehaviour
{
    private static readonly BossStateWait stateWait = new BossStateWait();
    private static readonly BossStateBurstShot stateBurstShot = new BossStateBurstShot();
    private static readonly BossStateRollingShot stateRollingShot = new BossStateRollingShot();
    private static readonly BossStateWingstab stateWingstab = new BossStateWingstab();
    private static readonly BossStateWingstabQuadruple stateWingQuadruple = new BossStateWingstabQuadruple();
    private static readonly BossStateBeam stateBeam = new BossStateBeam();

    private BossEnemyStateBase currentState = stateWait;
    private phase nowPhase;
    private step nowStep;

    [SerializeField]int MaxHP;
    int HP;
    [SerializeField]int offensivePower;
    int changePhaseHP;
    // Start is called before the first frame update
    void Start()
    {
        currentState.OnEnter(this, null);
        nowPhase = phase.pahse1;
        nowStep = step.step1;
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate(this);
    }
    private void LateUpdate()
    {
        currentState.OnLateUpdate(this);
    }

    private void FixedUpdate()
    {
        currentState.OnFixedUpdate(this);
    }
    private void ChangeState(BossEnemyStateBase nextState)
    {
        currentState.OnExit(this, nextState);
        nextState.OnEnter(this, currentState);
        currentState = nextState;
    }
    void ChangePhase()
    {
        nowPhase=(phase)Enum.ToObject(typeof(phase), (int)nowPhase++);
        nowStep = step.step1;
        //changePhaseHP=MaxHP
    }
    void OnDamage()
    {
        HP -= 1;
        if (changePhaseHP >= HP) ChangePhase();
    }
}
public abstract class BossEnemyStateBase
{
    /// <summary>
	/// ステートを開始した時に呼ばれる
	/// </summary>
	public virtual void OnEnter(BossEnemy owner, BossEnemyStateBase prevState) { }
    /// <summary>
    /// 毎フレーム呼ばれる
    /// </summary>
    public virtual void OnUpdate(BossEnemy owner) { }
    /// <summary>
    /// ステートを終了した時に呼ばれる
    /// </summary>
    public virtual void OnExit(BossEnemy owner, BossEnemyStateBase nextState) { }

    public virtual void OnLateUpdate(BossEnemy owner) { }
    public virtual void OnFixedUpdate(BossEnemy owner) { }
}
