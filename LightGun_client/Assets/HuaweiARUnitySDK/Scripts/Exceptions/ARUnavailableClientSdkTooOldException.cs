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
     * @brief Thrown when current used sdk is too old to be compatiable with installed service .
     * \else
     * @brief 当前使用的sdk版本过旧，与安装的service不兼容。
     * \endif
     */
    public class ARUnavailableClientSdkTooOldException:ARUnavailableException
    {
        ///@cond EXCLUDE_DOXYGEN
        public ARUnavailableClientSdkTooOldException() : base() { }
        public ARUnavailableClientSdkTooOldException(string message) : base(message) { }

        public ARUnavailableClientSdkTooOldException(string message, Exception innerException) : base(message, innerException) { }
        protected ARUnavailableClientSdkTooOldException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        ///@endcond
    }
}
