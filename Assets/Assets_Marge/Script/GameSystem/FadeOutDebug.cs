using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeOutDebug : MonoBehaviour
{
    private float R, G, B;
    private float alfa;
    public string selectedScene;
    [SerializeField] private float fadingSpeed;

    // Start is called before the first frame update
    void Start()
    {
        R = GetComponent<Image>().color.r;
        G = GetComponent<Image>().color.g;
        B = GetComponent<Image>().color.b;
        alfa = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedScene != "")
        {
            GetComponent<Image>().color = new Color(R, G, B, alfa);
            alfa += fadingSpeed * Time.deltaTime;

            if (alfa > 1)
            {
                alfa = 0;
                SceneManager.LoadScene(selectedScene);
            }
        }
    }
}
