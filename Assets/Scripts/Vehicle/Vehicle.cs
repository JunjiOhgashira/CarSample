using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.InputSystem;

namespace Car
{
    namespace Vehicle
    {
        public class Vehicle : MonoBehaviour
        {
            public GameManager gm;
            public LPF lPF;

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