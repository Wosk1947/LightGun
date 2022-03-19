/*
 * 2019.09.19 - Huawei modifies and matches AREngine.
 * Copyright (C) 2019. Huawei Technologies Co., Ltd. All rights reserved.
 */
//-----------------------------------------------------------------------
// <copyright file="MarshalingHelper.cs" company="Google">
//
// Copyright 2016 Google LLC. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

namespace HuaweiARInternal
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    internal class MarshalingHelper
    {

        public static T[] GetArrayOfUnmanagedArrayElement<T>(IntPtr arrayPtr, int arrayLength) where T: struct
        {
            if (IntPtr.Zero == arrayPtr || arrayLength == 0)
            {
                return new T[0];
            }
            T[] ret = new T[arrayLength];
            for(int i = 0; i < arrayLength; i++)
            {
                ret[i] = GetValueOfUnmanagedArrayElement<T>(arrayPtr, i);
            }
            return ret;
        }

        public static void AppendUnManagedArray2List<T>(IntPtr arrayPtr, int arrayLength, List<T> list) where T:struct
        {
            if(IntPtr.Zero==arrayPtr || null == list)
            {
                return;
            }
            for(int i = 0; i < arrayLength; i++)
            {
                IntPtr ptr = new IntPtr(arrayPtr.ToInt64() + (Marshal.SizeOf(typeof(T)) * i));
                list.Add((T)Marshal.PtrToStructure(ptr, typeof(T)));
            }
        }
        public static T GetValueOfUnmanagedArrayElement<T>(IntPtr arrayPtr, int arrayIndex) where T : struct
        {
            IntPtr ptr = new IntPtr(arrayPtr.ToInt64() + (Marshal.SizeOf(typeof(T)) * arrayIndex));
            T value = (T)Marshal.PtrToStructure(ptr, typeof(T));
            return value;
        }
    }
}
