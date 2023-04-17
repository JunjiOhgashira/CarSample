using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using Vehicle;

namespace Log
{
    public class Log : MonoBehaviour
    {
        FileInfo fi;
        private StreamWriter sw;

        public Parameter parameter;
        public ModeChange modeChange;
        public GetInput getInput;

        public Master master;
        public Slave slave;

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
            DataToWrite[0]  = Time.time.ToString();             // 0
            DataToWrite[1]  = (parameter.delay * 2).ToString(); // 1
            DataToWrite[2] = (master.deltam * parameter.ratio).ToString();          // 2
            DataToWrite[3] = (slave.deltas * parameter.ratio).ToString();           // 3
            DataToWrite[4] = master.omegam.ToString();          // 4
            DataToWrite[5] = slave.omegas.ToString();           // 5
            DataToWrite[6] = master.thetam.ToString();          // 6
            DataToWrite[7] = slave.thetas.ToString();           // 7
            DataToWrite[8] = slave.pxs.ToString();              // 8
            DataToWrite[9] = slave.pys.ToString();              // 9
            DataToWrite[10] = master.um.ToString();             // 10
            DataToWrite[11] = slave.us.ToString();              // 11
            DataToWrite[12] = master.vm.ToString();             // 12
            DataToWrite[13] = slave.vs.ToString();              // 13
            DataToWrite[14] = master.energy.ToString();         // 14
            DataToWrite[15] = master.b.ToString();              // 15
            DataToWrite[16] = slave.Vs.ToString();              // 16
            DataToWrite[17] = master.thetaError.ToString();              // 17
        }

        void Start()
        {
            DataToWrite = new string[18]
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
                "thetaError"
            };

            delay_text = "RTT" + (parameter.delay * 2).ToString() + "ms";
            method_text = (modeChange.ProposedMethod ? "apply" : "none");
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

        void Update()
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