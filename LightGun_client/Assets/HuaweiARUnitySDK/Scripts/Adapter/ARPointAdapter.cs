/*
 * 2019.09.19 - Huawei modifies and matches AREngine.
 * Copyright (C) 2019. Huawei Technologies Co., Ltd. All rights reserved.
 */

//-----------------------------------------------------------------------
// <copyright file="PointApi.cs" company="Google">
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
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using HuaweiARUnitySDK;

    class ARPointAdapter
    {
        private NDKSession m_ndkSession;

        public ARPointAdapter(NDKSession session)
        {
            m_ndkSession = session;
        }

        public Pose GetPose(IntPtr pointHandle)
        {
            IntPtr poseHandle = m_ndkSession.PoseAdapter.Create();
            NDKAPI.HwArPoint_getPose(m_ndkSession.SessionHandle, pointHandle, poseHandle);
            Pose pose = m_ndkSession.PoseAdapter.GetPoseValue(poseHandle);
            m_ndkSession.PoseAdapter.Destroy(poseHandle);
            return pose;
        }

        public ARPoint.OrientationMode GetOrientationMode(IntPtr pointHandle)
        {
            int mode = 0;
            NDKAPI.HwArPoint_getOrientationMode(m_ndkSession.SessionHandle, pointHandle, ref mode);
            return (ARPoint.OrientationMode) mode;
        }

        private struct NDKAPI
        {
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArPoint_getPose(IntPtr sessionHandle,IntPtr pointHandle,
                       IntPtr outPoseHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArPoint_getOrientationMode(IntPtr sessionHandle, IntPtr pointHandle,
                                  ref int out_orientation_mode);
        }
    }
}
