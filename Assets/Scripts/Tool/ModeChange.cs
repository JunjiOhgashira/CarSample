using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu]
public class ModeChange : ScriptableObject
{
    [Label("遅延あり"), OnValueChanged("OnValueChanged")]
    public bool DelayVehicle;

    [Label("提案手法あり"), EnableIf("DelayVehicle"), OnValueChanged("OnValueChanged1")]
    public bool ProposedMethod;

    [Label("一定特性インピーダンス"), EnableIf(EConditionOperator.And, "DelayVehicle", "ProposedMethod")]
    public bool ConstantB;

    [Label("カメラ視点（上から）")]
    public bool Mounted;

    [Label("速度0")]
    public bool ZeroVelocity;

    [Label("一定速度"), DisableIf("ZeroVelocity")]
    public bool ConstantVelocity;

    [Label("実験データ")]
    public bool ExperimentData;

    [Label("入力（ON:ゲームパッド, Off:ハンコン）")]
    public bool GamepadInput;

    [Label("車体を表示（Car）"), DisableIf("DelayVehicle")]
    public bool CarOutline;

    [Label("車体を表示（Master）"), EnableIf("DelayVehicle")]
    public bool MasterOutline;

    [Label("車体を表示（Slave）"), EnableIf("DelayVehicle")]
    public bool SlaveOutline;

    private void OnValueChanged()
    {
        if (DelayVehicle == false)
        {
            ProposedMethod = false;
            ConstantB = false;
        }
    }

    private void OnValueChanged1()
    {
        if (ProposedMethod == false)
        {
            ConstantB = false;
        }
    }
}
