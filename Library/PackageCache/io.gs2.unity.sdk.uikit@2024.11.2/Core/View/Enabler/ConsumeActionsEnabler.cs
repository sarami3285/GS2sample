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
using System.Threading;
using Gs2.Core.Exception;
using Gs2.Unity.Core.Exception;
using Gs2.Unity.Core.Model;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.Util;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Core.Consume.Enabler
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Core/View/Enabler/ConsumeActionsEnabler")]
    public partial class ConsumeActionsEnabler : MonoBehaviour
    {
        private IEnumerator Fetch() {
            var ready = new []{false};
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);
            
            var ok = true;
            foreach (var consumeAction in this._fetcher.ConsumeActions()) {
                var future = consumeAction.Satisfy(
                    clientHolder.Gs2,
                    gameSessionHolder.GameSession,
                    () =>
                    {
                        if (ready[0]) {
                            StartCoroutine(nameof(Fetch));
                        }
                    }
                );
                yield return future;
                if (future.Error != null) {
                    this.onError.Invoke(new CanIgnoreException(future.Error), null);
                }
                if (!future.Result) {
                    ok = false;
                    break;
                }
            }
            if (ok) {
                this.target.SetActive(this.satisfy);
            }
            else {
                this.target.SetActive(this.notSatisfy);
            }
            ready[0] = true;
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class ConsumeActionsEnabler
    {
        private IConsumeActionsFetcher _fetcher;
        
        public void Awake()
        {
            this._fetcher = GetComponent<IConsumeActionsFetcher>() ?? GetComponentInParent<IConsumeActionsFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the IConsumeActionsFetcher.");
                enabled = false;
            }
            if (this.target == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: target is not set.");
                enabled = false;
            }
        }
        
        public bool HasError()
        {
            this._fetcher = GetComponent<IConsumeActionsFetcher>() ?? GetComponentInParent<IConsumeActionsFetcher>(true);
            if (this._fetcher == null) {
                return true;
            }
            if (this.target == null) {
                return true;
            }
            return false;
        }

        private UnityAction _onFetched;

        public void OnEnable() {
            this._onFetched = () =>
            {
                if (gameObject.activeInHierarchy) {
                    StartCoroutine(nameof(Fetch));
                }
            };
            this._fetcher.OnFetchedEvent().AddListener(this._onFetched);
            if (this._fetcher.IsFetched()) {
                this._onFetched();
            }
        }

        public void OnDisable()
        {
            if (this._onFetched != null) {
                this._fetcher.OnFetchedEvent().RemoveListener(this._onFetched);
                this._onFetched = null;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class ConsumeActionsEnabler
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class ConsumeActionsEnabler
    {
        public bool notSatisfy;
        public bool satisfy;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class ConsumeActionsEnabler
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
