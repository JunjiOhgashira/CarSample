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
        public class AccelGauge : MonoBehaviour
        {
            public GameManager gm;

            void FixedUpdate()
            {
                if (!gm.ConstantVelocity)
                {
                    Image gaugeCtrl = GetComponent<Image>();
                    gaugeCtrl.fillAmount = (float)gm.accel;
                }
            }
        }

    }
}