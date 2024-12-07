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
using Gs2.Core.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Money.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Money
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Money/Wallet/View/Label/Gs2MoneyOwnWalletLabel")]
    public partial class Gs2MoneyOwnWalletLabel : MonoBehaviour
    {
        private void OnFetched()
        {
            var updatedAt = this._fetcher.Wallet.UpdatedAt == null ? DateTime.Now : UnixTime.FromUnixTime(_fetcher.Wallet.UpdatedAt).ToLocalTime();
            this.onUpdate?.Invoke(
                this.format.Replace(
                    "{slot}", $"{this._fetcher?.Wallet?.Slot}"
                ).Replace(
                    "{paid}", $"{this._fetcher?.Wallet?.Paid}"
                ).Replace(
                    "{free}", $"{this._fetcher?.Wallet?.Free}"
                ).Replace(
                    "{total}", $"{_fetcher?.Wallet?.Free + _fetcher?.Wallet?.Paid}"
                ).Replace(
                    "{updatedAt:yyyy}", updatedAt.ToString("yyyy")
                ).Replace(
                    "{updatedAt:yy}", updatedAt.ToString("yy")
                ).Replace(
                    "{updatedAt:MM}", updatedAt.ToString("MM")
                ).Replace(
                    "{updatedAt:MMM}", updatedAt.ToString("MMM")
                ).Replace(
                    "{updatedAt:dd}", updatedAt.ToString("dd")
                ).Replace(
                    "{updatedAt:hh}", updatedAt.ToString("hh")
                ).Replace(
                    "{updatedAt:HH}", updatedAt.ToString("HH")
                ).Replace(
                    "{updatedAt:tt}", updatedAt.ToString("tt")
                ).Replace(
                    "{updatedAt:mm}", updatedAt.ToString("mm")
                ).Replace(
                    "{updatedAt:ss}", updatedAt.ToString("ss")
                )
            );
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2MoneyOwnWalletLabel
    {
        private Gs2MoneyOwnWalletFetcher _fetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2MoneyOwnWalletFetcher>() ?? GetComponentInParent<Gs2MoneyOwnWalletFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MoneyOwnWalletFetcher.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2MoneyOwnWalletFetcher>() ?? GetComponentInParent<Gs2MoneyOwnWalletFetcher>(true);
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
        }

        public void OnDisable()
        {
            if (this._onFetched != null) {
                this._fetcher.OnFetched.RemoveListener(this._onFetched);
                this._onFetched = null;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MoneyOwnWalletLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MoneyOwnWalletLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MoneyOwnWalletLabel
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