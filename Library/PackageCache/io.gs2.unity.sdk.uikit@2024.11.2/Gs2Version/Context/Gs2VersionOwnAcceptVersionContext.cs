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

using Gs2.Unity.Gs2Version.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Version.Context
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Version/AcceptVersion/Context/Gs2VersionOwnAcceptVersionContext")]
    public partial class Gs2VersionOwnAcceptVersionContext : MonoBehaviour
    {
        public void Start() {
            if (AcceptVersion == null) {
                Debug.LogWarning($"{gameObject.GetFullPath()}: AcceptVersion is not set in Gs2VersionOwnAcceptVersionContext.");
            }
        }
        public virtual bool HasError() {
            if (AcceptVersion == null) {
                if (GetComponentInParent<Gs2VersionOwnAcceptVersionList>(true) != null) {
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

    public partial class Gs2VersionOwnAcceptVersionContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2VersionOwnAcceptVersionContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2VersionOwnAcceptVersionContext
    {
        [SerializeField]
        private OwnAcceptVersion _acceptVersion;
        public OwnAcceptVersion AcceptVersion
        {
            get => _acceptVersion;
            set => SetOwnAcceptVersion(value);
        }

        public void SetOwnAcceptVersion(OwnAcceptVersion acceptVersion) {
            if (acceptVersion == null) return;
            this._acceptVersion = acceptVersion;

            this.OnUpdate.Invoke();
        }

        public UnityEvent OnUpdate = new UnityEvent();
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2VersionOwnAcceptVersionContext
    {

    }
}