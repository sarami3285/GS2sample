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

using Gs2.Unity.Gs2Buff.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Buff.Context
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Buff/BuffEntryModel/Context/Gs2BuffBuffEntryModelContext")]
    public partial class Gs2BuffBuffEntryModelContext : MonoBehaviour
    {
        public void Start() {
            if (BuffEntryModel == null) {
                Debug.LogWarning($"{gameObject.GetFullPath()}: BuffEntryModel is not set in Gs2BuffBuffEntryModelContext.");
            }
        }

        public virtual bool HasError() {
            if (BuffEntryModel == null) {
                if (GetComponentInParent<Gs2BuffBuffEntryModelList>(true) != null) {
                    return false;
                }
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2BuffBuffEntryModelContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2BuffBuffEntryModelContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2BuffBuffEntryModelContext
    {
        [SerializeField]
        private BuffEntryModel _buffEntryModel;
        public BuffEntryModel BuffEntryModel
        {
            get => _buffEntryModel;
            set => SetBuffEntryModel(value);
        }

        public void SetBuffEntryModel(BuffEntryModel buffEntryModel) {
            if (buffEntryModel == null) return;

            this._buffEntryModel = buffEntryModel;

            this.OnUpdate.Invoke();
        }

        public UnityEvent OnUpdate = new UnityEvent();
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2BuffBuffEntryModelContext
    {

    }
}