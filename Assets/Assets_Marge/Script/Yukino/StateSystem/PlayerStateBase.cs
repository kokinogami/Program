/// <summary>
/// Stateの抽象クラス
/// </summary>
public abstract class PlayerStateBase
{
	/// <summary>
	/// ステートを開始した時に呼ばれる
	/// </summary>
	public virtual void OnEnter(YukinoMain owner, PlayerStateBase prevState) { }
	/// <summary>
	/// 毎フレーム呼ばれる
	/// </summary>
	public virtual void OnUpdate(YukinoMain owner) { }
	/// <summary>
	/// ステートを終了した時に呼ばれる
	/// </summary>
	public virtual void OnExit(YukinoMain owner, PlayerStateBase nextState) { }

	public virtual void OnLateUpdate(YukinoMain owner){}
	public virtual void OnFixedUpdate(YukinoMain owner) { }
}
