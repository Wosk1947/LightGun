// Copyright (C) 2019. Huawei Technologies Co., Ltd. All rights reserved.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the Apache License, Version 2.0.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// Apache License for more details.

namespace HuaweiARInternal
{
    using System;
    using System.Runtime.InteropServices;
    using HuaweiARUnitySDK;

    internal class AREnginesSelectorAdapter
    {

        public AREnginesAvaliblity CheckDeviceExecuteAbility()
        {
            return NDKAPI.HwArEnginesSelector_checkAllAvailableEngines(ARUnityHelper.Instance.GetJEnv(),
                ARUnityHelper.Instance.GetActivityHandle());
        }

        public AREnginesType SetAREngine(AREnginesType executor)
        {
            return NDKAPI.HwArEnginesSelector_setAREngine(executor);
        }
        public AREnginesType GetCreatedEngine()
        {
            return NDKAPI.HwArEnginesSelector_getCreatedEngine();
        }


        private struct NDKAPI
        {
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern AREnginesAvaliblity HwArEnginesSelector_checkAllAvailableEngines(IntPtr envHandle, IntPtr applicationContextHandle);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern AREnginesType HwArEnginesSelector_setAREngine(AREnginesType executerType);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern AREnginesType HwArEnginesSelector_getCreatedEngine();
        }
    }
}
