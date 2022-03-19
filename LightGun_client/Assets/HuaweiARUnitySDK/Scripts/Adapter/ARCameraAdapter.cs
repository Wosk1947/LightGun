/*
 * 2019.09.19 - Huawei modifies and matches AREngine.
 * Copyright (C) 2019. Huawei Technologies Co., Ltd. All rights reserved.
 */

//-----------------------------------------------------------------------
// <copyright file="CameraApi.cs" company="Google">
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
    using UnityEngine;
    using System.Runtime.InteropServices;
    using HuaweiARUnitySDK;

    internal class ARCameraAdapter
    {
        private NDKSession m_ndkSession;

        public ARCameraAdapter(NDKSession session)
        {
            m_ndkSession = session;
        }

        public ARTrackable.TrackingState GetTrackingState(IntPtr cameraHandle)
        {
            int state = (int)ARTrackable.TrackingState.STOPPED;
            NDKAPI.HwArCamera_getTrackingState(m_ndkSession.SessionHandle, cameraHandle, ref state);
            if (!ValueLegalityChecker.CheckInt("GetTrackingState", state,
                AdapterConstants.Enum_TrackingState_MinIntValue, AdapterConstants.Enum_TrackingState_MaxIntValue))
            {
                return ARTrackable.TrackingState.STOPPED;
            }
            return (ARTrackable.TrackingState)state;
        }

        public Pose GetPose(IntPtr cameraHandle)
        {
            if (cameraHandle == IntPtr.Zero)
            {
                return Pose.identity;
            }

            IntPtr poseHandle = m_ndkSession.PoseAdapter.Create();
            NDKAPI.HwArCamera_getDisplayOrientedPose(m_ndkSession.SessionHandle, cameraHandle,
                poseHandle);
            Pose resultPose = m_ndkSession.PoseAdapter.GetPoseValue(poseHandle);
            m_ndkSession.PoseAdapter.Destroy(poseHandle);
            return resultPose;
        }

        public Matrix4x4 GetProjectionMatrix(IntPtr cameraHandle, float near, float far)
        {
            Matrix4x4 matrix = Matrix4x4.identity;
            NDKAPI.HwArCamera_getProjectionMatrix(m_ndkSession.SessionHandle, cameraHandle,
                near, far, ref matrix);
            return matrix;
        }

        public void Release(IntPtr cameraHandle)
        {
            NDKAPI.HwArCamera_release(cameraHandle);
        }

        public ARCameraIntrinsics GetImageIntrinsics(IntPtr cameraHandle)
        {
            ARDebug.LogInfo("ARCamera adapter get image intrinsics start");
            IntPtr arCameraIntrinsicsHandle = IntPtr.Zero;

            NDKAPI.HwArCameraIntrinsics_create(m_ndkSession.SessionHandle, ref arCameraIntrinsicsHandle);
            NDKAPI.HwArCamera_getImageIntrinsics(m_ndkSession.SessionHandle, cameraHandle, arCameraIntrinsicsHandle);
            ARCameraIntrinsics imageIntrinsics = GetARCameraIntrinsicsFromeHandle(arCameraIntrinsicsHandle);
            NDKAPI.HwArCameraIntrinsics_destroy(m_ndkSession.SessionHandle, arCameraIntrinsicsHandle);
            ARDebug.LogInfo("ARCamera adapter get image intrinsics end");
            return imageIntrinsics;
        }

        private ARCameraIntrinsics GetARCameraIntrinsicsFromeHandle(IntPtr intrinsicsHandle)
        {
            float focalX = 0;
            float focalY = 0;
            float principalX = 0;
            float principalY = 0;
            int imageWidth = 0;
            int imageHeight = 0;
            float[] outDistortion = new float[5];

            NDKAPI.HwArCameraIntrinsics_getFocalLength(m_ndkSession.SessionHandle,
                intrinsicsHandle, ref focalX, ref focalY);
            NDKAPI.HwArCameraIntrinsics_getPrincipalPoint(m_ndkSession.SessionHandle,
                intrinsicsHandle, ref principalX, ref principalY);
            NDKAPI.HwArCameraIntrinsics_getImageDimensions(m_ndkSession.SessionHandle,
                intrinsicsHandle, ref imageWidth, ref imageHeight);
            NDKAPI.HwArCameraIntrinsics_getDistortion(m_ndkSession.SessionHandle,
                intrinsicsHandle, outDistortion);

            return new ARCameraIntrinsics(new Vector2(focalX, focalY),
                                          new Vector2(principalX, principalY),
                                          new Vector2Int(imageWidth, imageHeight),
                                          outDistortion);
        }

        private struct NDKAPI
        {
            //this function is useless in unity
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArCamera_getPose(IntPtr sessionHandle, IntPtr cameraHandle, IntPtr outPoseHandle);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArCamera_getDisplayOrientedPose(IntPtr sessionHandle, IntPtr cameraHandle,
                                       IntPtr outPoseHandle);
            //this function is unused in unity
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArCamera_getViewMatrix(IntPtr sessionHandle, IntPtr cameraHandle,
                              ref Matrix4x4 outColMajor_4x4);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArCamera_getTrackingState(IntPtr sessionHandle, IntPtr cameraHandle,
                                 ref int outTrackingState);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArCamera_getProjectionMatrix(IntPtr sessionHandle, IntPtr cameraHandle,
                                    float near, float far, ref Matrix4x4 outColMajor_4x4);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArCamera_release(IntPtr cameraHandle);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArCameraIntrinsics_create(IntPtr sessionHandle, ref IntPtr outCameraIntrinsicsHandle);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArCameraIntrinsics_destroy(IntPtr sessionHandle, IntPtr CameraIntrinsicsHandle);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArCameraIntrinsics_getFocalLength(IntPtr sessionHandle, IntPtr CameraIntrinsicsHandle, ref float outFocalX, ref float outFocalY);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArCameraIntrinsics_getPrincipalPoint(IntPtr sessionHandle, IntPtr CameraIntrinsicsHandle, ref float outPrincipalX, ref float outPrincipalY);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArCameraIntrinsics_getImageDimensions(IntPtr sessionHandle, IntPtr CameraIntrinsicsHandle, ref Int32 outWidth, ref Int32 outHeight);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArCameraIntrinsics_getDistortion(IntPtr sessionHandle, IntPtr CameraIntrinsicsHandle, float[] outDistortion);

            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArCamera_getImageIntrinsics(IntPtr sessionHandle, IntPtr cameraHandle, IntPtr CameraIntrinsicsHandle);
        }
    }
}
