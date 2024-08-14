using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    [System.NonSerialized] public GameObject[] enemies;//敵カウント用配列
    [System.NonSerialized] public int enemyNum;//敵の数
    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("zako");
        enemyNum = enemies.Length;
        Debug.Log(enemyNum);
    }

    // Update is called once per frame
    void Update()
    {

    }


}
