// Copyright (C) 2019. Huawei Technologies Co., Ltd. All rights reserved.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the Apache License, Version 2.0.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// Apache License for more details.

///@cond ContainShareAR
namespace HuaweiARUnitySDK
{
    /**
     * \if english
     * @brief State of world map in current frame.
     * \else
     * @brief 当前帧的世界地图的状态。
     * \endif
     */
    public enum ARWorldMappingState
    {
        /**
         * \if english
         * Current map has not been initialized, not available.
         * \else
         * 当前地图还未被初始化，不可用。
         * \endif
         */
        NOT_AVAILABLE = -1,

        /**
         * \if english
         * Current map is limited, shoul not be used.
         * \else
         * 当前地图受限，不应该被使用。
         * \endif
         */
        LIMITED = 0,

        /**
         * \if english
         * The nearby map has alredy been built. It's extending to the filed of vision.
         * This state indicates the map can be used.
         * \else
         * 附近的地图已经建立，正在扩展到当前视野。当前地图可以使用。
         * \endif
         */
        EXTENDING = 1,

        /**
         * \if english
         * The map has alredy been built in the filed of vision. This state indicates the map can be used.
         * \else
         * 当前视野的地图已经建立，可以被使用。
         * \endif
         */
        MAPPED = 2
    }
}
///@endcond