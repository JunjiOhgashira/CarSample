using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;

namespace Car
{
    namespace Tool
    {
        public class Logger : MonoBehaviour
        {
            FileInfo fi;
            private StreamWriter sw;

            public GameManager gm;
            public Parameter parameter;
            public ModeChange modeChange;
            public Vehicle.Input input;
            public Vehicle.WaveNormal waveNormal;
            public Vehicle.WaveAdjust waveAdjust;
            public Vehicle.WaveAdjustSecond waveAdjustSecond;

            public string delay_text;
            public string method_text;
            public string velocity_text;

            public int num;

            [HideInInspector]
            public string[] DataToWrite;

            public void WriteIn()
            {
                string str = string.Join(",", DataToWrite);
                sw.WriteLine(str);
            }

            public void UpdateValue()
            {
                DataToWrite[0] = Time.time.ToString();
                DataToWrite[1] = (parameter.delay * 2).ToString();
                DataToWrite[2] = (gm.deltam * parameter.gearRatio).ToString();
                DataToWrite[3] = (gm.deltas * parameter.gearRatio).ToString();
                DataToWrite[4] = gm.omegam.ToString();
                DataToWrite[5] = gm.omegas.ToString();
                DataToWrite[6] = gm.thetam.ToString();
                DataToWrite[7] = gm.thetas.ToString();
                DataToWrite[8] = gm.pxs.ToString();
                DataToWrite[9] = gm.pys.ToString();
                DataToWrite[10] = gm.um.ToString();
                DataToWrite[11] = gm.us.ToString();
                DataToWrite[12] = gm.vm.ToString();
                DataToWrite[13] = gm.vs.ToString();
                DataToWrite[14] = gm.energy.ToString();
                DataToWrite[15] = gm.CI.ToString();
                DataToWrite[16] = gm.vels.ToString();
                DataToWrite[17] = waveNormal.vm.ToString();
                DataToWrite[18] = waveAdjustSecond.delta_vm.ToString();
            }

            void Start()
            {
                DataToWrite = new string[19]
                {
                    "time",
                    "RTT",
                    "deltam",
                    "deltas",
                    "omegam",
                    "omegas",
                    "thetam",
                    "thetas",
                    "pxs",
                    "pys",
                    "um",
                    "us",
                    "vm",
                    "vs",
                    "energy",
                    "b",
                    "velocity",
                    "hat_vm",
                    "delta_vm"
                };

                delay_text = "RTT" + (parameter.delay * 2).ToString() + "ms";
                method_text = (modeChange.WaveVariableTransformation ? "apply" : "none");
                velocity_text = (modeChange.ConstantVelocity ? parameter.V.ToString() + "km" : "changeable");

                DateTime now = DateTime.Now;
                string suffix = now.Year.ToString() + "_" +
                                now.Month.ToString() + "_" +
                                now.Day.ToString() + "_" +
                                now.Hour.ToString() + "_" +
                                now.Minute.ToString() + "_" +
                                now.Second.ToString() + "_" +
                                delay_text + "_" +
                                method_text + "_" +
                                velocity_text;

                fi = new FileInfo(Application.dataPath + "/data/" + suffix + ".csv");
                sw = fi.AppendText();
            }

            void FixedUpdate()
            {
                WriteIn();
                UpdateValue();
            }

            void OnApplicationQuit()
            {
                sw.Close();
            }
        }
    }
}