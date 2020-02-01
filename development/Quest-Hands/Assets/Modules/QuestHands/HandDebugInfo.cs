﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HandDebugInfo : MonoBehaviour
{
    public Text fingerBend;
    OVRSkeleton skeleton;

    // Start is called before the first frame update
    void Start()
    {
        skeleton = GetComponent<AttachToFinger>().skeleton;
        tipTransforms.Clear();

        foreach(var t in tips) {
            var b = skeleton.Bones.FirstOrDefault(x => x.Id == t);
            if(b != null)
                tipTransforms.Add(b);
        }


    }

    OVRSkeleton.BoneId[] tips = new OVRSkeleton.BoneId[] {
        OVRSkeleton.BoneId.Hand_Thumb2,
        OVRSkeleton.BoneId.Hand_Index2,
        OVRSkeleton.BoneId.Hand_Middle2,
        OVRSkeleton.BoneId.Hand_Ring2,
        OVRSkeleton.BoneId.Hand_Pinky2
    };

    List<OVRBone> tipTransforms;

    public float averageBendiness = 0f;

    // Update is called once per frame
    void Update()
    {
        var str = "";
        averageBendiness = 0f;
        foreach(var b in tipTransforms) {
            str += b.Id.ToString() + ": " + b.Transform.localEulerAngles + "\n";
            averageBendiness += b.Transform.localEulerAngles.z;
        }

        averageBendiness /= tipTransforms.Count;
        str += "Avg: " + averageBendiness;

        fingerBend.text = str;
    }
}
