using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu]
public class Parameter : ScriptableObject
{
    [Label("片道遅延時間[ms]")]
    public int delay;

    [Label("質量[kg]")]
    public double M;

    [Label("慣性モーメント[kg・m2]")]
    public double I;

    [Label("速度[km/h]")]
    public double V;

    [Label("ホイルベース[m]")]
    public double l;

    [Label("前輪と重心の距離[m]")]
    public double lf;

    [Label("後輪と重心の距離[m]")]
    public double lr;

    [Label("前輪のコーナリングスティフネス[N/rad]")]
    public double Kf;

    [Label("後輪のコーナリングスティフネス[N/rad]")]
    public double Kr;

    [Label("車体の長さ[m]")]
    public double length;

    [Label("車体の幅[m]")]
    public double width;

    [Label("車体の高さ[m]")]
    public double height;

    [Label("初速度[km/h]")]
    public double V0;

    [Label("ステアリングギア比")]
    public double ratio;

    [Label("アクセルペダル最大値")]
    public double accelMax;

    [Label("ブレーキペダル最大値")]
    public double brakeMax;

    [Label("速度下限値[km/h]")]
    public double LowerLimitVelocity;
}