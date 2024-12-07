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

using Gs2.Unity.Util;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Account
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Account/Account/View/Enabler/Gs2AccountLoggedInEnabler")]
    public partial class Gs2AccountLoggedInEnabler : MonoBehaviour
    {
        private void OnLogin()
        {
            if (this._sessionHolder.GameSession == null) {
                this.target.SetActive(!this.loggedIn);
            }
            else {
                this.target.SetActive(this.loggedIn);
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2AccountLoggedInEnabler
    {
        private Gs2GameSessionHolder _sessionHolder;

        public void Awake()
        {
            this._sessionHolder = Gs2GameSessionHolder.Instance;
            this.target.SetActive(!this.loggedIn);
        }
        

        private UnityAction _onLogin;

        public void OnEnable()
        {
            this._onLogin = () =>
            {
                OnLogin();
            };
            this._sessionHolder.OnLogin.AddListener(this._onLogin);

            if (this._sessionHolder.Initialized) {
                OnLogin();
            }
        }

        public void OnDisable()
        {
            if (this._onLogin != null) {
                this._sessionHolder.OnLogin.RemoveListener(this._onLogin);
                this._onLogin = null;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2AccountLoggedInEnabler
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2AccountLoggedInEnabler
    {
        public bool loggedIn;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2AccountLoggedInEnabler
    {
        
    }
}