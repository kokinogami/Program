using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeColourChange : MonoBehaviour
{
    // Start is called before the first frame update
    Image image;
    Color defaultcolor;
    Color cooltimecolor = new Color(0.7f, 0.8f, 1.0f, 1.0f);
    Color atackedCooltimecolor = new Color(1.0f, 0.0f, 0.0f, 1.0f);
    void Start()
    {
        // Textコンポーネントを取得
        image = GetComponent<Image>();
        defaultcolor = image.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Main.attackedAuteHealCount > 0 || GameManager.Main.auteHealCount > 0)
        {
            if(GameManager.Main.attackedAuteHealCount> GameManager.Main.auteHealCount)image.color = atackedCooltimecolor;
            else image.color = cooltimecolor;
        }
        else
        {
            image.color = defaultcolor;
        }
    }
}
