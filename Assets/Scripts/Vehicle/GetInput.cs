using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;


namespace Vehicle
{
    public class GetInput : MonoBehaviour
    {
        public Parameter parameter;
        public ModeChange modeChange;
        public QuoteFromCsv quoteFromCsv;

        public Master master;

        public double HandleControllerAngle;
        public double FrontWheelAngle;
        public double SteeringGearRatio;

        public double accel;
        public double brake;
        public double accelMax;
        public double brakeMax;

        public double zero;
        public double peak;
        public double period;
        public double amp;
        void Start()
        {
            SteeringGearRatio = parameter.ratio;

            accelMax = parameter.accelMax;
            brakeMax = parameter.brakeMax;

            zero = 1;
            peak = 3;
            period = 2;
            amp = 100;
        }

        void Update()
        {
            if (modeChange.ExperimentData)
            {
                HandleControllerAngle = quoteFromCsv.HandleControllerAngle;
            }

            else if (modeChange.GamepadInput)
            {
                //deltam = 10 * Math.Sin(Time.time * 5);
                var a = Gamepad.current.leftStick.ReadValue();
                HandleControllerAngle = a.x * 350;

                // sin“ü—Í
                //if (Time.time < zero)
                //{
                //    HandleControllerAngle = 0;
                //}
                //else if (Time.time < zero + period / 2)
                //{
                //    HandleControllerAngle = Math.Pow(Math.Sin(Math.PI * (Time.time - zero) / period), 2) * amp;
                //}
                //else if (Time.time < zero + period / 2 + peak)
                //{
                //    HandleControllerAngle = amp;
                //}
                //else if (Time.time < zero + period + peak)
                //{
                //    HandleControllerAngle = Math.Pow(Math.Sin(Math.PI * (Time.time - zero - peak) / period), 2) * amp;
                //}
                //else
                //{
                //    HandleControllerAngle = 0;
                //}

                //if (Input.GetKey(KeyCode.UpArrow))
                //{
                //    accel = 1.0 * accelMax;
                //    brake = 0.0;
                //}
                //else if (Input.GetKey(KeyCode.DownArrow))
                //{
                //    accel = 0.0;
                //    brake = 1.0 * brakeMax;
                //}
                //else
                //{
                //    accel = 0.0;
                //    brake = 0.0;
                //}
            }

            else
            {
                if (!LogitechGSDK.LogiIsPlaying(0, LogitechGSDK.LOGI_FORCE_SPRING))
                {
                    LogitechGSDK.LogiPlaySpringForce(0, 0, 40, 100);
                }

                if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
                {
                    LogitechGSDK.DIJOYSTATE2ENGINES rec = LogitechGSDK.LogiGetStateUnity(0);
                    HandleControllerAngle = rec.lX / 32768f * 450;
                    accel = -(rec.lY / 65536f - 0.4999847f) * accelMax;
                    brake = -(rec.lRz / 65536f - 0.4999847f) * brakeMax;
                }
            }

            FrontWheelAngle = HandleControllerAngle / SteeringGearRatio;

            if (Time.time > zero + peak + period + 50)
            {

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
            }
        }
    }
}