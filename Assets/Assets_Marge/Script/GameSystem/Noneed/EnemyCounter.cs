using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    [System.NonSerialized] public GameObject[] enemies;//�G�J�E���g�p�z��
    [System.NonSerialized] public int enemyNum;//�G�̐�
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
