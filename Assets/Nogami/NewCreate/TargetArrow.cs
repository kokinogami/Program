using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArrow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Arrow;
    [SerializeField] GameObject Thisobj;
    [SerializeField] List<ChargebreakOffscreen> ArrowObj;
    void Start()
    {
        for (int i = 0; i < GameManager.AllEnemy.Count; i++)
        {
            //ArrowObj[i].target = GameManager.AllEnemy[i].gameObject.transform;
            GameObject instance = Instantiate(Arrow);
            instance.transform.parent = this.gameObject.transform;
            ArrowObj.Add(instance.GetComponent<ChargebreakOffscreen>());
            ArrowObj[i].target = GameManager.AllEnemy[i].gameObject.transform;
        }
        Thisobj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
