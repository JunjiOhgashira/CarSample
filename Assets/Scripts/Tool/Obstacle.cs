using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Car
{
    namespace Tool
    {
        public class Obstacle : MonoBehaviour
        {
            public Parameter parameter;
            public float Size;
            void Start()
            {
                Size = 0.5f;
                transform.localScale = new Vector3(Size, Size, Size);
            }
        }
    }
}