using Axis.Elements;
using Axis.Elements.AnimatorLink;
using Axis.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Used to reset transformation in relation to the chest not tracking, it's a counter transformation to what was applied on the skeleton
/// </summary>
public class ChestEstimatorFromHipAndArms : MonoBehaviour
{
    Quaternion ori_Rotate;
    public Quaternion HackRotateTest;
    public AxisMannequin aMann;
    public GameObject leftShoulder;
    public GameObject rightShoulder;
    public GameObject neck;

    public bool estimateRunning = false;
    // Start is called before the first frame update
    void Awake()
    {
        ori_Rotate = this.transform.localRotation;
    }
    void OnEnable()
    {
        aMann.onBodyModelAnimatorLinkUpdated += HandlePostUpdate;
    }
    void OnDisable()
    {
        aMann.onBodyModelAnimatorLinkUpdated -= HandlePostUpdate;
    }
    bool init = false;
  
    // Update is called once per frame
    //void HandlePostUpdate(string inString, Dictionary<Axis.Enumerations.NodeBinding, AxisNode> bind)
    void HandlePostUpdate(BodyModelAnimatorLink bMlink)
    {
        if (!estimateRunning) return;
        Quaternion worldRotationLeft = leftShoulder.transform.rotation;
        Quaternion worldRotationRight = rightShoulder.transform.rotation;
        Quaternion worldRotationNeck = neck.transform.rotation;

        this.transform.localRotation = ori_Rotate;
        leftShoulder.transform.rotation = worldRotationLeft;
        rightShoulder.transform.rotation = worldRotationRight;
        neck.transform.rotation = worldRotationNeck;
       
    }
}
