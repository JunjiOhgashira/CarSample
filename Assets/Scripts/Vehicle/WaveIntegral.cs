using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Car
{
    namespace Vehicle
    {
        public class WaveIntegral : MonoBehaviour
        {
            public GameManager gm;

            [HideInInspector]
            public double deltam_integral;

            void FixedUpdate()
            {
                if (gm.WaveIntegral)
                {
                    Communication();
                }
            }

            void Communication()
            {
                // master
                gm.epsilonm += gm.deltam * gm.dt;
                gm.Vm = gm.SFI * gm.all[gm.oneWayDelayIndex].Vs;
                gm.Um = (gm.CII * gm.epsilonm + gm.thetam) / Math.Sqrt(2 * gm.CII);

                // slave
                gm.Us = gm.SFI * gm.all[gm.oneWayDelayIndex].Um;
                gm.epsilons = (Math.Sqrt(2 * gm.CII) * gm.Us - gm.thetas) / gm.CII;
                gm.Vs = (gm.CII * gm.epsilons - gm.thetas) / Math.Sqrt(2 * gm.CII);
            }
        }
    }
}