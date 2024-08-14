using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearActiveObj : MonoBehaviour
{
    [SerializeField] Stage ClearStage;
    [SerializeField] GameObject activeObj;
    // Start is called before the first frame update
    void Start()
    {
        switch (ClearStage)
        {
            case (Stage.Stage1):
                if (GameManager.stage1Clear == true)
                {
                    activeObj.SetActive(true);
                }
                else
                {
                    activeObj.SetActive(false);
                }
                break;

            case (Stage.Stage2):
                if (GameManager.stage1Clear == true)
                {
                    activeObj.SetActive(true);
                }
                else
                {
                    activeObj.SetActive(false);
                }
                break;

            case (Stage.Stage3):
                if (GameManager.stage1Clear == true)
                {
                    activeObj.SetActive(true);
                }
                else
                {
                    activeObj.SetActive(false);
                }
                break;

            case (Stage.tutorial1):
                if (GameManager.stage1Clear == true)
                {
                    activeObj.SetActive(true);
                }
                else
                {
                    activeObj.SetActive(false);
                }
                break;

            case (Stage.tutorial2):
                if (GameManager.stage1Clear == true)
                {
                    activeObj.SetActive(true);
                }
                else
                {
                    activeObj.SetActive(false);
                }
                break;

            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
