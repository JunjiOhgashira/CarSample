using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Car
{
    namespace Vehicle
    {
        public class WaveIntegral : MonoBehaviour
        {
            public GameManager gm;

            [HideInInspector]
            public double thetas_T;
            [HideInInspector]
            public double thetam_2T;
            [HideInInspector]
            public double deltam_integral;
            [HideInInspector]
            public double CI;   // 特性インピーダンス
            [HideInInspector]
            public double damper;

            void Start()
            {
                CI = 0.5;
                damper = 0.5;
            }

            void Update()
            {
                //if (waveIntegral)
                //{
                //    GetThetam();
                //}
            }

            void FixedUpdate()
            {
            }

            void GetThetam()
            {
                //thetas_T = all[oneWayDelayIndex].thetas;
                //thetam_2T = all[roundTripDelayIndex].thetam;
                //deltam_integral = master.deltam_2T;
                //thetam = thetas_T + (thetas_T - thetam_2T) * damper + CI * deltam_integral * dt;
            }
        }
    }
}