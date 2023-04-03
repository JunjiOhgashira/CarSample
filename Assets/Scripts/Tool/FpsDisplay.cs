using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsDisplay : MonoBehaviour
{

    private float fps;

    private void Update()
    {
        fps = 1f / Time.deltaTime;
        //Debug.Log(fps);
    }

}