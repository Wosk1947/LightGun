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

    public enum NDKARTrackableType
    {
        Invalid = 0,
        BaseTrackable = 0x41520100,
        Plane = 0x41520101,
        Point = 0x41520102, //103???
        AugmentedImage = 0x41520104,

        Hand = 0x50000000,
        Body = 0x50000001,
        Face = 0x50000002,
    }
}
