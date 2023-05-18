using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Car
{
    namespace Vehicle
    {
        public class WaveAdjustSecond : MonoBehaviour
        {
            public GameManager gm;
            public double theta_actual;
            public double theta_predicted;
            public double um_sum_0;
            public double um_sum_2T;
            public double d;
            public double delta_vm;

            public void Communication()
            {
                theta_actual = gm.thetam - gm.all[gm.oneWayDelayIndex].thetas;
                um_sum_0 += gm.um * gm.dt;
                um_sum_2T += gm.all[gm.oneWayDelayIndex].um * gm.dt;
                theta_predicted = Math.Sqrt(gm.CI / 2) * (um_sum_0 - um_sum_2T);
                d = theta_predicted - theta_actual;
                delta_vm = -Math.Sqrt(2 / gm.CI) * d * gm.lamda;
                gm.omegam -= delta_vm * 0.5;

                //if((-gm.hat_vm + Math.Sqrt(2 * gm.CI) * gm.deltam - delta_vm) * gm.deltam >= 0)
                //{
                //    Debug.Log("a");
                //    gm.vm = gm.hat_vm + delta_vm;
                //}
                //else
                //{
                //    Debug.Log("b");
                //    gm.vm = gm.hat_vm;
                //}
            }
        }
    }
}