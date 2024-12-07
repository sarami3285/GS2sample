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
using Gs2.Gs2Enchant.Request;
using Gs2.Unity.Gs2Enchant.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Enchant.Context;
using Gs2.Unity.UiKit.Gs2Enchant.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Enchant.Label
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Enchant/BalanceParameterStatus/View/Label/Transaction/Gs2EnchantReDrawBalanceParameterStatusByUserIdLabel")]
    public partial class Gs2EnchantReDrawBalanceParameterStatusByUserIdLabel : MonoBehaviour
    {
        private void OnFetched()
        {
            if (this._userDataFetcher == null) {
                var context = gameObject.AddComponent<Gs2EnchantOwnBalanceParameterStatusContext>();
                context.SetOwnBalanceParameterStatus(
                    OwnBalanceParameterStatus.New(
                        Namespace.New(
                            this._fetcher.Request.NamespaceName
                        ),
                        this._fetcher.Request.ParameterName,
                        this._fetcher.Request.PropertyId
                    )
                );
                this._userDataFetcher = gameObject.AddComponent<Gs2EnchantOwnBalanceParameterStatusFetcher>();
                this._userDataFetcher.OnFetched.AddListener(this._onFetched);
            }
            if ((!this._fetcher?.Fetched ?? false) || this._fetcher.Request == null) {
                return;
            }
            if ((!this._userDataFetcher?.Fetched ?? false) || this._userDataFetcher.BalanceParameterStatus == null) {
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
                    "{parameterName}",
                    $"{this._fetcher.Request.ParameterName}"
                ).Replace(
                    "{propertyId}",
                    $"{this._fetcher.Request.PropertyId}"
                ).Replace(
                    "{fixedParameterNames}",
                    $"{this._fetcher.Request.FixedParameterNames}"
                ).Replace(
                    "{timeOffsetToken}",
                    $"{this._fetcher.Request.TimeOffsetToken}"
                ).Replace(
                    "{userData:parameterName}",
                    $"{this._userDataFetcher.BalanceParameterStatus.ParameterName}"
                ).Replace(
                    "{userData:propertyId}",
                    $"{this._userDataFetcher.BalanceParameterStatus.PropertyId}"
                ).Replace(
                    "{userData:parameterValues}",
                    $"{this._userDataFetcher.BalanceParameterStatus.ParameterValues}"
                )
            );
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2EnchantReDrawBalanceParameterStatusByUserIdLabel
    {
        private Gs2EnchantReDrawBalanceParameterStatusByUserIdFetcher _fetcher;
        private Gs2EnchantOwnBalanceParameterStatusFetcher _userDataFetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2EnchantReDrawBalanceParameterStatusByUserIdFetcher>() ?? GetComponentInParent<Gs2EnchantReDrawBalanceParameterStatusByUserIdFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2EnchantReDrawBalanceParameterStatusByUserIdFetcher.");
                enabled = false;
            }
            this._userDataFetcher = GetComponent<Gs2EnchantOwnBalanceParameterStatusFetcher>() ?? GetComponentInParent<Gs2EnchantOwnBalanceParameterStatusFetcher>();
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2EnchantReDrawBalanceParameterStatusByUserIdFetcher>() ?? GetComponentInParent<Gs2EnchantReDrawBalanceParameterStatusByUserIdFetcher>(true);
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

    public partial class Gs2EnchantReDrawBalanceParameterStatusByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2EnchantReDrawBalanceParameterStatusByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2EnchantReDrawBalanceParameterStatusByUserIdLabel
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