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
 *
 * deny overwrite
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

using Gs2.Unity.Gs2Experience.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Experience.Context
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Experience/Status/Context/Gs2ExperienceOwnStatusContext")]
    public partial class Gs2ExperienceOwnStatusContext : MonoBehaviour
    {
        public void Start() {
            if (Status == null) {
                Debug.LogWarning($"{gameObject.GetFullPath()}: Status is not set in Gs2ExperienceOwnStatusContext.");
            }
        }
        public virtual bool HasError() {
            if (Status == null) {
                if (GetComponentInParent<Gs2ExperienceOwnStatusList>(true) != null) {
                    return false;
                }
                else if (GetComponentInParent<Gs2ExperienceConvertInventoryItemSetToOwnStatus>(true) != null) {
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

    public partial class Gs2ExperienceOwnStatusContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ExperienceOwnStatusContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ExperienceOwnStatusContext
    {
        [SerializeField]
        private OwnStatus _status;
        public OwnStatus Status
        {
            get => _status;
            set => SetOwnStatus(value);
        }

        public void SetOwnStatus(OwnStatus status) {
            this._status = status;

            this.OnUpdate.Invoke();
        }

        public UnityEvent OnUpdate = new UnityEvent();
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExperienceOwnStatusContext
    {

    }
}