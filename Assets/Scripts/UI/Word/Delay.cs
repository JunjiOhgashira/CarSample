using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using Vehicle;

namespace UI
{
    public class Delay : MonoBehaviour
    {
        public Parameter parameter;

        public GameObject delay_object = null;

        void Update()
        {
            Text delay_text = delay_object.GetComponent<Text>();
            delay_text.text = "RTT:" + (parameter.delay * 2).ToString() + "ms";
        }
    }
}