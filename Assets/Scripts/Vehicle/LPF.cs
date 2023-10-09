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

            public double Filter(double pre_input, double cur_input, double pre_output)
            {
                double cur_output = pre_output + (-pre_output / gm.FilterTimeConstant + pre_input / gm.FilterTimeConstant) * gm.dt;
                return cur_output;
            }
        }
    }
}