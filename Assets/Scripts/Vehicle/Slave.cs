using UnityEngine;

namespace Car
{
    namespace Vehicle
    {
        public class Slave : MonoBehaviour
        {
            public GameManager gm;

            void FixedUpdate()
            {
                transform.eulerAngles = gm.slaveAzimuth;
                transform.position = gm.slavePosition;
            }
        }
    }
}