using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets.CrossPlatformInput;

namespace Vehicle
{
    public class Car : MonoBehaviour
    {
        public Parameter parameter;
        public ModeChange mode;
        public GetInput getInput;
        public UnitAdjuster unitAdjuster;

        [HideInInspector]
        public double t;
        [HideInInspector]
        public double dt;

        [HideInInspector]
        public double beta;
        [HideInInspector]
        public double omega;
        [HideInInspector]
        public double delta;
        [HideInInspector]
        public double theta;
        [HideInInspector]
        public double px;
        [HideInInspector]
        public double py;

        [HideInInspector]
        public double M;
        [HideInInspector]
        public double I;
        [HideInInspector]
        public double V;
        [HideInInspector]
        public double l;
        [HideInInspector]
        public double lf;
        [HideInInspector]
        public double lr;
        [HideInInspector]
        public double Kf;
        [HideInInspector]
        public double Kr;

        [HideInInspector]
        public double a11;
        [HideInInspector]
        public double a12;
        [HideInInspector]
        public double a21;
        [HideInInspector]
        public double a22;
        [HideInInspector]
        public double b1;
        [HideInInspector]
        public double b2;

        [HideInInspector]
        public double[,] A = new double[2, 2];
        [HideInInspector]
        public double[,] B = new double[2, 1];

        [HideInInspector]
        public double length;
        [HideInInspector]
        public double width;
        [HideInInspector]
        public double height;

        [HideInInspector]
        public double wd;
        [HideInInspector]
        public double ld;
        [HideInInspector]
        public double hd;

        [HideInInspector]
        public double V0;

        [HideInInspector]
        public double accel;
        [HideInInspector]
        public double brake;

        [HideInInspector]
        public double accelMax;
        [HideInInspector]
        public double brakeMax;

        [HideInInspector]
        public double ratio;

        [HideInInspector]
        public bool DelayVehicle;
        [HideInInspector]
        public bool ConstantVelocity;
        [HideInInspector]
        public bool GamepadInput;
        [HideInInspector]
        public bool Outline;

        void Start()
        {
            if (!DelayVehicle)
            {
                DelayVehicle     = mode.DelayVehicle;
                ConstantVelocity = mode.ConstantVelocity;
                GamepadInput     = mode.GamepadInput;
                Outline          = mode.CarOutline;

                t = 0;

                M  = parameter.M;
                I  = parameter.I;
                l  = parameter.l;
                lf = parameter.lf;
                lr = parameter.lr;
                Kf = parameter.Kf;
                Kr = parameter.Kr;

                length = parameter.length;
                width  = parameter.width;
                height = parameter.height;

                a11 = 2 * (Kf + Kr) / M;
                a12 = 2 * (lf * Kf - lr * Kr) / M;
                a21 = 2 * (lf * Kf - lr * Kr) / I;
                a22 = 2 * (lf * lf * Kf + lr * lr * Kr);
                b1  = 2 * Kf / M;
                b2  = 2 * lf * Kf / I;

                accelMax = parameter.accelMax;
                brakeMax = parameter.brakeMax;

                ratio = parameter.ratio;

                theta = 0;
                px = 0;
                py = 0;
                beta = 0;
                omega = 0;
                accel = 0;
                brake = 0;

                length = parameter.length;
                width  = parameter.width;
                height = parameter.height;

                if (ConstantVelocity)
                {
                    A = new double[,] { { -a11 / V, -1 - a12 / (V * V) }, { -a21, -a22 / V } };
                    B = new double[,] { { b1 / V }, { b2 } };
                }

                ld = Outline ? length : 0;
                wd = Outline ? width : 0;
                hd = Outline ? height : 0;

                if (ConstantVelocity)
                {
                    V = unitAdjuster.V_m_s;
                    V0 = V;
                }
                else
                {
                    V0 = unitAdjuster.V_m_s;
                    V = V0;
                }

                this.transform.localScale = new Vector3((float)wd, (float)hd, (float)ld);
            }
        }

        void Update()
        {
            if (!DelayVehicle)
            {
                dt = Time.deltaTime;
                t += dt;
                OperationInput();
                VelocityMode();
                VehicleModel();
                VideoPresentation();
            }
        }

        void OperationInput()
        {
            delta = getInput.FrontWheelAngle;
            accel = getInput.accel;
            brake = getInput.brake;
        }

        void VelocityMode()
        {
            if (!ConstantVelocity)
            {
                double acceleration_value = (V > V0) ? (accel - brake - 0.15) * dt : (accel - brake + 0.15) * dt;
                V += (V < 1 * (1000.0 / 3600.0) && acceleration_value < 0) ? 0 : acceleration_value;
            }
        }

        void VehicleModel()
        {
            A = new double[,] { { -a11 / V, -1 - a12 / (V * V) }, { -a21, -a22 / V } };
            B = new double[,] { { b1 / V }, { b2 } };

            beta += (A[0, 0] * beta + A[0, 1] * omega + B[0, 0] * delta) * dt;
            omega += (A[1, 0] * beta + A[1, 1] * omega + B[1, 0] * delta) * dt;
        }

        void VideoPresentation()
        {
            theta += omega * dt;
            px += V * Math.Cos((theta + beta) * Math.PI / 180) * dt;
            py += V * Math.Sin((theta + beta) * Math.PI / 180) * dt;

            transform.eulerAngles = new Vector3(0f, (float)theta, 0f);
            transform.position = new Vector3((float)py, (float)(height / 2), (float)px);
        }
    }
}
