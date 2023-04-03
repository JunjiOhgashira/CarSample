using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

namespace UI
{
    public class VelocityGauge : MonoBehaviour
    {
        public Vehicle.Master master;

        void Update()
        {
            if (!master.ConstantVelocity)
            {
                Image gaugeCtrl = GetComponent<Image>();
                gaugeCtrl.fillAmount = (float)master.Vm / (100f * (1000f / 3600f));
            }
        }
    }

}