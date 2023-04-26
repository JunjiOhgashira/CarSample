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
        public class Velocity : MonoBehaviour
        {
            public GameManager gm;

            public GameObject velocity_object = null;

            void FixedUpdate()
            {
                Text velocity_text = velocity_object.GetComponent<Text>();

                velocity_text.text = Math.Round((float)gm.velm, 0, MidpointRounding.AwayFromZero).ToString() + "km/h";
            }
        }
    }
}