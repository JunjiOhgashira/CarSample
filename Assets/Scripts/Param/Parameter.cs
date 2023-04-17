using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu]
public class Parameter : ScriptableObject
{
    [Label("�Г��x������[ms]")]
    public int delay;

    [Label("����[kg]")]
    public double M;

    [Label("�������[�����g[kg�Em2]")]
    public double I;

    [Label("���x[km/h]")]
    public double V;

    [Label("�z�C���x�[�X[m]")]
    public double l;

    [Label("�O�ւƏd�S�̋���[m]")]
    public double lf;

    [Label("��ւƏd�S�̋���[m]")]
    public double lr;

    [Label("�O�ւ̃R�[�i�����O�X�e�B�t�l�X[N/rad]")]
    public double Kf;

    [Label("��ւ̃R�[�i�����O�X�e�B�t�l�X[N/rad]")]
    public double Kr;

    [Label("�ԑ̂̒���[m]")]
    public double length;

    [Label("�ԑ̂̕�[m]")]
    public double width;

    [Label("�ԑ̂̍���[m]")]
    public double height;

    [Label("�����x[km/h]")]
    public double V0;

    [Label("�X�e�A�����O�M�A��")]
    public double ratio;

    [Label("�A�N�Z���y�_���ő�l")]
    public double accelMax;

    [Label("�u���[�L�y�_���ő�l")]
    public double brakeMax;

    [Label("���x�����l[km/h]")]
    public double LowerLimitVelocity;
}