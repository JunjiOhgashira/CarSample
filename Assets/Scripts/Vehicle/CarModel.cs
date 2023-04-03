using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vehicle
{
    public class CarModel : MonoBehaviour
    {
        public Master master;
        public Slave slave;

        public float height;
        public float difference;
        public float theta;
        public float x;
        public float y;
        public float z;

        void Start()
        {
            height = 1.6f;
            difference = 0;
            //difference = 1.4f;
            y = height / 2f;
        }

        void Update()
        {
            theta = (float)slave.thetasPast;
            x = (float)slave.pysPast - difference * Mathf.Sin(theta * Mathf.PI / 180);
            z = (float)slave.pxsPast - difference * Mathf.Cos(theta * Mathf.PI / 180);
            transform.eulerAngles = new Vector3(0, theta - 90, 0);
            transform.position = new Vector3(x, y, z);
        }
    }
}