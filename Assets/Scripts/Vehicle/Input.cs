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
            public double period;
            public double constantTime;
            public double amp;

            private void Start()
            {
                t = 0;
                amp = gm.HandleControllerAngleMax;
                period = 3;
                constantTime = 10.3;
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

                else
                {
                    if (!LogitechGSDK.LogiIsPlaying(0, LogitechGSDK.LOGI_FORCE_SPRING))
                    {
                        LogitechGSDK.LogiPlaySpringForce(0, 0, 40, 100);
                    }

                    if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
                    {
                        LogitechGSDK.DIJOYSTATE2ENGINES rec = LogitechGSDK.LogiGetStateUnity(0);
                        gm.HandleControllerAngle = rec.lX / 32768f * 450;
                        gm.accel = -(rec.lY / 65536f - 0.4999847f) * gm.accelMax;
                        gm.brake = -(rec.lRz / 65536f - 0.4999847f) * gm.brakeMax;
                    }
                }

                //if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
                //{

                //}

                var current = Gamepad.current;

                if (current.buttonEast.isPressed)
                {
                    if (t < period / 4)
                    {
                        Debug.Log("a");
                        gm.HandleControllerAngle = amp * Math.Sin(2 * Math.PI * t / period);
                    }
                    else if (t < period / 4 + constantTime)
                    {
                        Debug.Log("b");
                        gm.HandleControllerAngle = amp;
                    }
                    else if(t < period / 2 + constantTime)
                    {
                        Debug.Log("c");
                        gm.HandleControllerAngle = amp * Math.Sin(2 * Math.PI * (t - constantTime) / period);
                    }
                    else
                    {
                        Debug.Log("d");
                        gm.HandleControllerAngle = 0;
                    }
                    t += Time.deltaTime;
                }

                gm.FrontWheelAngle = gm.HandleControllerAngle / gm.gearRatio;
            }
        }
    }
}