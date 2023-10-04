using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Car
{
    namespace Vehicle
    {
        public class LPF : MonoBehaviour
        {
            public GameManager gm;

            double T = 0.2;

            public double Filter(double pre_input, double cur_input, double pre_output)
            {
                //double cur_output = (2 * T - gm.dt) / (2 * T + gm.dt) * pre_output + gm.dt / (2 * T + gm.dt) * (cur_input + pre_input);

                double cur_output = pre_output + (-pre_output / T + pre_input / T) * gm.dt;
                return cur_output;
            }
        }
    }
}