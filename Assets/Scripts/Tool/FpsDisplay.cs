using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Car
{
    namespace Tool
    {
        public class FpsDisplay : MonoBehaviour
        {

            private float fps;

            private void FixedUpdate()
            {
                fps = 1f / Time.deltaTime;
                //Debug.Log(fps);
            }

        }
    }
}