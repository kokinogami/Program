using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Nogami_SeaneCotroller : MonoBehaviour
{

    public static int a = 100;
	//�@�X�^�[�g�{�^��������������s����
	public void StartGame()
	{
		SceneManager.LoadScene("Nogami_Debug");
	}

	//�@�Q�[���I���{�^��������������s����
	public void EndGame()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}

	
	public static int getA()
	{
		return a;
	}
	public void StratCon(InputAction.CallbackContext context)
    {
		SceneManager.LoadScene("Nogami_Debug");
	}
}