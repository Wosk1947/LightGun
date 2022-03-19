/*
 * 2019.09.19 - Huawei modifies and matches AREngine.
 * Copyright (C) 2019. Huawei Technologies Co., Ltd. All rights reserved.
 */

//-----------------------------------------------------------------------
// <copyright file="RequiredOptionalPreprocessBuild.cs" company="Google">
//
// Copyright 2018 Google LLC. All Rights Reserved.
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
    using UnityEditor.Build;
    using UnityEditor;
    using UnityEngine;
    internal class RequiredOptinalSelectPreBuild : IPreprocessBuild
    {
        public int callbackOrder
        {
            get
            {
                return 0;
            }
        }

        public void OnPreprocessBuild(BuildTarget target, string path)
        {
            bool isHuaweiARReruired = HuaweiARProjectSettings.Instance.IsHuaweiARRequired;

            Debug.LogFormat("Building application with {0} HuaweiAR support", isHuaweiARReruired ? "Required" : "Optional");
            PluginHelper.GetImporterByPluginName("HUAWEI AR Engine Plugin_Required.aar")
                .SetCompatibleWithPlatform(BuildTarget.Android, isHuaweiARReruired);
            PluginHelper.GetImporterByPluginName("HUAWEI AR Engine Plugin_Optional.aar")
                .SetCompatibleWithPlatform(BuildTarget.Android, !isHuaweiARReruired);
        }
    }
}
