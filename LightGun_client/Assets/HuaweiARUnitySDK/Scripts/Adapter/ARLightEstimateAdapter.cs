/*
 * 2019.09.19 - Huawei modifies and matches AREngine.
 * Copyright (C) 2019. Huawei Technologies Co., Ltd. All rights reserved.
 */

//-----------------------------------------------------------------------
// <copyright file="LightEstimateApi.cs" company="Google">
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
    using System;
    internal class ARLightEstimateAdapter
    {

        private NDKSession m_ndkSession;

        public ARLightEstimateAdapter(NDKSession Session)
        {
            m_ndkSession = Session;
        }

        public IntPtr Create()
        {
            IntPtr lightEstimateHandle = IntPtr.Zero;
            NDKAPI.HwArLightEstimate_create(m_ndkSession.SessionHandle, ref lightEstimateHandle);
            return lightEstimateHandle;
        }

        public void Destroy(IntPtr lightEstimateHandle)
        {
            NDKAPI.HwArLightEstimate_destroy(lightEstimateHandle);
        }

        public bool GetState(IntPtr lightEstimateHandle)
        {
            int state = 0;
            NDKAPI.HwArLightEstimate_getState(m_ndkSession.SessionHandle, lightEstimateHandle, ref state);
            return state == 1 ? true : false;
        }

        public float GetPixelIntensity(IntPtr lightEstimateHandle)
        {
            float pixelIntensity = 0;
            NDKAPI.HwArLightEstimate_getPixelIntensity(m_ndkSession.SessionHandle,
                lightEstimateHandle, ref pixelIntensity);
            return pixelIntensity;
        }

        private struct NDKAPI
        {
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArLightEstimate_create(IntPtr sessionHandle, ref IntPtr outLightEstimateHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArLightEstimate_destroy(IntPtr lightEstimateHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArLightEstimate_getState(IntPtr sessionHandle, IntPtr lightEstimateHandle,
                                ref int LightEstimateState);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArLightEstimate_getPixelIntensity(IntPtr sessionHandle, IntPtr lightEstimateHandle,
                                         ref float outPixelIntensity);
        }
    }
}
