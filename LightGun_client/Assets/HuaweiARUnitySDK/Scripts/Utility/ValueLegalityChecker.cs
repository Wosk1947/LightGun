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

    public class ValueLegalityChecker
    {
        public static bool CheckInt(string methodName, int toCheckValue,int minValue, int maxValue=Int32.MaxValue)
        {
            if (toCheckValue < minValue || toCheckValue > maxValue)
            {
                ARDebug.LogWarning("{0}: value is {1}, while legal min value is {2}, max value is {3}",
                    methodName,toCheckValue,minValue,maxValue);
                return false;
            }
            return true;
        }

    }
}
