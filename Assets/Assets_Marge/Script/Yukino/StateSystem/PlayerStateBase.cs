/// <summary>
/// State�̒��ۃN���X
/// </summary>
public abstract class PlayerStateBase
{
	/// <summary>
	/// �X�e�[�g���J�n�������ɌĂ΂��
	/// </summary>
	public virtual void OnEnter(YukinoMain owner, PlayerStateBase prevState) { }
	/// <summary>
	/// ���t���[���Ă΂��
	/// </summary>
	public virtual void OnUpdate(YukinoMain owner) { }
	/// <summary>
	/// �X�e�[�g���I���������ɌĂ΂��
	/// </summary>
	public virtual void OnExit(YukinoMain owner, PlayerStateBase nextState) { }

	public virtual void OnLateUpdate(YukinoMain owner){}
	public virtual void OnFixedUpdate(YukinoMain owner) { }
}
