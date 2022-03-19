/*
 * 2019.09.19 - Huawei modifies and matches AREngine.
 * Copyright (C) 2019. Huawei Technologies Co., Ltd. All rights reserved.
 */
//-----------------------------------------------------------------------
// <copyright file="ARCoreProjectSettings.cs" company="Google">
//
// Copyright 2017 Google LLC. All Rights Reserved.
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

namespace HuaweiARInternal {
    using UnityEngine;
    using System.IO;
    public class HuaweiARProjectSettings
    {
        public static HuaweiARProjectSettings Instance { get; private set; }
        public bool IsHuaweiARRequired;
        private const string c_ProjectSettingsPath = "ProjectSettings/HuaweiARProjectSettings.json";

        static HuaweiARProjectSettings()
        {
            if (Application.isEditor)
            {
                Instance = new HuaweiARProjectSettings();
                Instance.LoadSettings();
            }
            else
            {
                Instance = null;
                Debug.LogError("Cannot access HuaweiARProjectSettings outside of Unity Editor");
            }
        }

        public void LoadSettings()
        {
            IsHuaweiARRequired = true;

            if(File.Exists(c_ProjectSettingsPath))
            {
                HuaweiARProjectSettings settings = JsonUtility.FromJson<HuaweiARProjectSettings>(
                    File.ReadAllText(c_ProjectSettingsPath));
                IsHuaweiARRequired = settings.IsHuaweiARRequired;
            }
        }

        public void SaveSettings()
        {
            File.WriteAllText(c_ProjectSettingsPath, JsonUtility.ToJson(this));
        }
    }
}
