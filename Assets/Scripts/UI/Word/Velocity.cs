using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

namespace UI
{
    public class Velocity : MonoBehaviour
    {
        public ModeChange modeChange;
        public UnitAdjuster unitAdjuster;

        public Vehicle.Master master;

        public double V;

        public GameObject velocity_object = null;

        void Update()
        {
            if (modeChange.ConstantVelocity)
            {
                V = unitAdjuster.V_km_h;
            }
            else
            {
                V = master.Vm;
            }

            Text velocity_text = velocity_object.GetComponent<Text>();

            velocity_text.text = Math.Round((float)V, 0, MidpointRounding.AwayFromZero).ToString() + "km/h";
        }
    }
}