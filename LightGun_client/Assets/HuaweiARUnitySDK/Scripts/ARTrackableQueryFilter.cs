/*
 * 2019.09.19 - Huawei modifies and matches AREngine.
 * Copyright (C) 2019. Huawei Technologies Co., Ltd. All rights reserved.
 */

//-----------------------------------------------------------------------
// <copyright file="TrackableQueryFilter.cs" company="Google">
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
     * @brief Enumeration of query filter.
     * \else
     * @brief 查询过滤器的枚举。
     * \endif
     */
    public enum ARTrackableQueryFilter
    {
        /**
         * \if english
         * Require all items.
         * \else
         * 需要所有的项。
         * \endif
         */
        ALL,

        /**
         * \if english
         * Only new items in current frame is required.
         * \else
         * 仅需要当前帧新出现的项。
         * \endif
         */
        NEW,

        /**
         * \if english
         * Only updated items in current frame is required.
         * \else
         * 仅需要当前帧变化过的项。
         * \endif
         */
        UPDATED,
    }
}
