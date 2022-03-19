/*
 * 2019.09.19 - Huawei modifies and matches AREngine.
 * Copyright (C) 2019. Huawei Technologies Co., Ltd. All rights reserved.
 */

//-----------------------------------------------------------------------
// <copyright file="ApiArStatus.cs" company="Google">
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

namespace HuaweiARInternal
{
    public enum NDKARStatus
    {
        HWAR_SUCCESS = 0,
        HWAR_ERROR_INVALID_ARGUMENT = -1,
        HWAR_ERROR_FATAL = -2,
        HWAR_ERROR_SESSION_PAUSED = -3,
        HWAR_ERROR_SESSION_NOT_PAUSED = -4,
        HWAR_ERROR_NOT_TRACKING = -5,
        HWAR_ERROR_TEXTURE_NOT_SET = -6,
        HWAR_ERROR_MISSING_GL_CONTEXT = -7,
        HWAR_ERROR_UNSUPPORTED_CONFIGURATION = -8,
        HWAR_ERROR_CAMERA_PERMISSION_NOT_GRANTED = -9,
        HWAR_ERROR_DEADLINE_EXCEEDED = -10,
        HWAR_ERROR_RESOURCE_EXHAUSTED = -11,
        HWAR_ERROR_NOT_YET_AVAILABLE = -12,
        HWAR_ERROR_CAMERA_NOT_AVAILABLE = -13,
        HWAR_UNAVAILABLE_AREXECUTOR_NOT_INSTALLED = -100,
        HWAR_UNAVAILABLE_DEVICE_NOT_COMPATIBLE = -101,
        HWAR_UNAVAILABLE_APK_TOO_OLD = -103,
        HWAR_UNAVAILABLE_SDK_TOO_OLD = -104,
        HWAR_UNAVAILABLE_USER_DECLINED_INSTALLATION = -105,

        HWAR_UNAVAILABLE_EMUI_NOT_CAPABLE = -1000,
        HWAR_UNAVAILABLE_CONNECT_SERVER_TIME_OUT = -1001,
    }
}
