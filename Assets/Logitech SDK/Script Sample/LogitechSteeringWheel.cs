using UnityEngine;
using System.Collections;
using System.Text;

public class LogitechSteeringWheel : MonoBehaviour
{

    LogitechGSDK.LogiControllerPropertiesData properties;
    private string actualState;
    private string activeForces;
    private string propertiesEdit;
    private string buttonStatus;
    private string forcesLabel;
    string[] activeForceAndEffect;

    // Use this for initialization
    void Start()
    {
        activeForces = "";
        propertiesEdit = "";
        actualState = "";
        buttonStatus = "";
        forcesLabel = "Press the following keys to activate forces and effects on the steering wheel / gaming controller \n";
        forcesLabel += "Spring force : S\n";
        forcesLabel += "Constant force : C\n";
        forcesLabel += "Damper force : D\n";
        forcesLabel += "Side collision : Left or Right Arrow\n";
        forcesLabel += "Front collision : Up arrow\n";
        forcesLabel += "Dirt road effect : I\n";
        forcesLabel += "Bumpy road effect : B\n";
        forcesLabel += "Slippery road effect : L\n";
        forcesLabel += "Surface effect : U\n";
        forcesLabel += "Car Airborne effect : A\n";
        forcesLabel += "Soft Stop Force : O\n";
        forcesLabel += "Set example controller properties : PageUp\n";
        forcesLabel += "Play Leds : P\n";
        activeForceAndEffect = new string[9];
        Debug.Log("SteeringInit:" + LogitechGSDK.LogiSteeringInitialize(false));
    }

    void OnApplicationQuit()
    {
        Debug.Log("SteeringShutdown:" + LogitechGSDK.LogiSteeringShutdown());
    }

    // Update is called once per frame
    void Update()
    {

    }



}
