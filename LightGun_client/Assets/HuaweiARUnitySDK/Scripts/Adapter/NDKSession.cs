/*
 * 2019.09.19 - Huawei modifies and matches AREngine.
 * Copyright (C) 2019. Huawei Technologies Co., Ltd. All rights reserved.
 */

//-----------------------------------------------------------------------
// <copyright file="NativeSession.cs" company="Google">
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

    internal class NDKSession
    {

        public IntPtr SessionHandle { get; private set; }
        public IntPtr FrameHandle { get; set; }
        public IntPtr ConfigHandle { get; set; }
        public ARExceptionAdapter ExceptionAdapter { get; private set; }
        public ARAnchorManager AnchorManager { get; private set; }
        public ARTrackableManager TrackableManager { get; private set; }
        public ARCameraMetadataAdapter CameraMetadataAdapter { get; private set; }
        public ARHitResultAdapter HitResultAdapter { get; private set; }
        public ARPointCloudAdapter PointCloudAdapter { get; private set; }
        public ARFrameAdapter FrameAdapter { get; private set; }
        public ARSessionAdapter SessionAdapter { get; private set; }
        public ARCameraAdapter CameraAdapter { get; private set; }
        public ARPoseAdapter PoseAdapter { get; private set; }
        public ARConfigBaseAdapter ConfigBaseAdapter { get; private set; }
        public ARLightEstimateAdapter LightEstimateAdapter { get; private set; }
        public ARAnchorAdapter AnchorAdapter { get; private set; }


        public ARTrackbaleAdapter TrackableAdapter { get; private set; }
        public ARPlaneAdapter PlaneAdapter { get; private set; }
        public ARPointAdapter PointAdapter { get; private set; }
        public ARImageAdapter ImageAdapter { get; private set; }

        //ARBody
        public ARBodyAdapter BodyAdapter { get; private set; }

        //ARCameraConfig
        public ARCameraConfigAdapter CameraConfigAdapter { get; private set; }

        //ARFace
        public ARFaceGeometryAdapter FaceGeometryAdapter { get; private set; }
        public ARFaceBlendShapeAdapter FaceBlendShapeAdapter { get; private set; }
        public ARFaceAdapter FaceAdapter { get; private set; }

        //ARHand
        public ARHandAdapter HandAdapter { get; private set; }

        //ARAugmentedImage
        public ARAugmentedImageAdapter AugmentedImageAdapter { get; private set; }
        public ARAugmentedImageDatabaseAdapter AugmentedImageDatabaseAdapter { get; private set; }

        //ARSceneMesh
        public ARSceneMeshAdapter SceneMeshAdapter { get; private set; }

        //ARVideoSource
        public ARVideoSourceAdapter VideoSourceAdapter { get; private set; }

        public NDKSession()
        {
            CameraMetadataAdapter = new ARCameraMetadataAdapter(this);
            AnchorAdapter = new ARAnchorAdapter(this);
            HitResultAdapter = new ARHitResultAdapter(this);
            PointCloudAdapter = new ARPointCloudAdapter(this);
            AnchorManager = new ARAnchorManager(this);
            TrackableManager = new ARTrackableManager(this);
            FrameAdapter = new ARFrameAdapter(this);
            SessionAdapter = new ARSessionAdapter(this);
            CameraAdapter = new ARCameraAdapter(this);
            PoseAdapter = new ARPoseAdapter(this);
            ConfigBaseAdapter = new ARConfigBaseAdapter(this);
            LightEstimateAdapter = new ARLightEstimateAdapter(this);

            TrackableAdapter = new ARTrackbaleAdapter(this);
            PlaneAdapter = new ARPlaneAdapter(this);
            PointAdapter = new ARPointAdapter(this);
            ImageAdapter = new ARImageAdapter(this);
            //ARFace
            FaceAdapter = new ARFaceAdapter(this);
            FaceGeometryAdapter = new ARFaceGeometryAdapter(this);
            FaceBlendShapeAdapter = new ARFaceBlendShapeAdapter(this);
            //ARBoady
            BodyAdapter = new ARBodyAdapter(this);
            //ARHand
            HandAdapter = new ARHandAdapter(this);
            //ARAugmentedImage
            AugmentedImageAdapter = new ARAugmentedImageAdapter(this);
            AugmentedImageDatabaseAdapter = new ARAugmentedImageDatabaseAdapter(this);
            //ARCameraConfig
            CameraConfigAdapter = new ARCameraConfigAdapter(this);
            //ARSceneMesh
            SceneMeshAdapter = new ARSceneMeshAdapter(this);
            //ARVideoSource
            VideoSourceAdapter = new ARVideoSourceAdapter(this);

            SessionHandle = SessionAdapter.Create();
            FrameHandle = IntPtr.Zero;
        }

        ~NDKSession()
        {
            if (FrameHandle != IntPtr.Zero)
            {
                FrameAdapter.Destroy(FrameHandle);
            }
            if(SessionHandle != IntPtr.Zero)
            {
                SessionAdapter.Destroy();
            }
            if(ConfigHandle != IntPtr.Zero)
            {
                ConfigBaseAdapter.Destroy(ConfigHandle);
            }
        }
    }
}
