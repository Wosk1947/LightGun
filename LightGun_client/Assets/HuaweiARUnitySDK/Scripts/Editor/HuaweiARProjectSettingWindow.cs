/*
 * 2019.09.19 - Huawei modifies and matches AREngine.
 * Copyright (C) 2019. Huawei Technologies Co., Ltd. All rights reserved.
 */

//-----------------------------------------------------------------------
// <copyright file="ARCoreProjectSettingsWindow.cs" company="Google">
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

namespace HuaweiARInternal
{
    using UnityEditor;
    using UnityEngine;
    internal class HuaweiARProjectSettingWindow : EditorWindow
    {
        [MenuItem("Edit/Project Settings/HuaweiAR")]
        private static void ShowProjectSettingsWindow()
        {
            HuaweiARProjectSettings.Instance.LoadSettings();

            Rect rect = new Rect(500, 300, 400, 150);
            HuaweiARProjectSettingWindow settingWindow = GetWindowWithRect<HuaweiARProjectSettingWindow>(
                rect);
            settingWindow.titleContent = new GUIContent("Huawei AR");
            settingWindow.Show();
        }

        private void OnGUI()
        {
            GUILayout.BeginVertical();
            GUILayout.Space(10);
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.alignment = TextAnchor.MiddleCenter;
            style.stretchWidth = true;
            style.fontSize = 20;
            style.fixedHeight = 20;
            EditorGUILayout.LabelField("HuaweiAR Project Settings", style);
            GUILayout.Space(10);
            HuaweiARProjectSettings.Instance.IsHuaweiARRequired =
                EditorGUILayout.Toggle("Huawei AR Required", HuaweiARProjectSettings.Instance.IsHuaweiARRequired);
            GUILayout.Space(10);

            if (GUI.changed)
            {
                HuaweiARProjectSettings.Instance.SaveSettings();
            }

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Close", GUILayout.Width(60), GUILayout.Height(20)))
            {
                Close();
            }

            EditorGUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }
    }
}