using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu]
public class ModeChange : ScriptableObject
{
    [Label("�x������"), OnValueChanged("OnValueChanged")]
    public bool DelayVehicle;

    [Label("��Ď�@����"), EnableIf("DelayVehicle"), OnValueChanged("OnValueChanged1")]
    public bool ProposedMethod;

    [Label("�������C���s�[�_���X"), EnableIf(EConditionOperator.And, "DelayVehicle", "ProposedMethod")]
    public bool ConstantB;

    [Label("�J�������_�i�ォ��j")]
    public bool Mounted;

    [Label("���x0")]
    public bool ZeroVelocity;

    [Label("��葬�x"), DisableIf("ZeroVelocity")]
    public bool ConstantVelocity;

    [Label("�����f�[�^")]
    public bool ExperimentData;

    [Label("���́iON:�Q�[���p�b�h, Off:�n���R���j")]
    public bool GamepadInput;

    [Label("�ԑ̂�\���iCar�j"), DisableIf("DelayVehicle")]
    public bool CarOutline;

    [Label("�ԑ̂�\���iMaster�j"), EnableIf("DelayVehicle")]
    public bool MasterOutline;

    [Label("�ԑ̂�\���iSlave�j"), EnableIf("DelayVehicle")]
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
