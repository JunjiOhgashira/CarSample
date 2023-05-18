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

                // master
                gm.hat_vm = gm.SF * gm.all[gm.oneWayDelayIndex].vs;
                gm.omegam = gm.CI * gm.deltam - Math.Sqrt(2 * gm.CI) * gm.vm;
                gm.um = (gm.CI * gm.deltam + gm.omegam) / Math.Sqrt(2 * gm.CI);

                // slave
                gm.us = gm.SF * gm.all[gm.oneWayDelayIndex].um;
                gm.deltas = (Math.Sqrt(2 * gm.CI) * gm.us - gm.omegas) / gm.CI;
                //gm.vs = (gm.CI * gm.deltas - gm.omegas) / Math.Sqrt(2 * gm.CI);
                gm.vs = gm.us - Math.Sqrt(2 / gm.CI) * gm.omegas;
            }
        }
    }
}