/*
 * 2019.09.19 - Huawei modifies and matches AREngine.
 * Copyright (C) 2019. Huawei Technologies Co., Ltd. All rights reserved.
 */
//-----------------------------------------------------------------------
// <copyright file="CameraIntrinsics.cs" company="Google">
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

namespace HuaweiARUnitySDK
{
    using UnityEngine;

    /// <summary>
    /// A struct to provide camera intrinsics in AREngine.
    /// </summary>
    public struct ARCameraIntrinsics
    {
        public Vector2 ARFocalLength;
        public Vector2 ARPrincipalPoint;
        public Vector2Int ARImageDimensions;
        public float[] ARDistortion;

        internal ARCameraIntrinsics(Vector2 arFocalLength, Vector2 arPrincipalPoint,
                                     Vector2Int arImageDimensions, float[] arDistortion)
        {
            ARFocalLength = arFocalLength;
            ARPrincipalPoint = arPrincipalPoint;
            ARImageDimensions = arImageDimensions;
            ARDistortion = arDistortion;
        }
    }
}