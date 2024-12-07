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

	[AddComponentMenu("GS2 UIKit/Inventory/ReferenceOf/View/Label/Transaction/Gs2InventoryVerifyReferenceOfByUserIdLabel")]
    public partial class Gs2InventoryVerifyReferenceOfByUserIdLabel : MonoBehaviour
    {
        private void OnFetched()
        {
            if ((!this._fetcher?.Fetched ?? false) || this._fetcher.Request == null) {
                return;
            }
            this.onUpdate?.Invoke(
                this.format.Replace(
                    "{namespaceName}",
                    $"{this._fetcher.Request.NamespaceName}"
                ).Replace(
                    "{inventoryName}",
                    $"{this._fetcher.Request.InventoryName}"
                ).Replace(
                    "{userId}",
                    $"{this._fetcher.Request.UserId}"
                ).Replace(
                    "{itemName}",
                    $"{this._fetcher.Request.ItemName}"
                ).Replace(
                    "{itemSetName}",
                    $"{this._fetcher.Request.ItemSetName}"
                ).Replace(
                    "{referenceOf}",
                    $"{this._fetcher.Request.ReferenceOf}"
                ).Replace(
                    "{verifyType}",
                    $"{this._fetcher.Request.VerifyType}"
                ).Replace(
                    "{timeOffsetToken}",
                    $"{this._fetcher.Request.TimeOffsetToken}"
                )
            );
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2InventoryVerifyReferenceOfByUserIdLabel
    {
        private Gs2InventoryVerifyReferenceOfByUserIdFetcher _fetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2InventoryVerifyReferenceOfByUserIdFetcher>() ?? GetComponentInParent<Gs2InventoryVerifyReferenceOfByUserIdFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventoryVerifyReferenceOfByUserIdFetcher.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2InventoryVerifyReferenceOfByUserIdFetcher>() ?? GetComponentInParent<Gs2InventoryVerifyReferenceOfByUserIdFetcher>(true);
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

    public partial class Gs2InventoryVerifyReferenceOfByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2InventoryVerifyReferenceOfByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InventoryVerifyReferenceOfByUserIdLabel
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