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
    internal static class UtilConstants
    {
#if UNITY_EDITOR_WIN
        public const string HWAugmentedImageCliBinaryName = "AREngineImageDbTool";
#elif UNITY_EDITOR_LINUX
        public const string HWAugmentedImageCliBinaryName = "ImageDatabaseTool_linux";
#elif UNITY_EDITOR_OSX
		public const string HWAugmentedImageCliBinaryName = "NoToolInMac";
#endif
    }
}