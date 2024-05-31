using Axis.DataTypes;
using Axis.Elements;
using Axis.Events;
using OscCore;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// This mono behaviour is used to send OSC mannequin using the AxisMannequin provided by AXIS SDK, 
/// This is tied to the puppetted mannequin and it will connect to the control bone
/// The rig is from mixamorig
/// </summary>
public class Axis_OSC_Mannequin : MonoBehaviour
{
    private bool m_HasSender;
  

    public string ip;
    public int port;

    OscClient oscClient;

    public AxisBrain axisBrain;
    List<PairTransformToString> UnityBoneTransformToVMCBoneName = new List<PairTransformToString>();
    public List<int> ActiveBonePairs = new List<int>();
    public List<PairTransformToString> AllPossibleBonesPairs;
    Axis.Native.AxisRuntimeNative aNative;
    public GameObject avatarModel;
    // SEND AS ZERO'ed rotations as required by VMC
    public bool ZeroRotationsForVMC = true;
    // SEND AS TRANSFORMATIONS THAT ARE NOT AFFECTED BY THE PARENTS AS PER WARUDO
    public bool LocalizeRotations = true;
    public void Start()
    {
        aNative = new Axis.Native.AxisRuntimeNative();
        aNative.Start();
        UnityBoneTransformToVMCBoneName.Clear();
        foreach (int indexToArray in ActiveBonePairs)
        {
            UnityBoneTransformToVMCBoneName.Add(AllPossibleBonesPairs[indexToArray]);
        }
        foreach (PairTransformToString ptr in AllPossibleBonesPairs)
        {
            ptr.Init();
        }
    }
    public void OnDestroy()
    {
        aNative.Stop();
    }
    public void SetPTRsForBones(List<PairTransformToString> inBones)
    {
        UnityBoneTransformToVMCBoneName = inBones;
        ZeroApplication();
    }
    void HandleZeroRotations()
    {
        for (int i = 0; i < UnityBoneTransformToVMCBoneName.Count; i++)
        {
            PairTransformToString ptrs = UnityBoneTransformToVMCBoneName[i];
            UnityBoneTransformToVMCBoneName[i].Init();
        }
    }
    void ZeroApplication()
    {
        if (oscClient == null) return;
        for (int i = 0; i < AllPossibleBonesPairs.Count; i++)
        {
            PairTransformToString pTS = AllPossibleBonesPairs[i];
            oscClient.SendPosQuart("/VMC/Ext/Bone/Pos", pTS.vmc_bone_name, Vector3.zero, Quaternion.identity);
        }
        
    }
    void OnEnable()
    {
        HandleZeroRotations();
        m_HasSender = oscClient != null;
    }
    public void CalibrateAXISMannequin()
    {
        AxisEvents.OnSinglePoseCalibration?.Invoke();
    }
    public void ZeroAxisMannequin()
    {
        AxisEvents.OnZeroAll?.Invoke();
    }
    public void ConnectToServer()
    {
        {
            oscClient = null;
            oscClient = new OscClient(ip , port);
        }
        m_HasSender = oscClient != null;
        ZeroApplication();
        //HandleZeroRotations();
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (!m_HasSender) return;

        for (int i = 0; i < UnityBoneTransformToVMCBoneName.Count; i++)
        {
            PairTransformToString pTS = UnityBoneTransformToVMCBoneName[i];
            Quaternion normalizedLocalRot = pTS.originalWorldRotation * Quaternion.Inverse(pTS.originalLocalRotation) * pTS.bone_trans.localRotation * Quaternion.Inverse(pTS.originalWorldRotation);

            //https://docs.unity3d.com/ScriptReference/HumanBodyBones.html
            oscClient.SendPosQuart("/VMC/Ext/Bone/Pos", pTS.vmc_bone_name, Vector3.zero, normalizedLocalRot);
        }
    }
    
}
