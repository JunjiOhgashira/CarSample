using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu]
public class ShareParam : ScriptableObject
{
    [Label("�����C���s�[�_���X[km/h]")]
    public double CI;

    [Label("�X�P�[�����O�W��")]
    public double SF;

    [Label("�����C���s�[�_���X�i�g�ϕ��j")]
    public double CII;

    [Label("�X�P�[�����O�W���i�g�ϕ��j")]
    public double SFI;
}