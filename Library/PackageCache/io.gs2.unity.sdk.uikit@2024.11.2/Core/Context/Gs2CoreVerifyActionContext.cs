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

using Gs2.Unity.Core.Model;
using Gs2.Unity.UiKit.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Core.Context
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Core/Context/Gs2CoreVerifyActionContext")]
    public partial class Gs2CoreVerifyActionContext : MonoBehaviour
    {
        public void Start() {
            if (VerifyAction == null) {
                Debug.LogWarning($"{gameObject.GetFullPath()}: EzVerifyAction is not set in Gs2CoreVerifyActionContext.");
            }
        }

        public virtual bool HasError() {
            if (VerifyAction == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2CoreVerifyActionContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2CoreVerifyActionContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2CoreVerifyActionContext
    {
        [SerializeField]
        private EzVerifyAction _consumeAction;
        public EzVerifyAction VerifyAction
        {
            get => _consumeAction;
            set => SetVerifyAction(value);
        }

        public void SetVerifyAction(EzVerifyAction consumeAction) {
            if (consumeAction == null) return;

            this._consumeAction = consumeAction;

            this.OnUpdate.Invoke();
        }

        public UnityEvent OnUpdate = new UnityEvent();
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2CoreVerifyActionContext
    {

    }
}