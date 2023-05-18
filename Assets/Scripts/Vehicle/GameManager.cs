using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Car
{
    public class Variable
    {
        public int date { get; set; }
        public double deltam { get; set; }
        public double deltas { get; set; }
        public double omegam { get; set; }
        public double omegas { get; set; }
        public double epsilonm { get; set; }
        public double epsilons { get; set; }
        public double thetam { get; set; }
        public double thetas { get; set; }
        public double pxm { get; set; }
        public double pxs { get; set; }
        public double pym { get; set; }
        public double pys { get; set; }
        public double velm { get; set; }
        public double vels { get; set; }
        public double um { get; set; }
        public double us { get; set; }
        public double vm { get; set; }
        public double vs { get; set; }
        public double hat_vm { get; set; }
        public double hat_vs { get; set; }
        public double Um { get; set; }
        public double Us { get; set; }
        public double Vm { get; set; }
        public double Vs { get; set; }
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager gm { get; set; }

        public List<Variable> all = new List<Variable>();
        public List<int> times = new List<int>();
        public List<double> deltam_sum = new List<double>();
        public List<double> vs_sum = new List<double>();

        public Parameter parameter;
        public ShareParam shareParam;
        public CsvParam csvParam;
        public ModeChange mode;
        public Tool.UnitAdjuster unitAdjuster;
        public Tool.QuoteFromCsv quoteFromCsv;
        public Input input;
        public Vehicle.GetVelocity getVelocity;
        public Vehicle.WaveIntegral waveIntegral;
        public Vehicle.WaveAdjust waveAdjust;

        [HideInInspector]
        public bool DelayVehicle;
        [HideInInspector]
        public bool WaveVariableTransformation;
        [HideInInspector]
        public bool WaveIntegral;
        [HideInInspector]
        public bool WaveAdjust;
        [HideInInspector]
        public bool LookDown;
        [HideInInspector]
        public bool ConstantVelocity;
        [HideInInspector]
        public bool Stop;
        [HideInInspector]
        public bool ExperimentData;
        [HideInInspector]
        public bool GamepadInput;

        [HideInInspector]
        public double dt;
        [HideInInspector]
        public int delay;
        [HideInInspector]
        public int now;
        [HideInInspector]
        public int oneWayDelayIndex;
        [HideInInspector]
        public int roundTripDelayIndex;

        [HideInInspector]
        public double deltam;
        [HideInInspector]
        public double deltas;
        [HideInInspector]
        public double beta;
        [HideInInspector]
        public double omegam;
        [HideInInspector]
        public double omegas;
        [HideInInspector]
        public double epsilonm;
        [HideInInspector]
        public double epsilons;
        [HideInInspector]
        public double thetam;
        [HideInInspector]
        public double thetas;
        [HideInInspector]
        public double pxm;
        [HideInInspector]
        public double pxs;
        [HideInInspector]
        public double pym;
        [HideInInspector]
        public double pys;
        [HideInInspector]
        public Vector3 masterPosition;
        [HideInInspector]
        public Vector3 slavePosition;
        [HideInInspector]
        public Vector3 masterAzimuth;
        [HideInInspector]
        public Vector3 slaveAzimuth;
        [HideInInspector]
        public double velm;
        [HideInInspector]
        public double vels;
        [HideInInspector]
        public double CI;   // 特性インピーダンス
        [HideInInspector]
        public double CII;
        [HideInInspector]
        public double um;
        [HideInInspector]
        public double us;
        [HideInInspector]
        public double vm;
        [HideInInspector]
        public double vs;
        [HideInInspector]
        public double hat_vs;
        [HideInInspector]
        public double hat_vm;
        [HideInInspector]
        public double Um;
        [HideInInspector]
        public double Us;
        [HideInInspector]
        public double Vm;
        [HideInInspector]
        public double Vs;

        [HideInInspector]
        public double M;
        [HideInInspector]
        public double I;
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
        public double[,] A = new double[2, 2];
        [HideInInspector]
        public double[,] B = new double[2, 1];

        [HideInInspector]
        public double gearRatio;
        [HideInInspector]
        public double accel;
        [HideInInspector]
        public double brake;
        [HideInInspector]
        public double accelMax;
        [HideInInspector]
        public double brakeMax;
        [HideInInspector]
        public double vel0;
        [HideInInspector]
        public double SF;   // スケーリング係数
        [HideInInspector]
        public double SFI;
        [HideInInspector]
        public double power;
        [HideInInspector]
        public double energy;
        [HideInInspector]
        public double lowerLimitVelocity;
        [HideInInspector]
        public double HandleControllerAngle;
        [HideInInspector]
        public double FrontWheelAngle;
        [HideInInspector]
        public double HandleControllerAngleMax;
        [HideInInspector]
        public double lamda;

        private void Awake()
        {
            if (gm == null)
            {
                gm = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            DelayVehicle = mode.DelayVehicle;
            WaveVariableTransformation = mode.WaveVariableTransformation;
            WaveIntegral = mode.WaveIntegral;
            WaveAdjust = mode.WaveAdjust;
            LookDown = mode.LookDown;
            ConstantVelocity = mode.ConstantVelocity;
            Stop = mode.Stop;
            ExperimentData = mode.ExperimentData;
            GamepadInput = mode.GamepadInput;

            delay = parameter.delay;
            oneWayDelayIndex = 0;
            roundTripDelayIndex = 0;

            M = parameter.M;
            I = parameter.I;
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

            velm = unitAdjuster.V_m_s;
            vels = unitAdjuster.V_m_s;
            gearRatio = parameter.gearRatio;
            accelMax = parameter.accelMax;
            brakeMax = parameter.brakeMax;
            vel0 = unitAdjuster.V0_m_s;
            SF = shareParam.SF;
            SFI = shareParam.SFI;
            CI = CISolver();
            CII = shareParam.CII;
            energy = 0;
            lowerLimitVelocity = unitAdjuster.LowerLimitVelocity_m_s;
            HandleControllerAngleMax = parameter.HandleControllerAngleMax;
            lamda = parameter.lamda;
            UpdateValue();
        }

        void FixedUpdate()
        {
            GetTimeInformation();
            UpdateValue();
        }

        void GetTimeInformation()
        {
            dt = Time.deltaTime;
            now = (int)(Time.time * 1000);
            oneWayDelayIndex = OneWayDelayData(now);
            roundTripDelayIndex = RoundTripDelayData(now);
        }

        int DelayData(int pastDate)
        {
            if (pastDate <= 0) return 0;
            else
            {
                var min = times.Min(c => Math.Abs(c - pastDate));
                var nearest = times.First(c => Math.Abs(c - pastDate) == min);
                return times.IndexOf(nearest);
            }
        }

        int OneWayDelayData(int now)
        {
            int pastDate = now - delay;
            return DelayData(pastDate);
        }

        int RoundTripDelayData(int now)
        {
            int pastDate = now - 2 * delay;
            return DelayData(pastDate);
        }

        void UpdateValue()
        {
            times.Add(now);
            deltam_sum.Add(deltam);
            vs_sum.Add(vs);

            Variable d = new Variable();
            d.date = now;
            d.deltam = deltam;
            d.deltas = deltas;
            d.omegam = omegam;
            d.omegas = omegas;
            d.epsilonm = epsilonm;
            d.epsilons = epsilons;
            d.thetam = thetam;
            d.thetas = thetas;
            d.pxm = pxm;
            d.pxs = pxs;
            d.pym = pym;
            d.pys = pys;
            d.velm = velm;
            d.vels = vels;
            d.um = um;
            d.us = us;
            d.vm = vm;
            d.vs = vs;
            d.hat_vs = hat_vs;
            d.hat_vm = hat_vm;
            d.Um = um;
            d.Us = Us;
            d.Vm = Vm;
            d.Vs = Vs;

            all.Add(d);

            while (true)
            {
                if (all[0].date < now - 2 * delay)
                {
                    all.RemoveAt(0);
                    times.RemoveAt(0);
                    deltam_sum.RemoveAt(0);
                    vs_sum.RemoveAt(0);
                }
                else
                {
                    break;
                }
            }
        }

        public double CISolver()
        {
            double velRef = unitAdjuster.Vb_m_s;
            double sf = (M * velRef * velRef * (lf * Kf - lr * Kr)) / (2f * (lf + lr) * (lf + lr) * Kf * Kr);
            double b = (1 / (1 - sf)) * (velRef / (lf + lr));
            return b;
        }
    }
}