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
using Gs2.Gs2Inventory.Request;
using Gs2.Unity.Gs2Inventory.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Inventory.Context;
using Gs2.Unity.UiKit.Gs2Inventory.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Inventory.Label
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Inventory/Inventory/View/Label/Transaction/Gs2InventoryVerifyInventoryCurrentMaxCapacityByUserIdLabel")]
    public partial class Gs2InventoryVerifyInventoryCurrentMaxCapacityByUserIdLabel : MonoBehaviour
    {
        private void OnFetched()
        {
            if (this._userDataFetcher == null) {
                var context = gameObject.AddComponent<Gs2InventoryOwnInventoryContext>();
                context.SetOwnInventory(
                    OwnInventory.New(
                        Namespace.New(
                            this._fetcher.Request.NamespaceName
                        ),
                        this._fetcher.Request.InventoryName
                    )
                );
                this._userDataFetcher = gameObject.AddComponent<Gs2InventoryOwnInventoryFetcher>();
                this._userDataFetcher.OnFetched.AddListener(this._onFetched);
            }
            if ((!this._fetcher?.Fetched ?? false) || this._fetcher.Request == null) {
                return;
            }
            if ((!this._userDataFetcher?.Fetched ?? false) || this._userDataFetcher.Inventory == null) {
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
                    "{inventoryName}",
                    $"{this._fetcher.Request.InventoryName}"
                ).Replace(
                    "{verifyType}",
                    $"{this._fetcher.Request.VerifyType}"
                ).Replace(
                    "{currentInventoryMaxCapacity}",
                    $"{this._fetcher.Request.CurrentInventoryMaxCapacity}"
                ).Replace(
                    "{multiplyValueSpecifyingQuantity}",
                    $"{this._fetcher.Request.MultiplyValueSpecifyingQuantity}"
                ).Replace(
                    "{timeOffsetToken}",
                    $"{this._fetcher.Request.TimeOffsetToken}"
                ).Replace(
                    "{userData:inventoryId}",
                    $"{this._userDataFetcher.Inventory.InventoryId}"
                ).Replace(
                    "{userData:inventoryName}",
                    $"{this._userDataFetcher.Inventory.InventoryName}"
                ).Replace(
                    "{userData:currentInventoryCapacityUsage}",
                    $"{this._userDataFetcher.Inventory.CurrentInventoryCapacityUsage}"
                ).Replace(
                    "{userData:currentInventoryMaxCapacity}",
                    $"{this._userDataFetcher.Inventory.CurrentInventoryMaxCapacity}"
                ).Replace(
                    "{userData:currentInventoryMaxCapacity:changed}",
                    $"{this._userDataFetcher.Inventory.CurrentInventoryMaxCapacity + this._fetcher.Request.CurrentInventoryMaxCapacity}"
                )
            );
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2InventoryVerifyInventoryCurrentMaxCapacityByUserIdLabel
    {
        private Gs2InventoryVerifyInventoryCurrentMaxCapacityByUserIdFetcher _fetcher;
        private Gs2InventoryOwnInventoryFetcher _userDataFetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2InventoryVerifyInventoryCurrentMaxCapacityByUserIdFetcher>() ?? GetComponentInParent<Gs2InventoryVerifyInventoryCurrentMaxCapacityByUserIdFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventoryVerifyInventoryCurrentMaxCapacityByUserIdFetcher.");
                enabled = false;
            }
            this._userDataFetcher = GetComponent<Gs2InventoryOwnInventoryFetcher>() ?? GetComponentInParent<Gs2InventoryOwnInventoryFetcher>();
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2InventoryVerifyInventoryCurrentMaxCapacityByUserIdFetcher>() ?? GetComponentInParent<Gs2InventoryVerifyInventoryCurrentMaxCapacityByUserIdFetcher>(true);
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

    public partial class Gs2InventoryVerifyInventoryCurrentMaxCapacityByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2InventoryVerifyInventoryCurrentMaxCapacityByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InventoryVerifyInventoryCurrentMaxCapacityByUserIdLabel
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