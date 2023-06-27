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

            private double previous_omegam = 0.0;
            private double current_omegam = 0.0;
            private double difference;
            private double threshold = 0.1;
            private double gain = 0.1;

            void FixedUpdate()
            {
                current_omegam = gm.vm;
                difference = Mathf.Abs((float)(current_omegam - previous_omegam));

                if (difference > threshold)
                {
                    Debug.Log("a");
                    //current_omegam *= 0.0;
                }
                else
                {
                    Debug.Log("b");
                }

                previous_omegam = current_omegam;
                gm.vm = current_omegam;
            }
        }
    }
}