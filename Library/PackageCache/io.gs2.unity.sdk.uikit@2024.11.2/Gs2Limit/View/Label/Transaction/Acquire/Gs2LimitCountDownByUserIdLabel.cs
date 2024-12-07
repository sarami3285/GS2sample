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
using Gs2.Gs2Limit.Request;
using Gs2.Unity.Gs2Limit.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Limit.Context;
using Gs2.Unity.UiKit.Gs2Limit.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Limit.Label
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Limit/Counter/View/Label/Transaction/Gs2LimitCountDownByUserIdLabel")]
    public partial class Gs2LimitCountDownByUserIdLabel : MonoBehaviour
    {
        private void OnFetched()
        {
            if (this._userDataFetcher == null) {
                var context = gameObject.AddComponent<Gs2LimitOwnCounterContext>();
                context.SetOwnCounter(
                    OwnCounter.New(
                        Namespace.New(
                            this._fetcher.Request.NamespaceName
                        ),
                        this._fetcher.Request.LimitName,
                        this._fetcher.Request.CounterName
                    )
                );
                this._userDataFetcher = gameObject.AddComponent<Gs2LimitOwnCounterFetcher>();
                this._userDataFetcher.OnFetched.AddListener(this._onFetched);
            }
            if ((!this._fetcher?.Fetched ?? false) || this._fetcher.Request == null) {
                return;
            }
            if ((!this._userDataFetcher?.Fetched ?? false) || this._userDataFetcher.Counter == null) {
                return;
            }
            this.onUpdate?.Invoke(
                this.format.Replace(
                    "{namespaceName}",
                    $"{this._fetcher.Request.NamespaceName}"
                ).Replace(
                    "{limitName}",
                    $"{this._fetcher.Request.LimitName}"
                ).Replace(
                    "{counterName}",
                    $"{this._fetcher.Request.CounterName}"
                ).Replace(
                    "{userId}",
                    $"{this._fetcher.Request.UserId}"
                ).Replace(
                    "{countDownValue}",
                    $"{this._fetcher.Request.CountDownValue}"
                ).Replace(
                    "{timeOffsetToken}",
                    $"{this._fetcher.Request.TimeOffsetToken}"
                ).Replace(
                    "{userData:counterId}",
                    $"{this._userDataFetcher.Counter.CounterId}"
                ).Replace(
                    "{userData:limitName}",
                    $"{this._userDataFetcher.Counter.LimitName}"
                ).Replace(
                    "{userData:name}",
                    $"{this._userDataFetcher.Counter.Name}"
                ).Replace(
                    "{userData:count}",
                    $"{this._userDataFetcher.Counter.Count}"
                ).Replace(
                    "{userData:count:changed}",
                    $"{this._userDataFetcher.Counter.Count + this._fetcher.Request.CountDownValue}"
                ).Replace(
                    "{userData:createdAt}",
                    $"{this._userDataFetcher.Counter.CreatedAt}"
                ).Replace(
                    "{userData:updatedAt}",
                    $"{this._userDataFetcher.Counter.UpdatedAt}"
                )
            );
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2LimitCountDownByUserIdLabel
    {
        private Gs2LimitCountDownByUserIdFetcher _fetcher;
        private Gs2LimitOwnCounterFetcher _userDataFetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2LimitCountDownByUserIdFetcher>() ?? GetComponentInParent<Gs2LimitCountDownByUserIdFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2LimitCountDownByUserIdFetcher.");
                enabled = false;
            }
            this._userDataFetcher = GetComponent<Gs2LimitOwnCounterFetcher>() ?? GetComponentInParent<Gs2LimitOwnCounterFetcher>();
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2LimitCountDownByUserIdFetcher>() ?? GetComponentInParent<Gs2LimitCountDownByUserIdFetcher>(true);
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

    public partial class Gs2LimitCountDownByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2LimitCountDownByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LimitCountDownByUserIdLabel
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