using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Car
{
    namespace Vehicle
    {
        public class WaveAdjust : MonoBehaviour
        {
            public GameManager gm;
            public double theta_actual;
            public double omegas_sum_0;
            public double omegam_sum_T;
            public double us_sum_0;
            public double vs_sum_0;
            public double um_sum_T;
            public double vm_sum_T;
            public double vs_sum_2T;
            public double theta_predicted;
            public double delta_vs;
            public double d;

            public void Communication()
            {
                theta_actual = gm.thetas - gm.all[gm.oneWayDelayIndex].thetam;
                omegas_sum_0 += gm.omegas * gm.dt;
                omegam_sum_T += gm.all[gm.oneWayDelayIndex].omegam * gm.dt;
                us_sum_0 += gm.us * gm.dt;
                vs_sum_0 += gm.vs * gm.dt;
                um_sum_T += gm.all[gm.oneWayDelayIndex].um * gm.dt;
                vm_sum_T += gm.all[gm.oneWayDelayIndex].vm * gm.dt;
                vs_sum_2T += gm.all[gm.roundTripDelayIndex].vs * gm.dt;
                theta_predicted = -Math.Sqrt(gm.CI / 2) * (vs_sum_0 - vs_sum_2T);
                d = theta_actual - theta_predicted;
                delta_vs = -Math.Sqrt(2 / gm.CI) * d * gm.lamda;
                gm.vs = gm.hat_vs + delta_vs;

                if (gm.SF * gm.vs < gm.hat_vs)
                {
                    Debug.Log("a");
                }
                else
                {
                    Debug.Log("b");
                }

                //if (d * gm.hat_vs <= 0)
                //{
                //    Debug.Log("a");
                //    delta_vs = 0;
                //}
                //else if (Math.Sqrt(2 / gm.CI) * gm.lamda * Math.Abs(d) < Math.Abs(gm.hat_vs))
                //{
                //    Debug.Log("b");
                //    delta_vs = -Math.Sqrt(2 / gm.CI) * gm.lamda * d;
                //}
                //else
                //{
                //    Debug.Log("c");
                //    delta_vs = -gm.hat_vs;
                //}

                Debug.Log(gm.hat_vs - gm.SF * gm.vs);
            }
        }
    }
}