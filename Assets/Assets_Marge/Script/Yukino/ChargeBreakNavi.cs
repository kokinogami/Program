using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBreakNavi : MonoBehaviour
{
    public CinemaScean cinema;
    void Update()
    {
        float Value = cinema.chargeBreakCamera_POV.m_HorizontalAxis.Value;
        this.transform.eulerAngles = new Vector3(0.0f, Value, 0.0f);
    }
}
