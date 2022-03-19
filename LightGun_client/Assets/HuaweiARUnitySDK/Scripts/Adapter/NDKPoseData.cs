/*
 * 2019.09.19 - Huawei modifies and matches AREngine.
 * Copyright (C) 2019. Huawei Technologies Co., Ltd. All rights reserved.
 */

//-----------------------------------------------------------------------
// <copyright file="ApiPoseData.cs" company="Google">
//
// Copyright 2017 Google LLC. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

namespace HuaweiARInternal
{
    using System.Runtime.InteropServices;
    using UnityEngine;

    [StructLayout(LayoutKind.Sequential)]
    public struct NDKPoseData
    {
        [MarshalAs(UnmanagedType.R4)]
        public float Qx;

        [MarshalAs(UnmanagedType.R4)]
        public float Qy;

        [MarshalAs(UnmanagedType.R4)]
        public float Qz;

        [MarshalAs(UnmanagedType.R4)]
        public float Qw;

        [MarshalAs(UnmanagedType.R4)]
        public float Tx;

        [MarshalAs(UnmanagedType.R4)]
        public float Ty;

        [MarshalAs(UnmanagedType.R4)]
        public float Tz;

        public NDKPoseData(Pose unityPose)
        {
            UnityPose2NDKPoseData(unityPose, out this);
        }

        public Pose ToUnityPose()
        {
            Pose unityPose;
            NDKPoseData2UnityPose(this,out unityPose);
            return unityPose;
        }

        public override string ToString()
        {
            return string.Format("tx:{0}, ty:{1}, tz{2}, qw{3}, qx{4}, qy{5}, qz{6}",
                Tx,Ty,Tz,Qw,Qx,Qy,Qz);
        }

        public static void NDKPoseData2UnityPose(NDKPoseData poseData, out Pose pose)
        {
            Matrix4x4 glWorld2glLocal = Matrix4x4.TRS(new Vector3(poseData.Tx, poseData.Ty, poseData.Tz),
                new Quaternion(poseData.Qx, poseData.Qy, poseData.Qz, poseData.Qw), Vector3.one);
            Matrix4x4 unityWorld2glWorld = Matrix4x4.Scale(new Vector3(1, 1, -1));
            Matrix4x4 unityWorld2unityLocal = unityWorld2glWorld * glWorld2glLocal * unityWorld2glWorld.inverse;

            Vector3 position = unityWorld2unityLocal.GetColumn(3);
            Quaternion quaternion = Quaternion.LookRotation(unityWorld2unityLocal.GetColumn(2),
                unityWorld2unityLocal.GetColumn(1));

            pose =  new Pose(position, quaternion);
        }

        public static void UnityPose2NDKPoseData(Pose pose, out NDKPoseData poseData)
        {
            Matrix4x4 unityWorld2unityLocal = Matrix4x4.TRS(pose.position,pose.rotation,Vector3.one);
            Matrix4x4 glWorld2unityWorld = Matrix4x4.Scale(new Vector3(1,1,-1));
            Matrix4x4 glWorld2glLocal = glWorld2unityWorld*unityWorld2unityLocal*glWorld2unityWorld.inverse;
            Vector3 position = glWorld2glLocal.GetColumn(3);
            Quaternion quaternion = Quaternion.LookRotation(glWorld2glLocal.GetColumn(2),
                glWorld2glLocal.GetColumn(1));
            poseData.Tx = position.x;
            poseData.Ty = position.y;
            poseData.Tz = position.z;

            poseData.Qw = quaternion.w;
            poseData.Qx = quaternion.x;
            poseData.Qy = quaternion.y;
            poseData.Qz = quaternion.z;
        }
    }
}
