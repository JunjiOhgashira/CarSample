using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Vehicle
{
    public class MasterData
    {
        public int date { get; set; }
        public double deltam { get; set; }
        public double deltas { get; set; }
        public double omegam { get; set; }
        public double omegas { get; set; }
        public double pxm { get; set; }
        public double pym { get; set; }
        public double thetam { get; set; }
        public double Vm { get; set; }
        public double Vs { get; set; }
        public double betas { get; set; }
        public double b { get; set; }
        public double um { get; set; }
        public double vm { get; set; }
    }

    public class Master : MonoBehaviour
    {
        public Parameter parameter;
        public ShareParam shareParam;
        public ModeChange mode;
        public UnitAdjuster unitAdjuster;

        public Slave slave;
        public GetInput getInput;

        public List<MasterData> AllData = new List<MasterData>();
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
        public double omegam;
        [HideInInspector]
        public double deltam;
        [HideInInspector]
        public double thetam;
        [HideInInspector]
        public double pxm;
        [HideInInspector]
        public double pym;
        [HideInInspector]
        public double Vm;
        [HideInInspector]
        public double deltas;
        [HideInInspector]
        public double betas;
        [HideInInspector]
        public double omegas;
        [HideInInspector]
        public double Vs;
        [HideInInspector]
        public double b;
        [HideInInspector]
        public double um;
        [HideInInspector]
        public double vm;

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
        public double[,] A = new double[2, 2];
        [HideInInspector]
        public double[,] B = new double[2, 1];

        [HideInInspector]
        public double noInputTime;
        [HideInInspector]
        public double ratio;

        [HideInInspector]
        public double accel;
        [HideInInspector]
        public double brake;

        [HideInInspector]
        public double accelMax;
        [HideInInspector]
        public double brakeMax;

        [HideInInspector]
        public double omegamSum;

        [HideInInspector]
        public double tmp;

        [HideInInspector]
        public double thetaError;

        [HideInInspector]
        public double D;

        [HideInInspector]
        public double power;
        [HideInInspector]
        public double energy;

        [HideInInspector]
        public bool DelayVehicle;
        [HideInInspector]
        public bool ProposedMethod;
        [HideInInspector]
        public bool ConstantVelocity;
        [HideInInspector]
        public bool ConstantB;
        [HideInInspector]
        public bool GamepadInput;
        [HideInInspector]
        public bool Outline;

        void Start()
        {
            if (DelayVehicle)
            {
                DelayVehicle     = mode.DelayVehicle;
                ProposedMethod   = mode.ProposedMethod;
                ConstantVelocity = mode.ConstantVelocity;
                ConstantB        = mode.ConstantB;
                GamepadInput     = mode.GamepadInput;
                Outline          = mode.MasterOutline;

                delay = parameter.delay;

                M = parameter.M;
                I = parameter.I;
                V = unitAdjuster.V_m_s;
                l = parameter.l;
                lf = parameter.lf;
                lr = parameter.lr;
                Kf = parameter.Kf;
                Kr = parameter.Kr;

                length = parameter.length;
                width = parameter.width;
                height = parameter.height;

                a11 = 2 * (Kf + Kr) / M;
                a12 = 2 * (lf * Kf - lr * Kr) / M;
                a21 = 2 * (lf * Kf - lr * Kr) / I;
                a22 = 2 * (lf * lf * Kf + lr * lr * Kr) / I;
                b1 = 2 * Kf / M;
                b2 = 2 * lf * Kf / I;

                if (ConstantVelocity)
                {
                    A = new double[,] { { -a11 / V, -1 - a12 / (V * V) }, { -a21, -a22 / V } };
                    B = new double[,] { { b1 / V }, { b2 } };
                }

                //ld = Outline ? length : 0;
                //wd = Outline ? width  : 0;
                //hd = Outline ? height : 0;

                if (ConstantVelocity)
                {
                    V = unitAdjuster.V_m_s;
                    V0 = V;
                }
                else
                {
                    V0 = unitAdjuster.V0_m_s;
                    V = V0;
                }

                noInputTime = 0;

                omegamSum = 0;

                ratio = parameter.ratio;

                accelMax = parameter.accelMax;
                brakeMax = parameter.brakeMax;

                D = shareParam.D;
                b = BSolver();

                energy = 0;

                //this.transform.localScale = new Vector3((float)wd, (float)hd, (float)ld);

                oneWayDelayIndex = 0;

                MasterData d = new MasterData();
                d.date = 0;
                d.deltam = 0;
                d.deltas = 0;
                d.omegam = 0;
                d.omegas = 0;
                d.pxm = 0;
                d.pym = 0;
                d.thetam = 0;
                d.Vm = ConstantVelocity ? 0 : V0;
                d.Vs = ConstantVelocity ? 0 : V0;
                d.betas = 0;
                d.b = b;
                d.um = 0;
                d.vm = 0;

                AllData.Add(d);
            }
        }

        void FixedUpdate()
        {
            if (DelayVehicle)
            {
                GetTimeInformation();
                OperationInput();
                VelocityMode();
                CommunicationMode();
                VideoPresentation();
                UpdateValue();
                EnergyReserver();
            }
        }

        void GetTimeInformation()
        {
            dt = Time.deltaTime;
            now = (int)(Time.time * 1000);
            oneWayDelayIndex = OneWayDelayData(now);
            tmp = Time.time;
        }

        int OneWayDelayData(int now)
        {
            int pastDate = now - delay;
            if (pastDate < 0) return 0;
            else
            {
                int count = 0;
                while (true)
                {
                    if (times.Contains(pastDate - count))
                    {
                        int index = times.IndexOf(pastDate - count);
                        return index;
                    }
                    else
                    {
                        count++;
                    }
                }
            }
        }

        void OperationInput()
        {
            deltam = getInput.FrontWheelAngle;
            accel = getInput.accel;
            brake = getInput.brake;
        }

        void VelocityMode()
        {
            if (!ConstantVelocity)
            {
                double acceleration_value = (Vm > V0) ? (accel - brake - 0.15) * dt : (accel - brake + 0.15) * dt;
                Vm += Vm < 1 * (1000.0 / 3600.0) ? 0 : acceleration_value;
                b = BSolver();
            }
        }

        double BSolver()
        {
            double Vref = ConstantB ? unitAdjuster.Vb_m_s : (ConstantVelocity ? unitAdjuster.V_m_s : Vm);
            double sf = (M * Vref * Vref * (lf * Kf - lr * Kr)) / (2f * (lf + lr) * (lf + lr) * Kf * Kr);
            double b = (1 / (1 - sf)) * (Vref / (lf + lr));
            return b;
        }

        void CommunicationMode()
        {
            if (ProposedMethod)
            {
                // master
                vm = D * slave.AllData[oneWayDelayIndex].vs;
                omegam = b * deltam - Math.Sqrt(2 * b) * vm;
                um = (b * deltam + omegam) / Math.Sqrt(2 * b);
            }

            else
            {
                omegam = slave.AllData[oneWayDelayIndex].omegas;
            }
        }

        void VideoPresentation()
        {
            omegamSum += omegam - AllData[oneWayDelayIndex].omegam;
            //thetam += omegam * dt;
            thetam = slave.thetasPast + omegamSum * dt;

            pxm = slave.AllData[oneWayDelayIndex].pxs;
            pym = slave.AllData[oneWayDelayIndex].pys;

            transform.eulerAngles = new Vector3(0f, (float)(slave.thetasPast) - 90, 0f);
            transform.position = new Vector3((float)pym, (float)(height / 2), (float)pxm);

            thetaError = thetam - slave.AllData[oneWayDelayIndex].thetas;

        }

        void UpdateValue()
        {
            times.Add(now);

            MasterData d = new MasterData();
            d.date = now;
            d.deltam = deltam;
            d.deltas = deltas;
            d.omegam = omegam;
            d.omegas = omegas;
            d.pxm = pxm;
            d.pym = pym;
            d.thetam = thetam;
            d.Vm = Vm;
            d.Vs = Vs;
            d.betas = betas;
            d.b = b;
            d.um = um;
            d.vm = vm;

            AllData.Add(d);
        }

        void EnergyReserver()
        {
            power = AllData[oneWayDelayIndex].deltam * AllData[oneWayDelayIndex].omegam - slave.AllData[oneWayDelayIndex].deltas * slave.AllData[oneWayDelayIndex].omegas;
            energy += power * dt;
        }

    }
}