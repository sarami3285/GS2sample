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

using Gs2.Unity.Gs2Friend.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Friend.Context
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Friend/PublicProfile/Context/Gs2FriendOwnPublicProfileContext")]
    public partial class Gs2FriendOwnPublicProfileContext : MonoBehaviour
    {
        public void Start() {
            if (PublicProfile == null) {
                Debug.LogWarning($"{gameObject.GetFullPath()}: PublicProfile is not set in Gs2FriendOwnPublicProfileContext.");
            }
        }
        public virtual bool HasError() {
            if (PublicProfile == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2FriendOwnPublicProfileContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FriendOwnPublicProfileContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2FriendOwnPublicProfileContext
    {
        [SerializeField]
        private OwnPublicProfile _publicProfile;
        public OwnPublicProfile PublicProfile
        {
            get => _publicProfile;
            set => SetOwnPublicProfile(value);
        }

        public void SetOwnPublicProfile(OwnPublicProfile publicProfile) {
            if (publicProfile == null) return;
            this._publicProfile = publicProfile;

            this.OnUpdate.Invoke();
        }

        public UnityEvent OnUpdate = new UnityEvent();
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FriendOwnPublicProfileContext
    {

    }
}