using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Concentration1 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private CanvasGroup a;
    [SerializeField] private GameObject concentration;
    void Start()
    {
        a.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        a.alpha += 0.5f * Time.deltaTime;
        if(GameManager.Main.index == 0)
        {
            a.alpha = 0;
        }
    }
}
