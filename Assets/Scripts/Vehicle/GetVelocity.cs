using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Car
{
    namespace Vehicle
    {
        public class GetVelocity : MonoBehaviour
        {
            public GameManager gm;

            void FixedUpdate()
            {
                gm.vels = gm.all[gm.oneWayDelayIndex].velm;

                if (gm.ConstantVelocity)
                {
                    gm.velm = gm.unitAdjuster.V_m_s;
                }
                else if (gm.Stop)
                {
                    gm.velm = 0;
                }
                else
                {
                    if (gm.velm > gm.lowerLimitVelocity)
                    {
                        gm.velm += (gm.accel - gm.brake) * gm.dt;
                    }
                    if (gm.velm > gm.vel0)
                    {
                        gm.velm -= 0.3 * gm.dt;
                    }
                    else
                    {
                        gm.velm += 0.3 * gm.dt;
                    }
                }
            }
        }
    }
}