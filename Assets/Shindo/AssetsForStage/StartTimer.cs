using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTimer : MonoBehaviour
{
    [SerializeField] private GameObject startPlatForm;
    StartPlatform start;

    private float time;
    public Text TimeText;

    [SerializeField] GameObject Image;
    public static bool startcount = false;
    // Start is called before the first frame update
    void Start()
    {
        start = startPlatForm.GetComponent<StartPlatform>();
        startcount = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (StartPlatform.countdown && startcount == true)
        {
            Image.SetActive(true);
        }
        if (start.count >= 3)
        { }
        else if (start.count > 0)
        {
            time = Mathf.Floor(start.count + 1f);
            GetComponent<Text>().text = time.ToString();
        }
        else if (start.count > -0.15f)
            GetComponent<Text>().text = "Start!!";
        else
        {
            Destroy(Image);
            Destroy(this.gameObject);
        }
    }

}
