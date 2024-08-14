/// <summary>
/// Stateの抽象クラス
/// </summary>

public abstract class Enemy2StateBase 
{
	/// <summary>
	/// ステートを開始した時に呼ばれる
	/// </summary>
	public virtual void OnEnter(Enemy2 owner, Enemy2StateBase prevState) { }
	/// <summary>
	/// 毎フレーム呼ばれる
	/// </summary>
	public virtual void OnUpdate(Enemy2 owner) { }
	public virtual void OnFixedUpdate(Enemy2 owner) { }
	/// <summary>
	/// ステートを終了した時に呼ばれる
	/// </summary>
	public virtual void OnExit(Enemy2 owner, Enemy2StateBase nextState) { }

}
