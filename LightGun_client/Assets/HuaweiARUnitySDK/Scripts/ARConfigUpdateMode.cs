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
     * @brief Selects the behavior of  ARSession.Update().
     * \else
     * @brief 设置 ARSession.Update() 的行为。
     * \endif
     */
    public enum ARConfigUpdateMode
    {
        /**
         * \if english
         * @brief  ARSession.Update() will wait until a new camera image is available.
         * <b>Note: If the camera image does not arrive by the built-in timeout(66ms), then ARSession.Update() will return
         * the most recent \link ARFrame \endlink instead.</b>
         * \else
         * @brief  ARSession.Update() 将会阻塞至下一帧预览可用。<b>注意：如果预览超时（66ms）， ARSession.Update()
         * 将会返回最近的一次 ARFrame </b>。
         * \endif
         */
        BLOCKING = 0,

        /**
         * \if english
         * @brief  ARSession.Update() will return immediately without blocking.
         * If no new camera image is available, then ARSession.Update() will return
         * the most recent ARFrame instead.
         * \else
         * @brief  ARSession.Update() 立刻返回。如果没有更新的帧可用， ARSession.Update()
         * 将会返回最近的一次 ARFrame 。
         * \endif
         */
        LATEST_CAMERA_IMAGE = 1,
    }
}
