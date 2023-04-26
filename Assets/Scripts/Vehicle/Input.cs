using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

namespace Car
{
    namespace Vehicle
    {
        public class Input : MonoBehaviour
        {
            public GameManager gm;

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

                gm.FrontWheelAngle = gm.HandleControllerAngle / gm.gearRatio;
            }
        }
    }
}