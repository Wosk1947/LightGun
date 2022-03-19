/*
 * 2019.09.19 - Huawei modifies and matches AREngine.
 * Copyright (C) 2019. Huawei Technologies Co., Ltd. All rights reserved.
 */

//-----------------------------------------------------------------------
// <copyright file="ARCoreBackgroundRenderer.cs" company="Google">
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

namespace HuaweiARUnitySDK
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.XR;
    using HuaweiARInternal;
    /**
     * \if english
     * @brief Renders the device's camera as a background to the attached Unity camera component.
     * \else
     * @brief 将相机预览渲染到unity的相机背景上。
     * \endif
     */
    public class BackGroundRenderer : MonoBehaviour
    {
        private Camera m_camera;
        private ARBackgroundRenderer m_backgroundRenderer;
        private static float[] QUAD_TEXCOORDS = { 0f, 1f, 0f, 0f, 1f, 1f, 1f, 0f };
        private float[] transformedUVCoords = QUAD_TEXCOORDS;

        /**
         * \if english
         * A material used to render the AR background image.
         * \else
         * 渲染相机背景所使用的材质。
         * \endif
         */
        public Material BackGroundMaterial;

        private void Start()
        {
            m_camera = GetComponent<Camera>();
        }

        /**
         * \if english
         * @brief Inherit from Unity Update().
         * \else
         * @brief Unity的Update()。
         * \endif
         */
        public void Update()
        {
            if (BackGroundMaterial == null)
            {
                return;
            }
            else if (!ARFrame.TextureIsAvailable())
            {
                return;
            }

            const string backroundTex = "_MainTex";
            const string leftTopBottom = "_UvLeftTopBottom";
            const string rightTopBottom = "_UvRightTopBottom";

            BackGroundMaterial.SetTexture(backroundTex, ARFrame.CameraTexture);

            if (ARFrame.IsDisplayGeometryChanged())
            {
                transformedUVCoords = ARFrame.GetTransformDisplayUvCoords(QUAD_TEXCOORDS);
            }

            BackGroundMaterial.SetVector(leftTopBottom, new Vector4(transformedUVCoords[0], transformedUVCoords[1],
                transformedUVCoords[2], transformedUVCoords[3]));
            BackGroundMaterial.SetVector(rightTopBottom, new Vector4(transformedUVCoords[4], transformedUVCoords[5],
                transformedUVCoords[6], transformedUVCoords[7]));
            Pose p = ARFrame.GetPose();
            m_camera.transform.position = p.position;
            m_camera.transform.rotation = p.rotation;
            m_camera.projectionMatrix = ARSession.GetProjectionMatrix(m_camera.nearClipPlane, m_camera.farClipPlane);

            if (m_backgroundRenderer == null)
            {
                m_backgroundRenderer = new ARBackgroundRenderer();
                m_backgroundRenderer.backgroundMaterial = BackGroundMaterial;
                m_backgroundRenderer.camera = m_camera;
                m_backgroundRenderer.mode = ARRenderMode.MaterialAsBackground;
            }
        }
    }
}