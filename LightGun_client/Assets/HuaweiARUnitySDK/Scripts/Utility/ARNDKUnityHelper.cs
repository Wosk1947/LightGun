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
    using UnityEngine;
    using System.Runtime.InteropServices;
    using HuaweiARUnitySDK;
    internal class ARUnityHelper
    {
        private IntPtr m_activityHandle;
        private int m_textureId = -1;
        private static ARUnityHelper s_unityHelper;

        private  ARUnityHelper()
        {
            m_activityHandle = IntPtr.Zero;
        }

        public static ARUnityHelper Instance
        {
            get
            {
                if (null == s_unityHelper)
                {
                    s_unityHelper= new ARUnityHelper();
                }
                return s_unityHelper;
            }
        }


        public IntPtr GetJEnv()
        {
            return NDKAPI.GetJEnv();
        }

        public void AttachCurrentThread()
        {
            NDKAPI.AttachCurrentThread();
        }

        public void DetachCurrentThread()
        {
            NDKAPI.DetachCurrentThread();
        }

        public IntPtr GetActivityHandle()
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            m_activityHandle = activity.GetRawObject();
            return m_activityHandle;
        }

        public int GetTextureId()
        {
            if (m_textureId == -1)
            {
                m_textureId = NDKAPI.getTextureId();
            }
            return m_textureId;
        }

        private struct NDKAPI
        {
           [DllImport(AdapterConstants.UnityPluginApi)]
            public static extern int getTextureId();

            [DllImport(AdapterConstants.UnityPluginApi)]
            public static extern IntPtr GetJEnv();

            [DllImport(AdapterConstants.UnityPluginApi)]
            public static extern void DetachCurrentThread();

            [DllImport(AdapterConstants.UnityPluginApi)]
            public static extern void AttachCurrentThread();
        }
    }
}
