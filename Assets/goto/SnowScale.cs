using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnowScale : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;

    void Start()
    {
        slider = GameObject.Find("SnowScaleBar").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScaleAdjust()
    {
        GameObject obj = GameObject.Find("SnowBall");
        float scale = slider.value;
        obj.transform.localScale = new Vector3(scale, scale, scale);
    }
}
