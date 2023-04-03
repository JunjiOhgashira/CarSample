using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using UnityEngine.InputSystem;

public class ScreenShotCapturer : MonoBehaviour
{
    private void Update()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            DateTime dateTime = DateTime.Now;
            CaptureScreenShot(Application.dataPath + @"\ScreenShots\"
                            + dateTime.Year.ToString() + "_" 
                            + dateTime.Month.ToString() + "_" 
                            + dateTime.Day.ToString() + "_" 
                            + dateTime.Hour.ToString() + "_" 
                            + dateTime.Minute.ToString() + "_" 
                            + dateTime.Second.ToString() + ".png");
        }
    }

    private void CaptureScreenShot(string filePath)
    {
        ScreenCapture.CaptureScreenshot(filePath);
    }
}