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
     * @brief Thrown when input configuration is not supported in \link ARSession.Config(ARConfigBase) \endlink.
     * \else
     * @brief \link ARSession.Config(ARConfigBase) \endlink的配置项不被支持时抛出。
     * \endif
     */
    class ARUnSupportedConfigurationException :ApplicationException
    {
        ///@cond EXCLUDE_DOXYGEN
        public ARUnSupportedConfigurationException() : base() { }
        public ARUnSupportedConfigurationException(string message) : base(message) { }

        public ARUnSupportedConfigurationException(string message, Exception innerException) : base(message, innerException) { }
        protected ARUnSupportedConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        ///@endcond
    }
}
