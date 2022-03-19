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
     * @brief Thrown when a fatal error occurs.
     * \else
     * @brief 致命错误时抛出。
     * \endif
     */
    class ARFatalException : ApplicationException
    {
        ///@cond EXCLUDE_DOXYGEN
        public ARFatalException() : base() { }
        public ARFatalException(string message) : base(message) { }

        public ARFatalException(string message, Exception innerException) : base(message, innerException) { }
        protected ARFatalException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        ///@endcond
    }
}
