using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    //�G�v���n�u
    public GameObject enemy1;
    public GameObject DestroyEffect;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject);
        //enemy���C���X�^���X������(��������)
        GameObject enemy = Instantiate(enemy1);
        GameObject effect = Instantiate(DestroyEffect);
        //���������G�̍��W�����肷��(����X=0,Y=10,Z=20�̈ʒu�ɏo��)
        enemy.transform.position = this.transform.position;
        effect.transform.position = this.transform.position;
        effect.transform.localScale = new Vector3(3, 3, 3);
    }
}
