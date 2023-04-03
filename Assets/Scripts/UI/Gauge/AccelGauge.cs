using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

namespace UI
{
    public class AccelGauge : MonoBehaviour
    {
        public Parameter parameter;
        public Vehicle.Master master;
        public Vehicle.GetInput getInput;

        void Update()
        {
            if (!master.ConstantVelocity)
            {
                Image gaugeCtrl = GetComponent<Image>();
                gaugeCtrl.fillAmount = (float)getInput.accel / (float)parameter.accelMax;
            }
        }
    }

}