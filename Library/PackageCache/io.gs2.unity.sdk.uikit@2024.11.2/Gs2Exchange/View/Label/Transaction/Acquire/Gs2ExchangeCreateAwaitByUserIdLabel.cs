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
using Gs2.Gs2Exchange.Request;
using Gs2.Unity.Gs2Exchange.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Exchange.Context;
using Gs2.Unity.UiKit.Gs2Exchange.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Exchange.Label
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Exchange/Await/View/Label/Transaction/Gs2ExchangeCreateAwaitByUserIdLabel")]
    public partial class Gs2ExchangeCreateAwaitByUserIdLabel : MonoBehaviour
    {
        private void OnFetched()
        {
            if ((!this._fetcher?.Fetched ?? false) || this._fetcher.Request == null) {
                return;
            }
            if ((!this._userDataFetcher?.Fetched ?? false) || this._userDataFetcher.Await == null) {
                return;
            }
            this.onUpdate?.Invoke(
                this.format.Replace(
                    "{namespaceName}",
                    $"{this._fetcher.Request.NamespaceName}"
                ).Replace(
                    "{userId}",
                    $"{this._fetcher.Request.UserId}"
                ).Replace(
                    "{rateName}",
                    $"{this._fetcher.Request.RateName}"
                ).Replace(
                    "{count}",
                    $"{this._fetcher.Request.Count}"
                ).Replace(
                    "{config}",
                    $"{this._fetcher.Request.Config}"
                ).Replace(
                    "{timeOffsetToken}",
                    $"{this._fetcher.Request.TimeOffsetToken}"
                ).Replace(
                    "{userData:userId}",
                    $"{this._userDataFetcher.Await.UserId}"
                ).Replace(
                    "{userData:rateName}",
                    $"{this._userDataFetcher.Await.RateName}"
                ).Replace(
                    "{userData:name}",
                    $"{this._userDataFetcher.Await.Name}"
                ).Replace(
                    "{userData:skipSeconds}",
                    $"{this._userDataFetcher.Await.SkipSeconds}"
                ).Replace(
                    "{userData:config}",
                    $"{this._userDataFetcher.Await.Config}"
                ).Replace(
                    "{userData:exchangedAt}",
                    $"{this._userDataFetcher.Await.ExchangedAt}"
                ).Replace(
                    "{userData:acquirableAt}",
                    $"{this._userDataFetcher.Await.AcquirableAt}"
                )
            );
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ExchangeCreateAwaitByUserIdLabel
    {
        private Gs2ExchangeCreateAwaitByUserIdFetcher _fetcher;
        private Gs2ExchangeOwnAwaitFetcher _userDataFetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2ExchangeCreateAwaitByUserIdFetcher>() ?? GetComponentInParent<Gs2ExchangeCreateAwaitByUserIdFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ExchangeCreateAwaitByUserIdFetcher.");
                enabled = false;
            }
            this._userDataFetcher = GetComponent<Gs2ExchangeOwnAwaitFetcher>() ?? GetComponentInParent<Gs2ExchangeOwnAwaitFetcher>();
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2ExchangeCreateAwaitByUserIdFetcher>() ?? GetComponentInParent<Gs2ExchangeCreateAwaitByUserIdFetcher>(true);
            if (this._fetcher == null) {
                return true;
            }
            return false;
        }

        private UnityAction _onFetched;

        public void OnEnable()
        {
            this._onFetched = () =>
            {
                OnFetched();
            };
            this._fetcher.OnFetched.AddListener(this._onFetched);
            if (this._fetcher.Fetched) {
                OnFetched();
            }
            if (this._userDataFetcher != null) {
                this._userDataFetcher.OnFetched.AddListener(this._onFetched);
                if (this._userDataFetcher.Fetched) {
                    OnFetched();
                }
            }
        }

        public void OnDisable()
        {
            if (this._onFetched != null) {
                this._fetcher.OnFetched.RemoveListener(this._onFetched);
                if (this._userDataFetcher != null) {
                    this._userDataFetcher.OnFetched.RemoveListener(this._onFetched);
                }
                this._onFetched = null;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ExchangeCreateAwaitByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ExchangeCreateAwaitByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExchangeCreateAwaitByUserIdLabel
    {
        [Serializable]
        private class UpdateEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private UpdateEvent onUpdate = new UpdateEvent();

        public event UnityAction<string> OnUpdate
        {
            add => this.onUpdate.AddListener(value);
            remove => this.onUpdate.RemoveListener(value);
        }
    }
}