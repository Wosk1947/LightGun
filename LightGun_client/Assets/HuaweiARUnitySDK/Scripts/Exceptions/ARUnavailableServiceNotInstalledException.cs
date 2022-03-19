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
     * @brief Thrown when HUAWEI AR Engine is not installed.
     * \else
     * @brief 当引擎没有安装时抛出。
     * \endif
     */
    public class ARUnavailableServiceNotInstalledException:ARUnavailableException
	{
        ///@cond EXCLUDE_DOXYGEN
        public ARUnavailableServiceNotInstalledException() : base() { }
        public ARUnavailableServiceNotInstalledException(string message) : base(message) { }

        public ARUnavailableServiceNotInstalledException(string message, Exception innerException) : base(message, innerException) { }
        protected ARUnavailableServiceNotInstalledException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        ///@endcond
    }
}

