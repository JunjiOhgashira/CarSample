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

            public LPF lPF;

            double vm_cur_output;
            double vm_pre_output;
            double vm_cur_input;
            double vm_pre_input;

            double us_cur_output;
            double us_pre_output;
            double us_cur_input;
            double us_pre_input;

            void FixedUpdate()
            {
                if (gm.WaveVariableTransformation)
                {
                    Communication();
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
                if (gm.WaveFilter)
                {
                    vm_cur_input = gm.all[gm.oneWayDelayIndex].vs;
                    vm_cur_output = lPF.Filter(vm_pre_input, vm_cur_input, vm_pre_output);
                    vm_pre_input = vm_cur_input;
                    vm_pre_output = vm_cur_output;
                    gm.vm = vm_cur_output;
                }
                else
                {
                    gm.vm = gm.SF * gm.all[gm.oneWayDelayIndex].vs;
                }

                gm.omegam = gm.CI * gm.deltam - Math.Sqrt(2 * gm.CI) * gm.vm;
                gm.um = (gm.CI * gm.deltam + gm.omegam) / Math.Sqrt(2 * gm.CI);
            }

            void SlaveCommunication()
            {
                if (gm.WaveFilter)
                {
                    us_cur_input = gm.SF * gm.all[gm.oneWayDelayIndex].um;
                    us_cur_output = lPF.Filter(us_pre_input, us_cur_input, us_pre_output);
                    us_pre_input = us_cur_input;
                    us_pre_output = us_cur_output;
                    gm.us = us_cur_output;
                }
                else
                {
                    gm.us = gm.SF * gm.all[gm.oneWayDelayIndex].um;
                }

                gm.deltas = (Math.Sqrt(2 * gm.CI) * gm.us - gm.omegas) / gm.CI;
                gm.vs = gm.us - Math.Sqrt(2 / gm.CI) * gm.omegas;
            }
        }
    }
}