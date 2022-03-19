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
     * Align state of the \link share map \endlink of WorldAR.
     * \else
     * WorldAR中\link 共享地图 \endlink 的对齐状态。
     * \endif
     */
    public enum ARAlignState
    {
        /**
         * \if english
         * No share map is aligning.
         * \else
         * 当前没有启动共享地图的对齐功能。
         * \endif
         */
        NONE = 0,
        /**
         * \if english
         * Align share map failed.
         * \else
         * 共享地图对齐失败。
         * \endif
         */
        FAILED = 1,
        /**
         * \if english
         * Aligning share map is in processing.
         * \else
         * 共享地图的正在对齐中。
         * \endif
         */
        PROCESSING = 2,
        /**
         * \if english
         * Align share map successfully.
         * \else
         * 共享地图对齐成功。
         * \endif
         */
        SUCCESS = 3
    }
}
///@endcond