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
    /**
     * \if english
     * @brief Selects the behavior of the lighting estimation subsystem.
     * \else
     * @brief 指定光照估计的行为。
     * \endif
     */
    public enum ARConfigLightingMode
    {
        /**
         * \if english
         * @brief  LightEstimate is disabled.
         * \else
         * @brief 关闭光线估计。
         * \endif
         */
        DISABLED=0,
        /**
         * \if english
         * @brief  LightEstimate is enabled.
         * \else
         * @brief 启动光线估计。
         * \endif
         */
        AMBIENT_INTENSITY = 1,
    }
}
