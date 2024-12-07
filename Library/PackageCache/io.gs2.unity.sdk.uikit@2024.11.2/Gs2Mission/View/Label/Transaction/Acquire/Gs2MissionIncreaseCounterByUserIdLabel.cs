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

using System;
using System.Linq;
using Gs2.Gs2Mission.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Mission.Label
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Mission/Counter/View/Label/Transaction/Gs2MissionIncreaseCounterByUserIdLabel")]
    public partial class Gs2MissionIncreaseCounterByUserIdLabel : MonoBehaviour
    {
        private void OnFetched()
        {
            if ((!this._fetcher?.Fetched ?? false) || this._fetcher.Request == null) {
                return;
            }
            if (this._userDataFetcher?.Fetched ?? false)
            {
                this.onUpdate?.Invoke(
                    this.format.Replace(
                        "{namespaceName}",
                        $"{this._fetcher.Request.NamespaceName}"
                    ).Replace(
                        "{counterName}",
                        $"{this._fetcher.Request.CounterName}"
                    ).Replace(
                        "{userId}",
                        $"{this._fetcher.Request.UserId}"
                    ).Replace(
                        "{value}",
                        $"{this._fetcher.Request.Value}"
                    ).Replace(
                        "{userData:name}",
                        $"{this._userDataFetcher.Counter.Name}"
                    ).Replace(
                        "{userData:values:notReset}",
                        $"{_userDataFetcher.Counter.Values.FirstOrDefault(v => v.ResetType == "notReset")?.Value}"
                    ).Replace(
                        "{userData:values:daily}",
                        $"{_userDataFetcher.Counter.Values.FirstOrDefault(v => v.ResetType == "daily")?.Value}"
                    ).Replace(
                        "{userData:values:weekly}",
                        $"{_userDataFetcher.Counter.Values.FirstOrDefault(v => v.ResetType == "weekly")?.Value}"
                    ).Replace(
                        "{userData:values:monthly}",
                        $"{_userDataFetcher.Counter.Values.FirstOrDefault(v => v.ResetType == "monthly")?.Value}"
                    ).Replace(
                        "{userData:values:notReset:changed}",
                        $"{(_userDataFetcher.Counter.Values.FirstOrDefault(v => v.ResetType == "notReset")?.Value ?? 0) + this._fetcher.Request.Value}"
                    ).Replace(
                        "{userData:values:daily:changed}",
                        $"{(_userDataFetcher.Counter.Values.FirstOrDefault(v => v.ResetType == "daily")?.Value ?? 0) + this._fetcher.Request.Value}"
                    ).Replace(
                        "{userData:values:weekly:changed}",
                        $"{(_userDataFetcher.Counter.Values.FirstOrDefault(v => v.ResetType == "weekly")?.Value ?? 0) + this._fetcher.Request.Value}"
                    ).Replace(
                        "{userData:values:monthly:changed}",
                        $"{(_userDataFetcher.Counter.Values.FirstOrDefault(v => v.ResetType == "monthly")?.Value ?? 0) + this._fetcher.Request.Value}"
                    )
                );
            } else {
                this.onUpdate?.Invoke(
                    this.format.Replace(
                        "{namespaceName}",
                        $"{this._fetcher.Request.NamespaceName}"
                    ).Replace(
                        "{counterName}",
                        $"{this._fetcher.Request.CounterName}"
                    ).Replace(
                        "{userId}",
                        $"{this._fetcher.Request.UserId}"
                    ).Replace(
                        "{value}",
                        $"{this._fetcher.Request.Value}"
                    )
                );
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2MissionIncreaseCounterByUserIdLabel
    {
        private Gs2MissionIncreaseCounterByUserIdFetcher _fetcher;
        private Gs2MissionOwnCounterFetcher _userDataFetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2MissionIncreaseCounterByUserIdFetcher>() ?? GetComponentInParent<Gs2MissionIncreaseCounterByUserIdFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MissionIncreaseCounterByUserIdFetcher.");
                enabled = false;
            }
            this._userDataFetcher = GetComponent<Gs2MissionOwnCounterFetcher>() ?? GetComponentInParent<Gs2MissionOwnCounterFetcher>();
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2MissionIncreaseCounterByUserIdFetcher>() ?? GetComponentInParent<Gs2MissionIncreaseCounterByUserIdFetcher>(true);
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

    public partial class Gs2MissionIncreaseCounterByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MissionIncreaseCounterByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MissionIncreaseCounterByUserIdLabel
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