using UnityEngine;

public partial class YukinoMain
{
	/// <summary>
	/// ���剻���
	/// </summary>
	public class StateGigantic : PlayerStateBase
	{
		//YukinoMain Main;
		public override void OnUpdate(YukinoMain owner)
		{
			if (Input.GetKey(KeyCode.DownArrow))
			{
				
			}
			else if (Input.GetKeyDown(KeyCode.Space))
			{
				owner.ChangeState(stateJumping);
			}
		}
	}
}