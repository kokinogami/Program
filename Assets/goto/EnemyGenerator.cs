using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    //敵プレハブ
    public GameObject enemy1;
    public GameObject DestroyEffect;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject);
        //enemyをインスタンス化する(生成する)
        GameObject enemy = Instantiate(enemy1);
        GameObject effect = Instantiate(DestroyEffect);
        //生成した敵の座標を決定する(現状X=0,Y=10,Z=20の位置に出力)
        enemy.transform.position = this.transform.position;
        effect.transform.position = this.transform.position;
        effect.transform.localScale = new Vector3(3, 3, 3);
    }
}
