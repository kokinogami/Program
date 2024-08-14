using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;

    void Start()
    {
        slider.value = 5;
    }

    void Update()
    {
        float dx = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f;
        float dz = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
        //���E�ƑO��ړ��̂�
        transform.position = new Vector3(
            transform.position.x + dx, 0.5f, transform.position.z + dz
        );
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Enemy")
        {
            if (slider.value > 0)
            {
               slider.value --;
                if (slider.value == 0)
                {
                    Destroy(gameObject);//�܂�A�G��5�񓖂�������v���C���[�͏��ł���
                }
            }
        }
    }
}
