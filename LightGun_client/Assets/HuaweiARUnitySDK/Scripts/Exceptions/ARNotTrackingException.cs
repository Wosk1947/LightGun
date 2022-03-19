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
     * @brief Thrown when an operation requires trackable to be tracking, but it's actually not.
     * \else
     * @brief 当调用要求trackable处于跟踪状态，但实际未跟踪时抛出。
     * \endif
     */
    class ARNotTrackingException :ApplicationException
    {
        ///@cond EXCLUDE_DOXYGEN
        public ARNotTrackingException() : base() { }
        public ARNotTrackingException(string message) : base(message) { }

        public ARNotTrackingException(string message, Exception innerException) : base(message, innerException) { }
        protected ARNotTrackingException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        ///@endcond
    }
}
