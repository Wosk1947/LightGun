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
     * @brief User declined installation.
     * \else
     * @brief 用户取消了安装。
     * \endif
     */
    class ARUnavailableUserDeclinedInstallationException :ARUnavailableException
    {
        ///@cond EXCLUDE_DOXYGEN
        public ARUnavailableUserDeclinedInstallationException() : base() { }
        public ARUnavailableUserDeclinedInstallationException(string message) : base(message) { }

        public ARUnavailableUserDeclinedInstallationException(string message, Exception innerException) : base(message, innerException) { }
        protected ARUnavailableUserDeclinedInstallationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        ///@endcond
    }
}
