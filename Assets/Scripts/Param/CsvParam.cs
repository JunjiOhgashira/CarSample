using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu]
public class CsvParam : ScriptableObject
{
    [Label("実車データ")]
    public string quote;
}