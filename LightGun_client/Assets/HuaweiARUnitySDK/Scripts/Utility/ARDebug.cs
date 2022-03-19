/*
 * 2019.09.19 - Huawei modifies and matches AREngine.
 * Copyright (C) 2019. Huawei Technologies Co., Ltd. All rights reserved.
 */
//-----------------------------------------------------------------------
// <copyright file="ARDebug.cs" company="Google">
//
// Copyright 2016 Google LLC. All Rights Reserved.
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

//#define __DEBUG__
//#define __USR_ANDROID_LOG__
namespace HuaweiARInternal
{
    using System;
    using UnityEngine;

    public class ARDebug
    {
        private static AndroidJavaClass AndroidUtilLog = new AndroidJavaClass("android.util.Log");

        private static string DefaultLogTag = "arengine_unity";

        public static void LogInfo(UnityEngine.Object message)
        {
#if __DEBUG__
#if __USR_ANDROID_LOG__
            AndroidUtilLog.CallStatic<int>("i", DefaultLogTag, message);
#else
            Debug.Log(message);
#endif
#endif
        }
        public static void LogInfo(string format, params object[] args)
        {
#if __DEBUG__

#if __USR_ANDROID_LOG__
            AndroidUtilLog.CallStatic<int>("i", DefaultLogTag, String.Format(format, args));
#else
            Debug.LogFormat(format,args);
#endif

#endif
        }

        public static void LogInfo(string tag, string format, params object[] args)
        {
#if __DEBUG__
#if __USR_ANDROID_LOG__
            AndroidUtilLog.CallStatic<int>("i", tag, String.Format(format, args));
#else
            Debug.LogFormat(format,args);
#endif
#endif
        }

        public static void LogWarning(UnityEngine.Object message)
        {
#if __USR_ANDROID_LOG__
            AndroidUtilLog.CallStatic<int>("w", DefaultLogTag, message);
#else
            Debug.LogWarning(message);
#endif

        }
        public static void LogWarning(string format, params object[] args)
        {
#if __USR_ANDROID_LOG__
            AndroidUtilLog.CallStatic<int>("w", DefaultLogTag, String.Format(format, args));
#else
            Debug.LogWarningFormat(format, args);
#endif

        }

        public static void LogError(UnityEngine.Object message)
        {
#if __USR_ANDROID_LOG__
            AndroidUtilLog.CallStatic<int>("e", DefaultLogTag, message);
#else
            Debug.LogError(message);
#endif

        }
        public static void LogError(string format, params object[] args)
        {
#if __USR_ANDROID_LOG__
            AndroidUtilLog.CallStatic<int>("e", DefaultLogTag, String.Format(format, args));
#else
            Debug.LogErrorFormat(format, args);
#endif

        }

    }
}
