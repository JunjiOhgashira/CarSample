using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

namespace Car
{
    namespace UI
    {
        public class accel : MonoBehaviour
        {
            public GameManager gm;
            public GameObject accel_object = null;

            void Start()
            {
            }

            void FixedUpdate()
            {
                if (!gm.ConstantVelocity)
                {
                    Text accel_text = accel_object.GetComponent<Text>();
                    accel_text.text = "accel";
                }
            }
        }
    }
}