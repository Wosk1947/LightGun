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

    internal class HuaweiArApkAdapter
    {
        public ARAvailability CheckAvailability()
        {
            int availability = 0;
            NDKAPI.HwArApk_checkAvailability(ARUnityHelper.Instance.GetJEnv(),
                ARUnityHelper.Instance.GetActivityHandle(), ref availability);
            return (ARAvailability)availability;
        }

        public ARInstallStatus RequestInstall(bool userRequestedInstall)
        {
            int installState = 0;
            NDKARStatus status = NDKAPI.HwArApk_requestInstall(ARUnityHelper.Instance.GetJEnv(),
                ARUnityHelper.Instance.GetActivityHandle(), userRequestedInstall, ref installState);
            ARExceptionAdapter.ExtractException(status);
            return (ARInstallStatus)installState;
        }

        public Boolean IsAREngineApkReady()
        {
            return NDKAPI.HwArEnginesApk_isAREngineApkReady(ARUnityHelper.Instance.GetJEnv(),
                ARUnityHelper.Instance.GetActivityHandle());
        }

        private struct NDKAPI
        {
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern void HwArApk_checkAvailability(IntPtr env,
                                IntPtr application_context,
                                ref int out_availability);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern NDKARStatus HwArApk_requestInstall(IntPtr env,
                                                IntPtr application_activity,
                                                bool user_requested_install,
                                                ref int out_install_status);
            [DllImport(AdapterConstants.HuaweiARNativeApi)]
            public static extern Boolean HwArEnginesApk_isAREngineApkReady(IntPtr env,
                                                IntPtr application_activity);
        }
    }
}
