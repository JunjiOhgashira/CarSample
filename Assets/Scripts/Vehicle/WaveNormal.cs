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

            void FixedUpdate()
            {
                if (gm.WaveVariableTransformation)
                {
                    OperationInput();
                    Communication();
                    VehicleModel();
                    GetPosition();
                }
            }

            void OperationInput()
            {
                gm.deltam = gm.FrontWheelAngle;
            }

            void Communication()
            {
                gm.b = gm.BSolver();

                // master
                gm.vm = gm.D * gm.all[gm.oneWayDelayIndex].vs;
                gm.omegam = gm.b * gm.deltam - Math.Sqrt(2 * gm.b) * gm.vm;
                gm.um = (gm.b * gm.deltam + gm.omegam) / Math.Sqrt(2 * gm.b);

                // slave
                gm.us = gm.D * gm.all[gm.oneWayDelayIndex].um;
                gm.deltas = (Math.Sqrt(2 * gm.b) * gm.us - gm.omegas) / gm.b;
                gm.vs = (gm.b * gm.deltas - gm.omegas) / Math.Sqrt(2 * gm.b);
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
                gm.thetam += gm.omegam * gm.dt;
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