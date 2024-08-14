using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Nogami_SeaneCotroller : MonoBehaviour
{

    public static int a = 100;
	//　スタートボタンを押したら実行する
	public void StartGame()
	{
		SceneManager.LoadScene("Nogami_Debug");
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
	public void StratCon(InputAction.CallbackContext context)
    {
		SceneManager.LoadScene("Nogami_Debug");
	}
}