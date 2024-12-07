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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gs2.Core.Exception;
using Gs2.Gs2Money.Request;
using Gs2.Unity.Core.Exception;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Core.Consume;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Money.Fetcher;
using Gs2.Unity.Util;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Core
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Core/ConsumeAction/View/Gs2CoreConsumeActionMoneyWalletEnabler")]
    public partial class Gs2MoneyWithdrawByUserIdBalanceSatisfyEnabler : MonoBehaviour
    {
        public IEnumerator Process() {
            while (true) {
                yield return new WaitForSeconds(0.1f);
                
                if (_fetcher.Fetched && _fetcher.Request != null) {
                    var future = this._clientHolder.Gs2.Money.Namespace(
                        _fetcher.Request.NamespaceName
                    ).Me(
                        this._sessionHolder.GameSession
                    ).Wallet(
                        _fetcher.Request.Slot ?? 0
                    ).ModelFuture();
                    yield return future;
                    if (future.Error != null) {
                        this.onError.Invoke(new CanIgnoreException(future.Error), null);
                    }

                    var wallet = future.Result;
                    
                    if (wallet == null) continue;

                    if ((_fetcher.Request.PaidOnly ?? false) == false && wallet.Free + wallet.Paid >= _fetcher.Request.Count) {
                        target.SetActive(this.satisfy);
                    }
                    else if ((_fetcher.Request.PaidOnly ?? false) == true && wallet.Paid >= _fetcher.Request.Count) {
                        target.SetActive(this.satisfy);
                    }
                    else {
                        target.SetActive(this.notSatisfy);
                    }
                }
                else
                {
                    target.SetActive(false);
                }
            }
        }

        public void OnEnable() {
            Gs2ClientHolder.Instance.StartCoroutine(Process());
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2MoneyWithdrawByUserIdBalanceSatisfyEnabler
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _sessionHolder;
        private Gs2MoneyWithdrawByUserIdFetcher _fetcher;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _sessionHolder = Gs2GameSessionHolder.Instance;
            _fetcher = GetComponentInParent<Gs2MoneyWithdrawByUserIdFetcher>();
            
            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MoneyWithdrawByUserIdFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MoneyWithdrawByUserIdBalanceSatisfyEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MoneyWithdrawByUserIdBalanceSatisfyEnabler
    {
        public bool satisfy;
        public bool notSatisfy;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MoneyWithdrawByUserIdBalanceSatisfyEnabler
    {

        [SerializeField]
        internal ErrorEvent onError = new ErrorEvent();

        public event UnityAction<Gs2Exception, Func<IEnumerator>> OnError
        {
            add => this.onError.AddListener(value);
            remove => this.onError.RemoveListener(value);
        }
    }
}