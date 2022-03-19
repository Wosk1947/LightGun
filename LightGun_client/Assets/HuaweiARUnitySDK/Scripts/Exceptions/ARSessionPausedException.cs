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
     * @brief Thrown when an operation requires HUAWEI AR Engine to be running, but it's actually paused.
     * \else
     * @brief 当一个调用要求引擎处于运行状态，但是实际上处于暂时状态时，抛出。
     * \endif
     */
    class ARSessionPausedException:ApplicationException
    {
        ///@cond EXCLUDE_DOXYGEN
        public ARSessionPausedException() : base() { }
        public ARSessionPausedException(string message) : base(message) { }

        public ARSessionPausedException(string message, Exception innerException) : base(message, innerException) { }
        protected ARSessionPausedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        ///@endcond
    }
}
