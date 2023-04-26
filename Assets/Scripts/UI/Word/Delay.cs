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
        public class Delay : MonoBehaviour
        {
            public GameManager gm;
            public GameObject delay_object = null;

            void FixedUpdate()
            {
                Text delay_text = delay_object.GetComponent<Text>();
                delay_text.text = "RTT:" + (gm.delay * 2).ToString() + "ms";
            }
        }
    }
}