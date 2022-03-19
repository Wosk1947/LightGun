/*
 * 2019.09.19 - Huawei modifies and matches AREngine.
 * Copyright (C) 2019. Huawei Technologies Co., Ltd. All rights reserved.
 */

//-----------------------------------------------------------------------
// <copyright file="AssetHelper.cs" company="Google">
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
    using UnityEditor;
    using System.IO;
    using UnityEditor.Build;
    class PluginHelper
    {
        public static PluginImporter GetImporterByPluginName(string name)
        {
            string[] guids = AssetDatabase.FindAssets(Path.GetFileNameWithoutExtension(name));

            PluginImporter importer = null;
            int count = 0;
            foreach(string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                if (Path.GetFileName(path) == name)
                {
                    importer = AssetImporter.GetAtPath(path) as PluginImporter;
                    count++;
                }
            }

            if(0==count)
            {
                throw new BuildFailedException(string.Format("plugin {0} is not found!",name));
            }
            else if (count>1)
            {
                throw new BuildFailedException(string.Format("multiple plugin {0} are found",name));
            }
            else if(null==importer)
            {
                throw new BuildFailedException(string.Format("{0} is found, however it is not a plugin.",name));
            }
            return importer;
        }
    }
}
