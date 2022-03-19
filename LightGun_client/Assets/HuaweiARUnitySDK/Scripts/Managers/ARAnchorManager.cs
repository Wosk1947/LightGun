// Copyright (C) 2019. Huawei Technologies Co., Ltd. All rights reserved.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the Apache License, Version 2.0.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// Apache License for more details.

namespace HuaweiARInternal
{
    using System;
    using System.Collections.Generic;
    using HuaweiARUnitySDK;

    internal class ARAnchorManager
    {
        private  Dictionary<IntPtr, ARAnchor> m_anchorDict =
            new Dictionary<IntPtr, ARAnchor>(new IntPtrComparer());

        private  NDKSession m_ndkSession;

        public ARAnchorManager(NDKSession session)
        {
            m_ndkSession = session;
        }

        public  ARAnchor ARAnchorFactory(IntPtr nativeHandle, bool isCreate=true)
        {
            if (nativeHandle == IntPtr.Zero)
            {
                return null;
            }
            ARAnchor result;
            if (m_anchorDict.TryGetValue(nativeHandle, out result))
            {
                m_ndkSession.AnchorAdapter.Release(nativeHandle);
                return result;
            }
            if (isCreate)
            {
                ARAnchor anchor = new ARAnchor(nativeHandle,m_ndkSession);
                m_anchorDict.Add(nativeHandle, anchor);
                return anchor;
            }
            return null;
        }

        public void RemoveAnchor(ARAnchor anchor)
        {
            m_anchorDict.Remove(anchor.m_anchorHandle);
        }

        public void GetAllAnchor(List<ARAnchor> anchors)
        {
            anchors.Clear();
            foreach(ARAnchor anchor in m_anchorDict.Values)
            {
                anchors.Add(anchor);
            }
        }
    }
}
