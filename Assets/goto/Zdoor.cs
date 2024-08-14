using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zdoor : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {
        if (GameManager.retryBoss)
        {
            Zdoor zdoor;
            TryGetComponent(out zdoor);
            zdoor.enabled = false;
        }
    }
    void Start()
    {
        if(GameManager.stage1Clear == true && GameManager.stage2Clear == true && GameManager.stage3Clear == true)
        {
            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        Zdoor zdoor;
        TryGetComponent(out zdoor);
        zdoor.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
