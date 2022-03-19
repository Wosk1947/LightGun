/*
 * 2019.09.19 - Huawei modifies and matches AREngine.
 * Copyright (C) 2019. Huawei Technologies Co., Ltd. All rights reserved.
 */
//-----------------------------------------------------------------------
// <copyright file="CameraConfigApi.cs" company="Google">
//
// Copyright 2018 Google LLC. All Rights Reserved.
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
    using HuaweiARUnitySDK;
    using UnityEngine;

    internal class ARCameraConfigAdapter
    {
        private NDKSession m_ndkSession;

        public ARCameraConfigAdapter(NDKSession session)
        {
            m_ndkSession = session;
        }

        public IntPtr Create()
        {
            IntPtr cameraConfigHandle = IntPtr.Zero;
            NDKAPI.HwArCameraConfig_create(m_ndkSession.SessionHandle, ref cameraConfigHandle);
            return cameraConfigHandle;
        }

        public void Destory(IntPtr cameraConfigHandle)
        {
            NDKAPI.HwArCameraConfig_destroy(cameraConfigHandle);
        }

        public Vector2Int GetImageDimensions(IntPtr cameraConfigHandle)
        {
            int width = 0;
            int height = 0;
            NDKAPI.HwArCameraConfig_getImageDimensions(m_ndkSession.SessionHandle, cameraConfigHandle, ref width, ref height);
            return new Vector2Int(width, height);
        }

        public Vector2Int GetTextureDimensions(IntPtr cameraConfigHandle)
        {
            int width = 0;
            int height = 0;
            NDKAPI.HwArCameraConfig_getTextureDimensions(m_ndkSession.SessionHandle, cameraConfigHandle, ref width, ref height);
            return new Vector2Int(width, height);
        }


        private struct NDKAPI
        {
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArCameraConfig_create(IntPtr sessionHandle, ref IntPtr cameraConfigHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArCameraConfig_destroy(IntPtr cameraConfigHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArCameraConfig_getImageDimensions(IntPtr sessionHandle, IntPtr cameraConfigHandle,
                ref int outWidth, ref int outHeight);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArCameraConfig_getTextureDimensions(IntPtr sessionHandle, IntPtr cameraConfigHandle,
                ref int outWidth, ref int outHeight);

        }
    }
}
