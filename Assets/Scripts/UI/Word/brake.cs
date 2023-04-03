using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

namespace UI
{
    public class brake : MonoBehaviour
    {
        public Vehicle.Master master;

        public GameObject brake_object = null;

        void Start()
        {
        }

        void FixedUpdate()
        {
            if (!master.ConstantVelocity)
            {
                Text accel_text = brake_object.GetComponent<Text>();
                accel_text.text = "brake";
            }
        }
    }
}
