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
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantAssignment
// ReSharper disable NotAccessedVariable
// ReSharper disable RedundantUsingDirective
// ReSharper disable Unity.NoNullPropagation
// ReSharper disable InconsistentNaming

#pragma warning disable CS0472

using Gs2.Unity.Gs2SerialKey.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2SerialKey.Context
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/SerialKey/User/Context/Gs2SerialKeyUserContext")]
    public partial class Gs2SerialKeyUserContext : MonoBehaviour
    {
        public void Start() {
            if (User == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: User is not set in Gs2SerialKeyUserContext.");
            }
        }

        public bool HasError() {
            if (User == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2SerialKeyUserContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2SerialKeyUserContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2SerialKeyUserContext
    {
        public User User;

        public void SetUser(User User) {
            this.User = User;
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2SerialKeyUserContext
    {

    }
}