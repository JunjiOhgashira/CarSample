using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu]
public class ModeChange : ScriptableObject
{
    // communication type
    [Label("�x������"), OnValueChanged("OnValueChanged")]
    public bool DelayVehicle;

    [Label("�g�ϐ��ϊ�"), EnableIf("DelayVehicle"), OnValueChanged("OnValueChanged1")]
    public bool WaveVariableTransformation;

    [Label("�g�ϕ�"), EnableIf("WaveVariableTransformation"), OnValueChanged("OnValueChanged1")]
    public bool WaveIntegral;

    [Label("�g����"), EnableIf("WaveVariableTransformation"), OnValueChanged("OnValueChanged1")]
    public bool WaveAdjust;

    // camera type
    [Label("�J�������_�i�ォ��j")]
    public bool LookDown;

    //velocity type
    [Label("��葬�x"), DisableIf("Stop")]
    public bool ConstantVelocity;

    [Label("��~"), DisableIf("ConstantVelocity")]
    public bool Stop;

    // input type
    [Label("�����f�[�^"), DisableIf("GamepadInput")]
    public bool ExperimentData;

    [Label("���́iON:�Q�[���p�b�h, Off:�n���R���j"), DisableIf("ExperimentData")]
    public bool GamepadInput;

    private void OnValueChanged()
    {
        if (DelayVehicle == false)
        {
            WaveVariableTransformation = false;
            WaveIntegral = false;
        }
    }

    private void OnValueChanged1()
    {
        if (WaveIntegral == true)
        {
            WaveVariableTransformation = true;
        }
    }

    private void OnValueChanged2()
    {
        if (GamepadInput == true)
        {
            ExperimentData = false;
        }
        if (ExperimentData == true)
        {
            GamepadInput = false;
        }
    }
}
