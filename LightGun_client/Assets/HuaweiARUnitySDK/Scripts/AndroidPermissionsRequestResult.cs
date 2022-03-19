/*
 * 2019.09.19 - Huawei modifies and matches AREngine.
 * Copyright (C) 2019. Huawei Technologies Co., Ltd. All rights reserved.
 */
//-----------------------------------------------------------------------
// <copyright file="AndroidPermissionsRequestResult.cs" company="Google">
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

namespace HuaweiARUnitySDK
{
    /**
    * \if english
    * @brief The results of \link AndroidPermissionsRequest.RequestPermission() \endlink permission request.
    * \else
    * @brief \link AndroidPermissionsRequest.RequestPermission() \endlink权限请求的结果。
    * \endif
    */
    public class AndroidPermissionsRequestResult
    {

        /**
         * \if english
         * @brief The result of a permission request.
         * \else
         * @brief 权限请求的结果。
         * \endif
         */
        [System.Serializable]
        public class PermissionResult
        {
            /**
             * \if english
             * The name of permission.
             * \else
             * 权限名。
             * \endif
             */
            public string permissionName;

            /**
             * \if english
             * \c 1 indicates  \link permissionName \endlink is granted. \c 0 indicates permissionName is denied.
             * \else
             * \link permissionName \endlink 是否被授权。值为\c 1，则表明被授权，否则，不被授权。
             * \endif
             */
            public int granted;
        }
        /**
         * \if english
         * The result array of permission request.
         * \else
         * 权限请求的结果数组。
         * \endif
         */
        public  PermissionResult[] Results
        {
            get;private set;
        }
        /**
         * \if english
         * Whether all permissions in \link Results \endlink are granted.
         * \else
         * \link Results \endlink中权限是否全部被授权。
         * \endif
         */
        public bool IsAllGranted
        {
            get
            {
                if (Results == null)
                {
                    return false;
                }

                for (int i = 0; i < Results.Length; i++)
                {
                    if (0 == Results[i].granted)
                    {
                        return false;
                    }
                }

                return true;
            }
        }
        ///@cond EXCLUDE_FROM_DOXYGEN
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="permissionResults"></param>
        public AndroidPermissionsRequestResult(PermissionResult[] permissionResults)
        {
            Results = permissionResults;
        }
        ///@endcond
    }
}
