/*
 * 2019.09.19 - Huawei modifies and matches AREngine.
 * Copyright (C) 2019. Huawei Technologies Co., Ltd. All rights reserved.
 */
//-----------------------------------------------------------------------
// <copyright file="CameraConfig.cs" company="Google">
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
    using System;
    using HuaweiARInternal;
    using UnityEngine;
    using System.Collections.Generic;

    /**
     * \if english
     * @brief Provides details of a camera configuration such as size of the CPU image and GPU texture.
     * \else
     * @brief 提供相机的配置，例如，CPU上图片的宽高，GPU上贴图（预览纹理）的宽高。
     * \endif
     */
    public class ARCameraConfig
    {
        internal IntPtr m_cameraConfigHandle = IntPtr.Zero;
        internal NDKSession m_ndkSession;

        internal ARCameraConfig()
        {
        }
        internal ARCameraConfig(IntPtr cameraConfigHandle, NDKSession session)
        {
            m_cameraConfigHandle = cameraConfigHandle;
            m_ndkSession = session;
        }
        ~ARCameraConfig()
        {
            destory();
        }

        private void create()
        {
            m_cameraConfigHandle = m_ndkSession.CameraConfigAdapter.Create();
        }

        private void destory()
        {
            m_ndkSession.CameraConfigAdapter.Destory(m_cameraConfigHandle);
            m_cameraConfigHandle = IntPtr.Zero;
        }

        /**
         * \if english
         * @brief Get the dimensions of the CPU-accessible image byte (\link ARCameraImageBytes \endlink)for the camera configuration.
         * @return The dimensions of the image. \c x is the width. \c y is the height.
         * \else
         * @brief 获取CPU上可访问的图片（\link ARCameraImageBytes \endlink）的宽高。
         * @return 图片的宽高。\c x 分量是宽，\c y 分量是高。
         * \endif
         */
        public Vector2Int GetImageDimensions()
        {
            return m_ndkSession.CameraConfigAdapter.GetImageDimensions(m_cameraConfigHandle);
        }

        /**
         * \if english
         * @brief Get the dimensions of the GPU-accessible external texture (namely the camera preview \link ARFrame.CameraTexture \endlink)
         * for the camera configuration.
         * @return The dimensions of the external texture. \c x is the width. \c y is the height.
         * \else
         * @brief 获取GPU上可访问的外部纹理（也就是相机预览\link ARFrame.CameraTexture \endlink）的宽高。
         * @return 纹理的宽高。\c x 分量是宽，\c y 分量是高。
         * \endif
         */
        public Vector2Int GetTextureDimensions()
        {
            return m_ndkSession.CameraConfigAdapter.GetTextureDimensions(m_cameraConfigHandle);
        }

    }
}
