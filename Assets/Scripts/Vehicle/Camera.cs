using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Car
{
    namespace Vehicle
    {
        public class Camera : MonoBehaviour
        {
            public GameManager gm;

            void FixedUpdate()
            {
                var offset = gm.LookDown ? new Vector3(0f, 10f, 0f) : new Vector3(0f, (float)(gm.parameter.height / 2) + 0.2f, 0f);
                gameObject.transform.position = gm.masterPosition + offset;
                transform.eulerAngles = gm.LookDown ? new Vector3(90, (float)gm.thetam, 0) : new Vector3(10, (float)gm.thetam, 0);
            }
        }
    }
}