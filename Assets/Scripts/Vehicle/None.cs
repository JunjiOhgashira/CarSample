using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Car
{
    namespace Vehicle
    {
        public class None : MonoBehaviour
        {
            public GameManager gm;

            void FixedUpdate()
            {
                if (!gm.WaveVariableTransformation)
                {
                    Communication();
                }
            }

            void Communication()
            {
                MasterCommunication();
                SlaveCommunication();
            }

            void MasterCommunication()
            {
                gm.omegam = gm.all[gm.oneWayDelayIndex].omegas;
            }

            void SlaveCommunication()
            {
                gm.deltas = gm.all[gm.oneWayDelayIndex].deltam;
            }
        }
    }
}