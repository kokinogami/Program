using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakableobject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform brokenPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Transform brokenTransform = Instantiate(brokenPrefab, transform.position, transform.rotation);
            brokenTransform.localScale = transform.localScale;

            foreach(Rigidbody rigidbody in brokenTransform.GetComponentsInChildren<Rigidbody>())
            {
                rigidbody.AddExplosionForce(150f, transform.position + Vector3.up * 0.5f, 5f);
            }

            Destroy(gameObject);
        }
    }
}
