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

using Gs2.Unity.Gs2Gateway.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Gateway.Context
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Gateway/FirebaseToken/Context/Gs2GatewayOwnFirebaseTokenContext")]
    public partial class Gs2GatewayOwnFirebaseTokenContext : MonoBehaviour
    {
        public void Start() {
            if (FirebaseToken == null) {
                Debug.LogWarning($"{gameObject.GetFullPath()}: FirebaseToken is not set in Gs2GatewayOwnFirebaseTokenContext.");
            }
        }
        public virtual bool HasError() {
            if (FirebaseToken == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2GatewayOwnFirebaseTokenContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2GatewayOwnFirebaseTokenContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2GatewayOwnFirebaseTokenContext
    {
        [SerializeField]
        private OwnFirebaseToken _firebaseToken;
        public OwnFirebaseToken FirebaseToken
        {
            get => _firebaseToken;
            set => SetOwnFirebaseToken(value);
        }

        public void SetOwnFirebaseToken(OwnFirebaseToken firebaseToken) {
            if (firebaseToken == null) return;
            this._firebaseToken = firebaseToken;

            this.OnUpdate.Invoke();
        }

        public UnityEvent OnUpdate = new UnityEvent();
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2GatewayOwnFirebaseTokenContext
    {

    }
}