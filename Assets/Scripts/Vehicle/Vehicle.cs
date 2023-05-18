using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Car
{
    namespace Vehicle
    {
        public class Vehicle : MonoBehaviour
        {
            public GameManager gm;

            void FixedUpdate()
            {
                OperationInput();
                VehicleModel();
                GetPosition();
            }

            void OperationInput()
            {
                gm.deltam = gm.FrontWheelAngle;
            }

            void VehicleModel()
            {
                if (!gm.Stop && !(gm.vels < gm.lowerLimitVelocity))
                {
                    gm.A = new double[,] { { -gm.a11 / gm.vels, -1 - gm.a12 / (gm.vels * gm.vels) }, { -gm.a21, -gm.a22 / gm.vels } };
                    gm.B = new double[,] { { gm.b1 / gm.vels }, { gm.b2 } };
                    gm.beta += (gm.A[0, 0] * gm.beta + gm.A[0, 1] * gm.omegas + gm.B[0, 0] * gm.deltas) * gm.dt;
                    gm.omegas += (gm.A[1, 0] * gm.beta + gm.A[1, 1] * gm.omegas + gm.B[1, 0] * gm.deltas) * gm.dt;
                }
            }

            void GetPosition()
            {
                if (gm.WaveIntegral)
                {
                    var deltam_sum = gm.deltam_sum.Sum();
                    var thetas_T = gm.all[gm.oneWayDelayIndex].thetas;
                    var thetam_2T = gm.all[gm.roundTripDelayIndex].thetam;
                    gm.thetam = thetas_T + 0.7 * (thetas_T - thetam_2T) + gm.CII * deltam_sum * gm.dt;
                    //gm.thetam = gm.CII * gm.epsilonm - Math.Sqrt(2 * gm.CII) * gm.Vm;
                }
                else if (gm.WaveVariableTransformation)
                {
                    gm.thetam += gm.omegam * gm.dt;
                }
                gm.pxm = gm.all[gm.oneWayDelayIndex].pxs;
                gm.pym = gm.all[gm.oneWayDelayIndex].pys;
                gm.masterAzimuth = new Vector3(0, (float)gm.thetas - 90, 0);
                gm.masterPosition = new Vector3((float)gm.pym, (float)(gm.height / 2), (float)gm.pxm);

                gm.thetas += gm.omegas * gm.dt;
                gm.pxs += gm.vels * Math.Cos((gm.thetas + gm.beta) * Math.PI / 180) * gm.dt;
                gm.pys += gm.vels * Math.Sin((gm.thetas + gm.beta) * Math.PI / 180) * gm.dt;
                gm.slaveAzimuth = new Vector3(0, (float)gm.thetas, 0);
                gm.slavePosition = new Vector3((float)gm.pys, (float)(gm.height / 2), (float)gm.pxs);
            }
        }
    }
}