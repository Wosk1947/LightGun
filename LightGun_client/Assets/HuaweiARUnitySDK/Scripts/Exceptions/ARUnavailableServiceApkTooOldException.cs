// Copyright (C) 2019. Huawei Technologies Co., Ltd. All rights reserved.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the Apache License, Version 2.0.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// Apache License for more details.

namespace HuaweiARUnitySDK
{
    using System;
    using System.Runtime.Serialization;

    /**
     * \if english
     * @brief Thrown when installed service is too old to be compatiable with current used sdk.
     * \else
     * @brief 当安装的引擎过旧，与当前使用的SDK不兼容时抛出该异常
     * \endif
     */
    class ARUnavailableServiceApkTooOldException:ARUnavailableException
    {
        ///@cond EXCLUDE_DOXYGEN
        public ARUnavailableServiceApkTooOldException() : base() { }
        public ARUnavailableServiceApkTooOldException(string message) : base(message) { }

        public ARUnavailableServiceApkTooOldException(string message, Exception innerException) : base(message, innerException) { }
        protected ARUnavailableServiceApkTooOldException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        ///@endcond
    }
}
