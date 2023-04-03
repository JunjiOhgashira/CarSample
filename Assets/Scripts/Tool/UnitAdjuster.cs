using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAdjuster : MonoBehaviour
{
    public Parameter parameter;
    public ShareParam shareParam;

    [HideInInspector]
    public double V_km_h;
    [HideInInspector]
    public double V_m_s;
    [HideInInspector]
    public double V0_km_h;
    [HideInInspector]
    public double V0_m_s;
    [HideInInspector]
    public double Vb_km_h;
    [HideInInspector]
    public double Vb_m_s;

    void Start()
    {
        V_m_s   = parameter.V * 1000 / 3600;
        V_km_h  = parameter.V;
        V0_m_s  = parameter.V0 * 1000 / 3600;
        V0_km_h = parameter.V0;
        Vb_m_s  = shareParam.b * 1000 / 3600;
        Vb_km_h = shareParam.b;
    }
}