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
            public Vector3 obstaclePosition;

            void Start()
            {

            }

            void Update()
            {
                elapsedTime += Time.deltaTime;
                if(elapsedTime > 3)
                {
                    Create();
                    elapsedTime = 0;
                }
            }

            void Create()
            {
                obstaclePosition = new Vector3(Random.Range(-1.5f, 1.5f), 0.25f, (float)gm.pxs + 7);
                var ob = Instantiate(createPrefab, obstaclePosition, createPrefab.transform.rotation);
                var pos = ob.transform.position;
                obstacles.Add(pos);
            }
        }
    }
}