using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDSet : MonoBehaviour
{
    [SerializeField] Toggle toggle;
    [SerializeField] GameObject MainCanvas;
    [SerializeField] GameObject SubCanvas;
    //[SerializeField] Canvas effectCanvas;
    // Start is called before the first frame update
    void Start()
    {
        if (MainCanvas != null)
        {
            MainCanvas.SetActive(toggle.isOn);
            SubCanvas.SetActive(toggle.isOn);
            //effectCanvas.enabled = toggle.isOn;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HUDSetActive()
    {
        if (MainCanvas != null)
        {
            MainCanvas.SetActive(toggle.isOn);
            SubCanvas.SetActive(toggle.isOn);
            //effectCanvas.enabled = toggle.isOn;
        }
    }
}
