/*
 * 2019.09.19 - Huawei modifies and matches AREngine.
 * Copyright (C) 2019. Huawei Technologies Co., Ltd. All rights reserved.
 */
//-----------------------------------------------------------------------
// <copyright file="AnchorApi.cs" company="Google">
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

    internal class ARAnchorAdapter
    {
        private NDKSession m_ndkSession;

        public ARAnchorAdapter(NDKSession session)
        {
            m_ndkSession = session;
        }

        //anchor
        public Pose GetPose(IntPtr anchorHandle)
        {
            IntPtr poseHandle = m_ndkSession.PoseAdapter.Create();
            NDKAPI.HwArAnchor_getPose(m_ndkSession.SessionHandle, anchorHandle, poseHandle);
            Pose pose = m_ndkSession.PoseAdapter.GetPoseValue(poseHandle);
            m_ndkSession.PoseAdapter.Destroy(poseHandle);
            return pose;
        }

        public ARTrackable.TrackingState GetTrackingState(IntPtr anchorHandle)
        {
            int trackingState = (int)ARTrackable.TrackingState.STOPPED;

            NDKAPI.HwArAnchor_getTrackingState(m_ndkSession.SessionHandle, anchorHandle,
                ref trackingState);
            if (!ValueLegalityChecker.CheckInt("GetTrackingState", trackingState,
                AdapterConstants.Enum_TrackingState_MinIntValue, AdapterConstants.Enum_TrackingState_MaxIntValue))
            {
                return ARTrackable.TrackingState.STOPPED;
            }
            return (ARTrackable.TrackingState)trackingState;
        }

        public void Detach(IntPtr anchorHandle)
        {
            NDKAPI.HwArAnchor_detach(m_ndkSession.SessionHandle, anchorHandle);
        }

        public void Release(IntPtr anchorHandle)
        {
            NDKAPI.HwArAnchor_release(anchorHandle);
        }

        //anchor list
        public IntPtr CreateList()
        {
            IntPtr listHandle = IntPtr.Zero;
            NDKAPI.HwArAnchorList_create(m_ndkSession.SessionHandle, ref listHandle);
            return listHandle;
        }

        public void DestroyList(IntPtr anchorListHandle)
        {
            NDKAPI.HwArAnchorList_destroy(anchorListHandle);
        }

        public int GetListSize(IntPtr anchorListHandle)
        {
            int size = 0;
            NDKAPI.HwArAnchorList_getSize(m_ndkSession.SessionHandle, anchorListHandle, ref size);
            return size;
        }

        public IntPtr AcquireListItem(IntPtr anchorListHandle, int index)
        {
            IntPtr anchorHandle = IntPtr.Zero;
            NDKAPI.HwArAnchorList_acquireItem(m_ndkSession.SessionHandle, anchorListHandle, index,
                ref anchorHandle);
            return anchorHandle;
        }

        public void AddListItem(IntPtr anchorListHandle,IntPtr anchorHandle)
        {
            NDKAPI.HwArAnchorList_addItem(m_ndkSession.SessionHandle, anchorListHandle, anchorHandle);
        }

        private struct NDKAPI
        {
            //anchor
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArAnchor_getPose(IntPtr sessionHandle, IntPtr anchorHandle, IntPtr poseHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArAnchor_getTrackingState(IntPtr sessionHandle, IntPtr anchorHandle,
                ref int trakingstate);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArAnchor_detach(IntPtr sessionHandle, IntPtr anchorHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArAnchor_release(IntPtr anchorHandle);

            //anchor list
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArAnchorList_create(IntPtr sessionHandle, ref IntPtr outAnchorListHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArAnchorList_destroy(IntPtr anchorListHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArAnchorList_getSize(IntPtr sessionHandle, IntPtr anchorListHandle,
                ref int outSize);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArAnchorList_acquireItem(IntPtr sessionHandle, IntPtr anchorListHandle,
                                int index, ref IntPtr outAnchorHandle);
            //only valid in arengine
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArAnchorList_addItem(IntPtr sessionHandle, IntPtr anchorListHandle,
                            IntPtr anchorHandle);
        }
    }
}