using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleSystem : MonoBehaviour
{
	public static int a = 100;
	//　スタートボタンを押したら実行する
	public void StartGame()
	{
		SceneManager.LoadScene("HarutoNakayama_Marge");
	}

	//　ゲーム終了ボタンを押したら実行する
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