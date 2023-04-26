using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Car
{
    namespace Vehicle
    {
        public class EnergyReserver : MonoBehaviour
        {
            public GameManager gm;

            void FixedUpdate()
            {
                gm.power = gm.all[gm.oneWayDelayIndex].deltam * gm.all[gm.oneWayDelayIndex].omegam - gm.all[gm.oneWayDelayIndex].deltas * gm.all[gm.oneWayDelayIndex].omegas;
                gm.energy += gm.power * gm.dt;
            }
        }
    }
}