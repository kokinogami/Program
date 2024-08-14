using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    private float remainTime = 10.0f;
    private Vector3 startPos;
    [SerializeField] private GameObject connecter;
    BoxCollider box;
    GameObject child;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        box = GetComponent<BoxCollider>();
        child = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < startPos.y + 1.44f)
        {
            transform.Translate(0.0f, 20.0f * Time.deltaTime, -40.0f * Time.deltaTime);
        }

        else
        {
            child.SetActive(true);
            box.enabled = true;
            //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            if (Physics.Raycast(this.transform.position, Vector3.down, 1.0f) == false)
            {
                Instantiate(connecter, this.transform.position + new Vector3(0.0f, -5.5f, 0.0f), Quaternion.identity);
            }
        }

        remainTime -= Time.deltaTime;
        if (remainTime < 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}