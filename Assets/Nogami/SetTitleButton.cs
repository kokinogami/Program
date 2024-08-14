using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTitleButton : MonoBehaviour
{
    [SerializeField] TitleUIController uIController;
    public void StartChangd()
    {
        Debug.Log("startC");
        uIController.StartChangd();
    }
    public void StageChangd()
    {
        uIController.StageChangd();
    }
    public void OptionChangd()
    {
        uIController.OptionChangd();
    }
    public void StaffRollChangd()
    {
        uIController.StaffRollChangd();
    }
}
