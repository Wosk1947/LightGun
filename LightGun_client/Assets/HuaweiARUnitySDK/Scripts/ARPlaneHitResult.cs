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
     * @deprecated This class has been deprecated, please use \link ARHitResult \endlink.
     * \else
     * @deprecated 当前类已废弃，请使用\link ARHitResult \endlink。
     * \endif
     */
    [Obsolete]
    public class ARPlaneHitResult:ARHitResult
    {
        /// @cond EXCLUDE_DOXYGEN
        public ARPlane Plane
        {
            get
            {
                ARTrackable trackable = GetTrackable();
                return trackable is ARPlane ? (ARPlane)trackable : null;
            }
        }

        public bool IsHitInExtents
        {
            get
            {
                return Plane == null ? false : Plane.IsPoseInExtents(HitPose);
            }
        }

        public bool IsHitInPolygon
        {
            get
            {
                return Plane == null ? false : Plane.IsPoseInPolygon(HitPose);
            }
        }

        internal ARPlaneHitResult(IntPtr planeHitResult, NDKSession session) : base(planeHitResult, session) { }

        public bool IsHitOnFrontFace { get { return true; } }//Deprecated member
        /// @endcond
    }
}
