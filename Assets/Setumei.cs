using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setumei : MonoBehaviour
{
    public GameObject setu;

    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setumei()
    {
        GameManager.gameState = GameState.Nomal;
        Time.timeScale = 1f;
        setu.SetActive(false);
    }
}
