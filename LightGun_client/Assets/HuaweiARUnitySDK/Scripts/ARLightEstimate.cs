/*
 * 2019.09.19 - Huawei modifies and matches AREngine.
 * Copyright (C) 2019. Huawei Technologies Co., Ltd. All rights reserved.
 */
//-----------------------------------------------------------------------
// <copyright file="LightEstimate.cs" company="Google">
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
    /**
     * \if english
     * @brief An estimation of lighting conditinos in the environment coorespoding to an \link ARFrame \endlink.
     * \else
     * @brief 与\link ARFrame \endlink相关的环境光估计。
     * \endif
     */
    public class ARLightEstimate
    {
        /**
         * \if english
         * @brief The intensity of environment light.
         * \else
         * @brief 环境光强。
         * \endif
         */
        public float PixelIntensity { get; private set; }

        /**
         * \if english
         * @brief Indicate whether this data is valid.
         * \else
         * @brief 数据是否有效。
         * \endif
         */
        public bool Valid { get; private set; }

        ///@cond EXCLUDE_DOXYGEN
        public ARLightEstimate(bool valid,float intensity)
        {
            Valid = valid;
            PixelIntensity = intensity;
        }
        ///@endcond
    }
}
