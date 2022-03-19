/*
 * 2019.09.19 - Huawei modifies and matches AREngine.
 * Copyright (C) 2019. Huawei Technologies Co., Ltd. All rights reserved.
 */
//-----------------------------------------------------------------------
// <copyright file="AugmentedImage.cs" company="Google">
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

///@cond ContainImageAR
namespace HuaweiARUnitySDK
{
    using HuaweiARInternal;
    using System;
    using UnityEngine;

    /**
     * \if english
     * @brief Return tracking results for image detecting and tracking in the environment,
     * derived from ARTrackableBase.
     *
     * \else
     * @brief 用于Image图像识别和跟踪时返回跟踪结果，派生自ARTrackableBase。
     *
     * \endif
     */
    public class ARAugmentedImage : ARTrackable
    {

        internal ARAugmentedImage(IntPtr trackableHandle, NDKSession session) : base(trackableHandle, session)
        {

        }

        /**
         * \if english
         * @brief  Returns the pose of the center of the augmented image, in world coordinates.
         *
         * \else
         * @brief 获取识别到图片中心点在世界坐标系中的位置信息。
         *
         * \endif
         */
        public Pose GetCenterPose()
        {
            return m_ndkSession.AugmentedImageAdapter.GetCenterPose(m_trackableHandle);
        }

        /**
         * \if english
         * @brief  Returns the estimated width, in metres, of the corresponding physical image,
         * as measured along the local X-axis of the coordinate space centered on the image.
         *
         * \else
         * @brief 获取以图像为中心的坐标系沿X轴测试物理图片的估计宽度（以米为单位）。
         *
         * \endif
         */
        public float GetExtentX()
        {
            return m_ndkSession.AugmentedImageAdapter.GetExtentX(m_trackableHandle);
        }

        /**
         * \if english
         * @brief  Returns the estimated height, in metres, of the corresponding physical image,
         * as measured along the local Z-axis of the coordinate space centered on the image.
         *
         * \else
         * @brief 获取以图像为中心的坐标系沿Z轴测试物理图片的估计高度（以米为单位）。
         *
         * \endif
         */
        public float GetExtentZ()
        {
            return m_ndkSession.AugmentedImageAdapter.GetExtentZ(m_trackableHandle);
        }

        /**
         * \if english
         * @brief  Returns the zero-based positional index of this augmented image from its originating image database.
         *
         * \else
         * @brief 获取识别到图片配置的Index（以0开始，通过ImageDatabase配置顺序增加）。
         *
         * \endif
         */
        public int GetDataBaseIndex()
        {
            return m_ndkSession.AugmentedImageAdapter.GetDataBaseIndex(m_trackableHandle);
        }

        /**
         * \if english
         * @brief  Returns the name of this augmented image.
         *
         * \else
         * @brief 获取识别到图片配置的名字。
         *
         * \endif
         */
        public string AcquireName()
        {
            return m_ndkSession.AugmentedImageAdapter.AcquireName(m_trackableHandle);
        }
    }
}
///@endcond