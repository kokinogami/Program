using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int EnemyHP = 5;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyHP == 0)
        {
            Destroy(gameObject);//つまり、敵に5回当たったらプレイヤーは消滅する
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            int ID = collision.gameObject.GetComponent<YukinoController>().index;
            if (ID == 1)
            {
                if (EnemyHP > 0)
                {
                    EnemyHP -= 5;
                }
            }
        }
    }
}
