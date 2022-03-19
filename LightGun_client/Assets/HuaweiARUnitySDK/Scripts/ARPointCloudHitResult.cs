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
    using HuaweiARInternal;
    using System;

    /**
     * \if english
     * @deprecated Use \link ARHitResult \endlink instead.
     * \else
     * @deprecated 请使用\link ARHitResult \endlink。
     * \endif
     */
    [Obsolete]
    public class ARPointCloudHitResult:ARHitResult
    {
        /// @cond EXCLUDE_DOXYGEN
        public ARPointCloud PointCloud { get; private set; }
        internal ARPointCloudHitResult(IntPtr hitResultHandle,NDKSession session,ARPointCloud pointCloud)
            : base(hitResultHandle, session)
        {
            PointCloud = pointCloud;
        }
        ///@endcond
    }
}
