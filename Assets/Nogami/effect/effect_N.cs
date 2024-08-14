using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect_N : MonoBehaviour
{
    [System.NonSerialized] public int index = 0;//�l�Ɛ�ʂ̐؂�ւ�
    [System.NonSerialized] public int o_max = 0;
    [System.NonSerialized] public GameObject[] childObject;
    public GameObject Effect;
    // Start is called before the first frame update
    void Start()
    {
        o_max = this.transform.childCount;//�q�I�u�W�F�N�g�̌��擾
        childObject = new GameObject[o_max];//�C���X�^���X�쐬

        for (int i = 0; i < o_max; i++)
        {
            childObject[i] = transform.GetChild(i).gameObject;//���ׂĂ̎q�I�u�W�F�N�g�擾
        }
        foreach (GameObject gamObj in childObject)
        {
            gamObj.SetActive(false);
        }
        childObject[index].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            childObject[index].SetActive(false);
            if (index == 0)
            {
                index = 1;
            }
            else if (index == 1)
            {
                index = 0;
                Instantiate(Effect, this.gameObject.transform.position, Quaternion.identity);
            }
            childObject[index].SetActive(true);
        }
    }
}
