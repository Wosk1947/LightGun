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
     * @brief HUAWEI AR Engine is unavailable.
     * \else
     * @brief HUAWEI AR Engine不可用。
     * \endif
     */
    public class ARUnavailableException:ApplicationException
    {
        ///@cond EXCLUDE_DOXYGEN
        public ARUnavailableException() : base() { }
        public ARUnavailableException(string message) : base(message) { }

        public ARUnavailableException(string message, Exception innerException) : base(message, innerException) { }
        protected ARUnavailableException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        ///@endcond
    }
}
