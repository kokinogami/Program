using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleSystem : MonoBehaviour
{
	public static int a = 100;
	//�@�X�^�[�g�{�^��������������s����
	public void StartGame()
	{
		SceneManager.LoadScene("HarutoNakayama_Marge");
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
}