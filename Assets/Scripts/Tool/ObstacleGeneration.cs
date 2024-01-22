using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Car
{
    namespace Tool
    {
        public class ObstacleGeneration : MonoBehaviour
        {
            public GameManager gm;

            [SerializeField]
            private GameObject createPrefab;
            public List<Vector3> obstacles = new List<Vector3>();

            private float elapsedTime;
            private float preElapsedTime;
            public Vector3 obstaclePosition;

            void Start()
            {

            }

            void Update()
            {
                elapsedTime = Time.time - (int)Time.time;
                if(elapsedTime < preElapsedTime)
                {
                    if(Random.Range(0.0f, 1.0f) < 0.4)
                    {
                        Create();
                    }
                }
                preElapsedTime = elapsedTime;
            }

            void Create()
            {
                obstaclePosition = new Vector3(Random.Range(-2.0f, 2.0f), 0.5f, (float)gm.pxs + 10);
                var ob = Instantiate(createPrefab, obstaclePosition, createPrefab.transform.rotation);
                var pos = ob.transform.position;
                obstacles.Add(pos);
            }
        }
    }
}