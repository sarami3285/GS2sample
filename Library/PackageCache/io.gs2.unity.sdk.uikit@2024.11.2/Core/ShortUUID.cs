/*
 * Copyright 2016 Game Server Services, Inc. or its affiliates. All Rights
 * Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * or in the "license" file accompanying this file. This file is distributed
 * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 */
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CheckNamespace

using System;

namespace Gs2.Unity.UiKit.Core
{
    public class ShortUuid
    {
        private readonly string _value;
        
        public ShortUuid(string shortUuid)
        {
            _value = shortUuid.Replace("-", "");
        }

        public override string ToString()
        {
            return _value.Substring(0, 6) + "-" + 
                   _value.Substring(6, 5) + "-" + 
                   _value.Substring(11, 6) + "-" + 
                   _value.Substring(17, 5);
        }

        public static ShortUuid ParseUuid(string uuid)
        {
            return new ShortUuid(Convert.ToBase64String(Guid.Parse(uuid).ToByteArray()).Replace("==", ""));
        }

        public string ToUuid()
        {
            return new Guid(Convert.FromBase64String(this._value + "==")).ToString();
        }
    }
}