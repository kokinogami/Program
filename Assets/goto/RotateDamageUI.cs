using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDamageUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //HP�o�[���J�����ɏ�Ɍ�����
        transform.rotation = Camera.main.transform.rotation;
    }
}
