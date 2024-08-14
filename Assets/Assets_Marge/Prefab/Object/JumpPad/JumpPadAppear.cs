using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpPadAppear : MonoBehaviour
{
    [System.NonSerialized] public float appearSpeed = 1.0f;
    private Vector3 appearVector = new Vector3(0.0f, 20.0f, -40.0f);
    private float moveTime = 0.09f;
    private float remainTime = 10.0f;
    private Vector3 startPos;
    [SerializeField] private GameObject connecter;
    [SerializeField] private GameObject effect;
    private bool effecter = false;
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
        if (moveTime > 0)
        {
            transform.Translate(appearSpeed * Time.deltaTime * appearVector);
        }

        else
        {
            child.SetActive(true);
            box.enabled = true;

            if (effecter == false)
            {
                Instantiate(effect, this.transform.position, this.transform.rotation, this.transform);
                effecter = true;
            }

            //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            if (Physics.Raycast(this.transform.position, Vector3.down, 1.5f) == false)
            {
                Instantiate(connecter, this.transform.position + new Vector3(0.0f, -1.3f, 0.0f), Quaternion.identity);
            }
        }

        remainTime -= Time.deltaTime;
        if (remainTime < 0)
        {
            Destroy(this.gameObject);
        }

        moveTime -= Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}