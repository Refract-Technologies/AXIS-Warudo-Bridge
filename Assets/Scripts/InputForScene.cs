using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputForScene : MonoBehaviour
{
    public TMPro.TMP_InputField fieldIP;
    public TMPro.TMP_InputField fieldPort;
    public Axis_OSC_Mannequin axisMannequinRef;
    public GameObject popUpObject;

    public Toggle nodeToggle;
    public Toggle hubToggle;
    public Toggle chest_EstToggle;
    public BoneConfigurator_UI bCUI;
    public CountdownTimer cTimer;

    public ChestEstimatorFromHipAndArms cEstim;
    public void OpenSettings()
    {
        bCUI.ToggleFromActiveBonePairs();
    }
    public void ToggleChestEstimate()
    {
        cEstim.estimateRunning = chest_EstToggle.isOn;
    }
    public void CallCalibrate()
    {
        cTimer.StartCountDown();
        cTimer.TimerDone += () =>
        {
            axisMannequinRef.CalibrateAXISMannequin();
            cTimer.ConfirmFinished();
        };
       
    }
    public void CallZero()
    {
        cTimer.StartCountDown();
        cTimer.TimerDone += () =>
        {
            axisMannequinRef.ZeroAxisMannequin();
            cTimer.ConfirmFinished();
        };
    }
    public void Start()
    {
        fieldIP.text = axisMannequinRef.ip;
        fieldPort.text = axisMannequinRef.port + "";
        ClosePopUp();

        nodeToggle.isOn = axisMannequinRef.axisBrain.hipProvider == Axis.Enumerations.HipProvider.Node;
        hubToggle.isOn = axisMannequinRef.axisBrain.hipProvider == Axis.Enumerations.HipProvider.Hub;
        bCUI.InitalizeBoneConfigurations();
    }
    public void OnEnable()
    {
        nodeToggle.isOn = axisMannequinRef.axisBrain.hipProvider == Axis.Enumerations.HipProvider.Node;
        hubToggle.isOn = axisMannequinRef.axisBrain.hipProvider == Axis.Enumerations.HipProvider.Hub;
    }
    public void IPField_Finished()
    {
        axisMannequinRef.ip = fieldIP.text;
    }
    public void PortField_Finished()
    {
        axisMannequinRef.port = int.Parse(fieldPort.text);
    }
    public void OpenPopUp()
    {
        popUpObject.SetActive(true);
        OpenSettings();
    }
    public void ClosePopUp()
    {
        popUpObject.SetActive(false);
    }
    public void HipType_Toggle_Node()
    {
        axisMannequinRef.axisBrain.hipProvider = Axis.Enumerations.HipProvider.Node;
    }
    public void HipType_Toggle_Hub()
    {
        axisMannequinRef.axisBrain.hipProvider = Axis.Enumerations.HipProvider.Hub;
    }
}
