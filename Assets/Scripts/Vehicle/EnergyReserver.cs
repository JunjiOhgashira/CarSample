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
                gm.power = gm.deltam * gm.omegam - gm.deltas * gm.omegas;
                gm.energy += gm.power * gm.dt;
            }
        }
    }
}