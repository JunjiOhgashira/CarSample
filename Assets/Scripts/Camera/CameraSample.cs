using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CameraSample
{
    public class CameraSample : MonoBehaviour
    {
        public Parameter parameter;
        public ModeChange mode;

        public Vehicle.Car car;
        public Vehicle.Master master;
        public Vehicle.Slave slave;

        Vector3 carPosition;
        Vector3 offset;

        public double TurningAngle;

        void Update()
        {
            //TurningAngle = mode.DelayVehicle ? master.thetam : car.theta;
            TurningAngle = mode.DelayVehicle ? (mode.ProposedMethod ? master.thetam : slave.thetasPast) : car.theta;

            offset = mode.Mounted ? new Vector3(0f, 10f, 0f) : new Vector3(0f, (float)(parameter.height / 2) + 0.2f, 0f);
            carPosition = mode.DelayVehicle ? master.transform.position : car.transform.position;
            gameObject.transform.position = carPosition + offset;
            transform.eulerAngles = mode.Mounted ? new Vector3(90, (float)TurningAngle, 0) : new Vector3(10, (float)TurningAngle, 0);
        }
    }
}