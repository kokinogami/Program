using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMarkCreate : MonoBehaviour
{
    int allEnemyCount;
    [SerializeField] GameObject marke;
    [SerializeField] GameObject ChargebrakeCanvas;
    public List<Marking> markedObj;
    public RectTransform _parentUI;
    // Start is called before the first frame update
    void Start()
    {
        allEnemyCount= GameManager.EnemyCount;
        for (int i = 0; i < GameManager.AllEnemy.Count; i++)
        {
            GameObject instance = Instantiate(marke);
            instance.transform.parent = this.gameObject.transform;
            markedObj.Add(instance.GetComponent<Marking>());
            markedObj[i].target = GameManager.AllEnemy[i].gameObject.transform;
            markedObj[i]._parentUI = _parentUI;
        }
    }

    // Update is called once per frame
    void Update()
    {
        int Aa=0;
        OnDebug();
        if (GameManager.EnemyCount == 0) return;
        int activeNum = 0;
        float distanse = 0;
        for (int i = 0; i < allEnemyCount; i++)
        {
            Aa++;
            if (markedObj[i].none == false)
            {
                if (ChargebrakeCanvas.activeSelf == true)
                {
                    if (markedObj[i].gameObject.activeSelf == false)
                    {
                        markedObj[i].gameObject.SetActive(true);
                    }
                }
                else
                {
                    //markedObj[i].gameObject.SetActive(false);
                    var A = (GameManager.Main.transform.position - markedObj[i].target.transform.position).sqrMagnitude;
                    if (distanse == 0)
                    {
                        distanse = A;
                        activeNum = i;
                    }
                    else if (A < distanse)
                    {
                        distanse = A;
                        activeNum = i;
                    }
                }
            }

        }
        if (ChargebrakeCanvas.activeSelf == true) return;
        for (int i = 0; i < allEnemyCount; i++)
        {
            if (markedObj[i].none == false)
            {
                if (i != activeNum)
                {
                    markedObj[i].gameObject.SetActive(false);
                }
                else if (markedObj[i].gameObject.activeSelf == false)
                {
                    markedObj[i].gameObject.SetActive(true);
                }
            }
        }
    }
    void OnDebug()
    {
        //var A = (GameManager.Main.transform.position - GameManager.AllEnemy[0].transform.position).sqrMagnitude;
        //Debug.Log(A);
    }
}
