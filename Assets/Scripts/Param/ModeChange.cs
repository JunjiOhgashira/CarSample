using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu]
public class ModeChange : ScriptableObject
{
    // communication type
    [Label("遅延あり"), OnValueChanged("OnValueChanged")]
    public bool DelayVehicle;

    [Label("波変数変換"), EnableIf("DelayVehicle"), OnValueChanged("OnValueChanged1")]
    public bool WaveVariableTransformation;

    [Label("波積分"), EnableIf("WaveVariableTransformation"), OnValueChanged("OnValueChanged1")]
    public bool WaveIntegral;

    [Label("波調整"), EnableIf("WaveVariableTransformation"), OnValueChanged("OnValueChanged1")]
    public bool WaveAdjust;

    // camera type
    [Label("カメラ視点（上から）")]
    public bool LookDown;

    //velocity type
    [Label("一定速度"), DisableIf("Stop")]
    public bool ConstantVelocity;

    [Label("停止"), DisableIf("ConstantVelocity")]
    public bool Stop;

    // input type
    [Label("実験データ"), DisableIf("GamepadInput")]
    public bool ExperimentData;

    [Label("入力（ON:ゲームパッド, Off:ハンコン）"), DisableIf("ExperimentData")]
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
