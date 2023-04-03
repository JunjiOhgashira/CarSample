using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets.CrossPlatformInput;

namespace UI
{
    public class Steering : MonoBehaviour
    {
        public Parameter parameter;

        public Vehicle.GetInput getInput;

        public double HandleControllerAngle;
        Quaternion rot, rot_ini, rot_end;
        Vector3 ini = new Vector3(0.0f, -1.0f, 1.0f);

        void Start()
        {
            rot_ini = Quaternion.AngleAxis(303, Vector3.right);
        }

        void FixedUpdate()
        {
            HandleControllerAngle = getInput.HandleControllerAngle;

            rot = Quaternion.AngleAxis(-(float)HandleControllerAngle, ini);
            gameObject.transform.localRotation = rot * rot_ini;

            gameObject.transform.localPosition = new Vector3(0, 300, 0);
        }
    }

}