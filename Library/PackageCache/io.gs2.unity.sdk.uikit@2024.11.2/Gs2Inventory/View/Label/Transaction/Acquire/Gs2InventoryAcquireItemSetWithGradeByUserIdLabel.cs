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

	[AddComponentMenu("GS2 UIKit/Inventory/ItemSet/View/Label/Transaction/Gs2InventoryAcquireItemSetWithGradeByUserIdLabel")]
    public partial class Gs2InventoryAcquireItemSetWithGradeByUserIdLabel : MonoBehaviour
    {
        private void OnFetched()
        {
            if ((!this._fetcher?.Fetched ?? false) || this._fetcher.Request == null) {
                return;
            }
            if ((!this._userDataFetcher?.Fetched ?? false) || this._userDataFetcher.ItemSet == null) {
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
                    "{itemName}",
                    $"{this._fetcher.Request.ItemName}"
                ).Replace(
                    "{userId}",
                    $"{this._fetcher.Request.UserId}"
                ).Replace(
                    "{gradeModelId}",
                    $"{this._fetcher.Request.GradeModelId}"
                ).Replace(
                    "{gradeValue}",
                    $"{this._fetcher.Request.GradeValue}"
                ).Replace(
                    "{userData:itemSetId}",
                    $"{this._userDataFetcher.ItemSet[0].ItemSetId}"
                ).Replace(
                    "{userData:name}",
                    $"{this._userDataFetcher.ItemSet[0].Name}"
                ).Replace(
                    "{userData:inventoryName}",
                    $"{this._userDataFetcher.ItemSet[0].InventoryName}"
                ).Replace(
                    "{userData:itemName}",
                    $"{this._userDataFetcher.ItemSet[0].ItemName}"
                ).Replace(
                    "{userData:count}",
                    $"{this._userDataFetcher.ItemSet[0].Count}"
                ).Replace(
                    "{userData:sortValue}",
                    $"{this._userDataFetcher.ItemSet[0].SortValue}"
                ).Replace(
                    "{userData:expiresAt}",
                    $"{this._userDataFetcher.ItemSet[0].ExpiresAt}"
                ).Replace(
                    "{userData:referenceOf}",
                    $"{this._userDataFetcher.ItemSet[0].ReferenceOf}"
                )
            );
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2InventoryAcquireItemSetWithGradeByUserIdLabel
    {
        private Gs2InventoryAcquireItemSetWithGradeByUserIdFetcher _fetcher;
        private Gs2InventoryOwnItemSetFetcher _userDataFetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2InventoryAcquireItemSetWithGradeByUserIdFetcher>() ?? GetComponentInParent<Gs2InventoryAcquireItemSetWithGradeByUserIdFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventoryAcquireItemSetWithGradeByUserIdFetcher.");
                enabled = false;
            }
            this._userDataFetcher = GetComponent<Gs2InventoryOwnItemSetFetcher>() ?? GetComponentInParent<Gs2InventoryOwnItemSetFetcher>();
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2InventoryAcquireItemSetWithGradeByUserIdFetcher>() ?? GetComponentInParent<Gs2InventoryAcquireItemSetWithGradeByUserIdFetcher>(true);
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

    public partial class Gs2InventoryAcquireItemSetWithGradeByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2InventoryAcquireItemSetWithGradeByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InventoryAcquireItemSetWithGradeByUserIdLabel
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