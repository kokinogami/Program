using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    public static float TimeCount;
    public Text TimeText;
    public static float minutes;
    private bool Result;
    [SerializeField] private RectTransform zone;
    [SerializeField] private GameObject resultObj;
    [SerializeField] private GameObject fadeInScreen;
    [SerializeField] private GameObject startPlatform;
    private GameObject gameSystem;
    public EnemyCounter EnemyCount;
    Vector3 A;

    FadeIn fading;//フェードイン判定
    StartPlatform start;
    // Start is called before the first frame update
    void Start()
    {
        TimeCount = 0.0f;
        minutes = 0;
        if (TimeCount > 60.0f)
        {
            minutes = TimeCount / 60.0f;
            minutes = Mathf.Floor(minutes);
            float lostcount = minutes * 60;
            TimeCount += lostcount;
        }

        gameSystem = GameObject.Find("GameSystem");
        EnemyCount = gameSystem.GetComponent<EnemyCounter>();
        fading = fadeInScreen.GetComponent<FadeIn>();
        start = startPlatform.GetComponent<StartPlatform>();
    }

    // Update is called once per frame
    void Update()
    {
        A = new Vector3(Screen.width / 2 + 320, Screen.height / 2 + 120, 0.0f);
        //Debug.Log(A);
        if (TimeCount > 60.0f)
        {
            minutes += 1;
            TimeCount -= 60.0f;
        }
        TimeText.text = string.Format("{0:00.00}", TimeCount);
        GetComponent<Text>().text = minutes + ":" + string.Format("{0:00.00}", TimeCount);

        if (fading.alfa <= 0 && start.count <= 0)
        {
            if (EnemyCount.enemyNum != 0)
                TimeCount += Time.deltaTime;

            else if (Result == false)
            {
                resultObj.SetActive(true);
                zone.position = A;//new Vector3(960, 450, 0);
                Result = true;
            }
        }
    }
}