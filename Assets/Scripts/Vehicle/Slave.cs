using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Vehicle
{
    public class SlaveData
    {
        public int date { get; set; }
        public double betas { get; set; }
        public double omegas { get; set; }
        public double deltas { get; set; }
        public double thetas { get; set; }
        public double pxs { get; set; }
        public double pys { get; set; }
        public double Vs { get; set; }
        public double us { get; set; }
        public double vs { get; set; }
    }

    public class Slave : MonoBehaviour
    {
        public Parameter parameter;
        public ShareParam shareParam;
        public ModeChange mode;
        public UnitAdjuster unitAdjuster;

        public Master master;

        public List<SlaveData> AllData = new List<SlaveData>();
        public List<int> times = new List<int>();

        [HideInInspector]
        public double dt;
        [HideInInspector]
        public int delay;
        [HideInInspector]
        public int now;
        [HideInInspector]
        public int oneWayDelayIndex;

        [HideInInspector]
        public double betas;
        [HideInInspector]
        public double omegas;
        [HideInInspector]
        public double deltas;
        //[HideInInspector]
        public double thetas;
        [HideInInspector]
        public double pxs;
        [HideInInspector]
        public double pys;
        [HideInInspector]
        public double Vs;
        [HideInInspector]
        public double us;
        [HideInInspector]
        public double vs;

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
        public double tmp;

        [HideInInspector]
        public double D;

        [HideInInspector]
        public double thetasPast;
        [HideInInspector]
        public double pysPast;
        [HideInInspector]
        public double pxsPast;

        [HideInInspector]
        public double[,] A = new double[2, 2];
        [HideInInspector]
        public double[,] B = new double[2, 1];

        [HideInInspector]
        public bool ProposedMethod;
        [HideInInspector]
        public bool ZeroVelocity;
        [HideInInspector]
        public bool ConstantVelocity;
        [HideInInspector]
        public bool DelayVehicle;
        [HideInInspector]
        public bool Outline;

        void Start()
        {
            ZeroVelocity = mode.ZeroVelocity;
            DelayVehicle = mode.DelayVehicle;
            ProposedMethod   = mode.ProposedMethod;
            ConstantVelocity = mode.ConstantVelocity;
            Outline          = mode.SlaveOutline;

            delay = parameter.delay;

            M  = parameter.M;
            I  = parameter.I;
            V  = unitAdjuster.V_m_s;
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
            a22 = 2 * (lf * lf * Kf + lr * lr * Kr) / I;
            b1  = 2 * Kf / M;
            b2  = 2 * lf * Kf / I;

            A = new double[,] { { -a11 / V, -1 - a12 / (V * V) }, { -a21, -a22 / V } };
            B = new double[,] { { b1 / V }, { b2 } };

            ld = Outline ? length : 0;
            wd = Outline ? width  : 0;
            hd = Outline ? height : 0;

            V0 = unitAdjuster.V0_m_s;

            D = shareParam.D;

            this.transform.localScale = new Vector3((float)wd, (float)hd, (float)ld);

            SlaveData d = new SlaveData();
            d.date   = 0;
            d.betas  = 0;
            d.omegas = 0;
            d.deltas = 0;
            d.thetas = 0;
            d.pxs    = 0;
            d.pys    = 0;
            d.Vs     = ConstantVelocity ? V : V0;
            d.us     = 0;
            d.vs     = 0;

            AllData.Add(d);
        }

        void FixedUpdate()
        {
            GetTimeInformation();
            GetVelocity();
            GetSteeringInput();
            VehicleModel();
            UpdatePosition();
            UpdateValue();
        }

        void GetTimeInformation()
        {
            dt = Time.deltaTime;
            now = (int)(Time.time * 1000);
            oneWayDelayIndex = master.oneWayDelayIndex;
            tmp = Time.time;
        }

        void GetVelocity()
        {
            if (!ConstantVelocity) Vs = master.AllData[oneWayDelayIndex].Vm;
        }

        void GetSteeringInput()
        {
            if (ProposedMethod)
            {
                double b = master.AllData[oneWayDelayIndex].b;

                us = D * master.AllData[oneWayDelayIndex].um;
                deltas = (Math.Sqrt(2 * b) * us - omegas) / b;
                vs = (b * deltas - omegas) / Math.Sqrt(2 * b);
            }

            else
            {
                deltas = master.AllData[oneWayDelayIndex].deltam;
            }
        }

        void VehicleModel()
        {
            A = new double[,] { { -a11 / V, -1 - a12 / (V * V) }, { -a21, -a22 / V } };
            B = new double[,] { { b1 / V }, { b2 } };

            if (ZeroVelocity)
            {
                betas += 0;
                omegas += 0;
            }
            else
            {
                betas += (A[0, 0] * betas + A[0, 1] * omegas + B[0, 0] * deltas) * dt;
                omegas += (A[1, 0] * betas + A[1, 1] * omegas + B[1, 0] * deltas) * dt;
            }
        }

        void UpdatePosition()
        {
            thetas += omegas * dt;

            if (ZeroVelocity)
            {
                pxs += 0;
                pys += 0;
            }
            else
            {
                pxs += V * Math.Cos((thetas + betas) * Math.PI / 180) * dt;
                pys += V * Math.Sin((thetas + betas) * Math.PI / 180) * dt;
            }

            transform.eulerAngles = new Vector3(0, (float)thetas, 0);
            transform.position = new Vector3((float)pys, (float)(height / 2), (float)pxs);

            thetasPast = AllData[oneWayDelayIndex].thetas;
            pysPast = AllData[oneWayDelayIndex].pys;
            pxsPast = AllData[oneWayDelayIndex].pxs;
        }

        void UpdateValue()
        {
            SlaveData d = new SlaveData();
            d.date = now;
            d.betas = betas;
            d.omegas = omegas;
            d.deltas = deltas;
            d.thetas = thetas;
            d.pxs = pxs;
            d.pys = pys;
            d.Vs = Vs;
            d.us = us;
            d.vs = vs;

            AllData.Add(d);
        }
    }
}