using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTutorial : MonoBehaviour
{
    [SerializeField] GameObject textTutorial;
    [System.NonSerialized]bool OpenWind = true;
    float triggerCount;
    // Start is called before the first frame update
    void Start()
    {
        textTutorial.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerCount <= 0)
        {
            textTutorial.SetActive(false);
        }
        else
        {
            triggerCount -= Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            triggerCount = 0.1f;
            if (OpenWind == false) return;
            OpenWind = false;
            textTutorial.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            textTutorial.SetActive(false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            triggerCount = 0.1f;
        }
    }
}
