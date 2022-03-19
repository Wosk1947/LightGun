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
     * @brief HUAWEI AR Engine is not compatible with current OS.
     * \else
     * @brief 当前系统不支持HUAWEI AR Engine。
     * \endif
     */
    public class ARUnavailableEmuiNotCompatibleException:ARUnavailableException
    {
        ///@cond EXCLUDE_DOXYGEN
        public ARUnavailableEmuiNotCompatibleException() : base() { }
        public ARUnavailableEmuiNotCompatibleException(string message) : base(message) { }

        public ARUnavailableEmuiNotCompatibleException(string message, Exception innerException) : base(message, innerException) { }
        protected ARUnavailableEmuiNotCompatibleException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        ///@endcond
    }
}
