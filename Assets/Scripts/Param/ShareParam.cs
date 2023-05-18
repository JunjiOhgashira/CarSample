using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu]
public class ShareParam : ScriptableObject
{
    [Label("特性インピーダンス[km/h]")]
    public double CI;

    [Label("スケーリング係数")]
    public double SF;

    [Label("特性インピーダンス（波積分）")]
    public double CII;

    [Label("スケーリング係数（波積分）")]
    public double SFI;
}