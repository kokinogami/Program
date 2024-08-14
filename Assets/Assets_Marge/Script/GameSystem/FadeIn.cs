using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeIn : MonoBehaviour
{
    private Image image;
    private float R, G, B;
    [System.NonSerialized] public float alfa;
    [SerializeField] private float fadingSpeed;

    bool fadeIn = true;
    bool fadeOut = false;

    public string selectedScene;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out image);
        R = image.color.r;
        G = image.color.g;
        B = image.color.b;
        alfa = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
        {
            alfa -= fadingSpeed * Time.deltaTime;
            if (alfa > 1)
            {
                alfa = 0;
                SceneManager.LoadScene(selectedScene);
            }
        }
        if (fadeOut)
        {
            alfa += fadingSpeed * Time.deltaTime;
            if (alfa > 1)
            {
                alfa = 0;
                SceneManager.LoadScene(selectedScene);
            }

        }
        image.color = new Color(R, G, B, alfa);
    }
}
