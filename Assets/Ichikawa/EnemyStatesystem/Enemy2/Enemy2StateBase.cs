/// <summary>
/// State�̒��ۃN���X
/// </summary>

public abstract class Enemy2StateBase 
{
	/// <summary>
	/// �X�e�[�g���J�n�������ɌĂ΂��
	/// </summary>
	public virtual void OnEnter(Enemy2 owner, Enemy2StateBase prevState) { }
	/// <summary>
	/// ���t���[���Ă΂��
	/// </summary>
	public virtual void OnUpdate(Enemy2 owner) { }
	public virtual void OnFixedUpdate(Enemy2 owner) { }
	/// <summary>
	/// �X�e�[�g���I���������ɌĂ΂��
	/// </summary>
	public virtual void OnExit(Enemy2 owner, Enemy2StateBase nextState) { }

}
