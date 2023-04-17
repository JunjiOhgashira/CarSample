using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class QuoteFromCsv : MonoBehaviour
{
    public ModeChange modeChange;
    public CsvParam csvParam;

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
    public double Velocity; // t-Tにおける速度
    [HideInInspector]
    public double LowerLimitVelocity;
    [HideInInspector]
    public float startTime;
    [HideInInspector]
    public float finishTime;

    void Start()
    {
        FileName = csvParam.quote;
        csvFile = Resources.Load(@"ExperimentData\" + FileName) as TextAsset;
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
        }

        index           = 1;
        timeIndex       = 0;
        deltamIndex     = 12;
        velocityIndex   = 4;
        startTime = 55;
        finishTime = 90;

        //startTime = 0;
        //finishTime = float.Parse(csvDatas[csvDatas.Count - 1][timeIndex]);

        LowerLimitVelocity = 1.0 * 1000.0 / 3600.0;
        //Velocity = double.Parse(csvDatas[index][velocityIndex]) > LowerLimitVelocity ? double.Parse(csvDatas[index][velocityIndex]) : LowerLimitVelocity;
        Velocity = double.Parse(csvDatas[index][velocityIndex]);
    }

    void Update()
    {
        while (float.Parse(csvDatas[index][timeIndex]) < Time.time + startTime)
        {
            HandleControllerAngle = double.Parse(csvDatas[index][deltamIndex]);
            //Velocity              = double.Parse(csvDatas[index][velocityIndex]) > LowerLimitVelocity ? double.Parse(csvDatas[index][velocityIndex]) : LowerLimitVelocity;
            Velocity = double.Parse(csvDatas[index][velocityIndex]);
            index += (float.Parse(csvDatas[index][timeIndex]) < Time.time + startTime) ? 1 : 0;
        }

        if (Time.time > finishTime - startTime && (modeChange.ExperimentDataSteer || modeChange.ExperimentDataVelocity))
        {
            Debug.Log(finishTime);
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();//ゲームプレイ終了
#endif
        }
    }
}