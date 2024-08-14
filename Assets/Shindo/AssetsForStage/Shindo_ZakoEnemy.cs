using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shindo_ZakoEnemy : MonoBehaviour
{
    int HP = 3;
    int DetectDist = 20;
    public float searchAngle = 45f;
    public bool InArea;
    public Transform target;
    public GameObject Sphere;
    public float SphereSpeed = 5f;
    private float count;

    private GameObject Yukino;
    Shindo_Yukino script;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Yukino = GameObject.FindWithTag("Player");
        script = Yukino.GetComponent<Shindo_Yukino>();
        rb = Yukino.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InArea == true)
        {
            //���L�m�̕��֏�Ɍ�������
            transform.LookAt(target);
            count += Time.deltaTime;
            if (count >= 0.5f)//�R�O�t���[�������Ƃ�10�_���[�W�U������
            {
                SphereShot();
                count = 0;
            }
        }
        //�G���ƃ��L�m�Ԃ̋������v�Z
        Vector3 Apos = this.transform.position;
        Vector3 Bpos = target.transform.position;
        float distance = Vector3.Distance(Apos, Bpos);
        Vector3 Direction = target.position - Apos;
        float targetAngle = Vector3.Angle(transform.forward, Direction);
        if (distance > DetectDist & searchAngle > targetAngle)
        {
            InArea = false;
        }
        if (distance < DetectDist & searchAngle < targetAngle)
        {
            InArea = true;
        }

    }
    public void SphereShot()
    {
        var Shot = Instantiate(Sphere, transform.position, Quaternion.identity);
        Shot.GetComponent<Rigidbody>().velocity = transform.forward.normalized * SphereSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (script.IceBall == true)
            {
                HP -= 1;
                if (HP <= 0)
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    rb.velocity = Vector3.zero;
                    rb.AddForce(this.gameObject.transform.forward.normalized * 5, ForceMode.Impulse);
                }
            }
            else
            {
                rb.AddForce(this.gameObject.transform.forward.normalized * 5, ForceMode.Impulse);
                script.HP -= 10;
                script.TimeCount = 3.0f;
            }
        }
    }
}