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
     * @brief Thrown when an operation requires a gl context.
     * \else
     * @brief 当调用缺少gl上下文时抛出。
     * \endif
     */
    public class ARMissingGlContextException:ApplicationException
    {
        ///@cond EXCLUDE_DOXYGEN
        public ARMissingGlContextException() : base() { }
        public ARMissingGlContextException(string message) : base(message) { }

        public ARMissingGlContextException(string message, Exception innerException) : base(message, innerException) { }
        protected ARMissingGlContextException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        ///@endcond
    }
}
