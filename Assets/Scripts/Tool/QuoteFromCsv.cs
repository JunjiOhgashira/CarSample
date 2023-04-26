using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Car
{
    namespace Tool
    {
        public class QuoteFromCsv : MonoBehaviour
        {
            public GameManager gm;

            string FileName;
            TextAsset csvFile;
            List<string[]> csvDatas = new List<string[]>();

            [HideInInspector]
            public int index;
            [HideInInspector]
            public int timeIndex;
            [HideInInspector]
            public int deltamIndex;
            [HideInInspector]
            public double HandleControllerAngle;
            [HideInInspector]
            public int velocityIndex;
            [HideInInspector]
            public float startTime;
            [HideInInspector]
            public float finishTime;

            void Start()
            {
                FileName = gm.csvParam.quote;
                csvFile = Resources.Load(@"ExperimentData\" + FileName) as TextAsset;
                StringReader reader = new StringReader(csvFile.text);

                while (reader.Peek() != -1)
                {
                    string line = reader.ReadLine();
                    csvDatas.Add(line.Split(','));
                }

                index = 1;
                timeIndex = 0;
                deltamIndex = 12;
                velocityIndex = 4;
                startTime = 55;
                finishTime = 90;

                //startTime = 0;
                //finishTime = float.Parse(csvDatas[csvDatas.Count - 1][timeIndex]);

                gm.vels = double.Parse(csvDatas[index][velocityIndex]);
            }

            void FixedUpdate()
            {
                while (float.Parse(csvDatas[index][timeIndex]) < Time.time + startTime)
                {
                    HandleControllerAngle = double.Parse(csvDatas[index][deltamIndex]);
                    gm.vels = double.Parse(csvDatas[index][velocityIndex]);
                    index += (float.Parse(csvDatas[index][timeIndex]) < Time.time + startTime) ? 1 : 0;
                }

                if (Time.time > finishTime - startTime && gm.ExperimentData)
                {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();//ゲームプレイ終了
#endif
                }
            }
        }
    }
}