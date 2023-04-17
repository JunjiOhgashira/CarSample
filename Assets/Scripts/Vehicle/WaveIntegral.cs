using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vehicle
{
    public class WaveIntegral : MonoBehaviour
    {
        public Master master;
        public Slave slave;

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
        public double tmp;

        [HideInInspector]
        public double thetam;
        [HideInInspector]
        public double thetas_T;
        [HideInInspector]
        public double thetam_2T;
        [HideInInspector]
        public double deltam_integral;
        [HideInInspector]
        public double b;
        [HideInInspector]
        public double damper;

        void Start()
        {
            b = 0.5;
            damper = 0.5;
        }

        void Update()
        {
            GetTimeInformation();
            GetThetam();
        }

        void GetTimeInformation()
        {
            dt = Time.deltaTime;
            now = (int)(Time.time * 1000);
            oneWayDelayIndex = master.oneWayDelayIndex;
            roundTripDelayIndex = master.roundTripDelayIndex;
            tmp = Time.time;
        }

        void GetThetam()
        {
            thetas_T = slave.AllData[oneWayDelayIndex].thetas;
            thetam_2T = master.AllData[roundTripDelayIndex].thetam;
            deltam_integral = master.deltam_2T;
            thetam = thetas_T + (thetas_T - thetam_2T) * damper + b * deltam_integral * dt;
        }
    }
}