using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

public class StartUIController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    //　ポーズした時に表示するUIのプレハブ
    private GameObject pauseUIPrefab;
    //　ポーズUIのインスタンス
    private GameObject pauseUIInstance;
    //
    [SerializeField]
    private GameObject PauseFirstButton;
    InputSystemUIInputModule UI;
    void Start()
    {
        UI = GetComponent<InputSystemUIInputModule>();
        UI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PauseMenu()
    {
        if (pauseUIInstance == null)
        {
            UI.enabled = true;
            pauseUIInstance = GameObject.Instantiate(pauseUIPrefab) as GameObject;
            Time.timeScale = 0f;
            PauseFirstButton = GameObject.Find("RetryButton");
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(PauseFirstButton);
        }
        else
        {
            UI.enabled = false;
            Destroy(pauseUIInstance);
            PauseFirstButton = null;
            Time.timeScale = 1f;
        }
    }
}