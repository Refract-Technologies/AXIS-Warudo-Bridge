using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PairTransformToString
{
    public Transform bone_trans;
    public string vmc_bone_name;
    [NonSerialized]
    public Quaternion originalLocalRotation;
    [NonSerialized]
    public Quaternion originalWorldRotation;
    [NonSerialized]
    public Quaternion parent_originalWorldRotation;
    [NonSerialized]
    public Quaternion parent_originalLocalRotation;
    [NonSerialized]
    public Transform parentTrans;
   
    public void Init(bool inIsWorldRotation = false)
    {
        if (bone_trans.parent == null) parentTrans = bone_trans;
        else parentTrans = bone_trans.parent;
        
        if (inIsWorldRotation)
        {
            originalLocalRotation = bone_trans.rotation;
            parent_originalLocalRotation = parentTrans.rotation;
        }
        else
        {
            originalLocalRotation = bone_trans.localRotation;
            parent_originalLocalRotation = parentTrans.localRotation;
        }
        parent_originalWorldRotation = parentTrans.rotation;
        originalWorldRotation = bone_trans.rotation;
    }
}

