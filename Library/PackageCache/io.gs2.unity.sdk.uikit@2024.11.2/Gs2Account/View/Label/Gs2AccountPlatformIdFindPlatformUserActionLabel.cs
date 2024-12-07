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

using System;
using Gs2.Core.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Account.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Account
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Account/PlatformId/View/Label/Gs2AccountPlatformIdFindPlatformUserActionLabel")]
    public partial class Gs2AccountPlatformIdFindPlatformUserActionLabel : MonoBehaviour
    {
        private void OnChange()
        {
            this.onUpdate?.Invoke(
                this.format
            );
        }

        public void Awake() {
            if (this.action == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2AccountAccountAuthenticationAction.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            if (this.action == null) {
                return true;
            }
            return false;
        }

        public void OnEnable() {
            this.action.OnChange.AddListener(OnChange);
        }

        public void OnDisable() {
            this.action.OnChange.RemoveListener(OnChange);
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2AccountPlatformIdFindPlatformUserActionLabel
    {
        
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2AccountPlatformIdFindPlatformUserActionLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2AccountPlatformIdFindPlatformUserActionLabel
    {
        public Gs2AccountPlatformIdFindPlatformUserAction action;
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2AccountPlatformIdFindPlatformUserActionLabel
    {
        [Serializable]
        private class UpdateEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private UpdateEvent onUpdate = new UpdateEvent();

        public event UnityAction<string> OnUpdate
        {
            add => onUpdate.AddListener(value);
            remove => onUpdate.RemoveListener(value);
        }
    }
}