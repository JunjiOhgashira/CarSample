using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Car
{
    namespace Vehicle
    {
        public class Input : MonoBehaviour
        {
            public GameManager gm;

            public double t;
            public double step;
            public double period;
            public double constantTime;
            public double amp;

            private void Start()
            {
                t = 0;
                step = 0.5;
                amp = gm.HandleControllerAngleMax;
                period = 1;
                constantTime = 100;
            }

            void FixedUpdate()
            {
                if (gm.ExperimentData)
                {
                    gm.HandleControllerAngle = gm.quoteFromCsv.HandleControllerAngle;
                }

                else if (gm.GamepadInput)
                {
                    var s = Gamepad.current.leftStick.ReadValue();
                    var a = Gamepad.current.rightStick.ReadValue();
                    gm.HandleControllerAngle = s.x * gm.HandleControllerAngleMax;
                    var ac = a.y * gm.accelMax;
                    gm.accel = ac > 0 ? ac : 0;
                    gm.brake = ac < 0 ? 0 : ac;
                }

                else if (gm.HandleController)
                {
                    if (!LogitechGSDK.LogiIsPlaying(0, LogitechGSDK.LOGI_FORCE_SPRING))
                    {
                        LogitechGSDK.LogiPlaySpringForce(0, 0, 40, 100);
                    }

                    if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
                    {
                        LogitechGSDK.DIJOYSTATE2ENGINES rec = LogitechGSDK.LogiGetStateUnity(0);
                        // gm.HandleControllerAngle = rec.lX / 32768f * 450;
                        gm.HandleControllerAngle = rec.lX / 32768f * 450 * 3;
                        gm.accel = -(rec.lY / 65536f - 0.4999847f) * gm.accelMax;
                        gm.brake = -(rec.lRz / 65536f - 0.4999847f) * gm.brakeMax;
                    }
                }

                double sign = 0.0;

                if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
                {
                    if (Keyboard.current.rightArrowKey.isPressed)
                    {
                        sign = 1.0;
                    }
                    if (Keyboard.current.leftArrowKey.isPressed)
                    {
                        sign = -1.0;
                    }

                    if (t < period / 2)
                    {
                        gm.HandleControllerAngle = sign * amp / 2 * (Math.Sin(2 * Math.PI * (t - period / 4) / period) + 1);
                    }
                    else if (t < period / 2 + constantTime)
                    {
                        gm.HandleControllerAngle = sign * amp;
                    }
                    else if (t < period + constantTime)
                    {
                        gm.HandleControllerAngle = sign * amp / 2 * (Math.Sin(2 * Math.PI * (t - (constantTime + period / 4)) / period) + 1);
                    }

                    t += Time.deltaTime;
                }
                else
                {
                    t = 0.0;
                    gm.HandleControllerAngle -= 3 * gm.HandleControllerAngle * gm.dt;
                }

                if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.downArrowKey.isPressed)
                {
                    if (Keyboard.current.upArrowKey.isPressed)
                    {
                        gm.accel = gm.accelMax;
                    }
                    if (Keyboard.current.downArrowKey.isPressed)
                    {
                        gm.brake = gm.brakeMax;
                    }
                }
                else
                {
                    gm.accel = 0.0;
                    gm.brake = 0.0;
                }

                gm.FrontWheelAngle = gm.HandleControllerAngle / gm.gearRatio;
            }
        }
    }
}