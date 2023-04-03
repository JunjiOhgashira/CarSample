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

        index       = 1;
        timeIndex   = 0;
        deltamIndex = 12;
        startTime = 320;
        finishTime = 370;

        //startTime   = 0;
        //finishTime  = float.Parse(csvDatas[csvDatas.Count-1][timeIndex]);
    }

    void Update()
    {
        while (float.Parse(csvDatas[index][timeIndex]) < Time.time + startTime)
        {
            HandleControllerAngle = double.Parse(csvDatas[index][deltamIndex]);
            index += (float.Parse(csvDatas[index][timeIndex]) < Time.time + startTime) ? 1 : 0;
        }

        if (Time.time > finishTime - startTime && modeChange.ExperimentData)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();//ゲームプレイ終了
#endif
        }
    }
}