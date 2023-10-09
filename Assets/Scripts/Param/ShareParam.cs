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
}