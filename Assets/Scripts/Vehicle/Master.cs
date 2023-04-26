using UnityEngine;

namespace Car
{
    namespace Vehicle
    {
        public class Master : MonoBehaviour
        {
            public GameManager gm;

            void FixedUpdate()
            {
                transform.eulerAngles = gm.masterAzimuth;
                transform.position = gm.masterPosition;
            }
        }
    }
}