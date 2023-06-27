using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Car
{
    namespace Vehicle
    {
        public class WaveNormal : MonoBehaviour
        {
            public GameManager gm;

            public WaveAdjust waveAdjust;
            public WaveAdjustSecond waveAdjustSecond;

            public double pre_us;
            public double pre_vm;
            public double vm;

            void FixedUpdate()
            {
                if (gm.WaveVariableTransformation)
                {
                    Communication();

                    if (gm.WaveAdjust)
                    {
                        waveAdjustSecond.Communication();
                    }
                }
            }

            void Communication()
            {
                gm.CI = gm.CISolver();
                MasterCommunication();
                SlaveCommunication();
            }

            void MasterCommunication()
            {
                if (gm.waveAdjust)
                {
                    gm.hat_vm = gm.SF * gm.all[gm.oneWayDelayIndex].vs;
                }
                else
                {
                    gm.vm = gm.SF * gm.all[gm.oneWayDelayIndex].vs;
                    //double vm_dif = (vm - pre_vm) / gm.dt;
                    ////gm.vm = vm * 1 / (1 + Math.Pow(10000000000000000000 * vm_dif, 2));
                    //gm.vm = vm - 1 * vm_dif * gm.dt;
                    //pre_us = gm.us;
                }
                gm.omegam = gm.CI * gm.deltam - Math.Sqrt(2 * gm.CI) * gm.vm;
                gm.um = (gm.CI * gm.deltam + gm.omegam) / Math.Sqrt(2 * gm.CI);
            }

            void SlaveCommunication()
            {
                gm.us = gm.SF * gm.all[gm.oneWayDelayIndex].um;

                //double us_dif = (gm.us - pre_us) / gm.dt;
                //gm.us *= 1 / (1 + Math.Pow(0.0008 * us_dif, 2));
                //pre_us = gm.us;

                gm.deltas = (Math.Sqrt(2 * gm.CI) * gm.us - gm.omegas) / gm.CI;
                gm.vs = gm.us - Math.Sqrt(2 / gm.CI) * gm.omegas;
            }
        }
    }
}