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
using Gs2.Core.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Inventory.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Inventory
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Inventory/ItemSet/View/Label/Gs2InventoryOwnItemSetQuantitySliderLabel")]
    public partial class Gs2InventoryOwnItemSetQuantitySliderLabel : MonoBehaviour
    {
        private void OnValueChanged(long quantity)
        {
            OnFetched();
        }
        
        private void OnFetched()
        {
            var expiresAt = this._fetcher.ItemSet[index].ExpiresAt == null ? DateTime.Now : UnixTime.FromUnixTime(_fetcher.ItemSet[index].ExpiresAt).ToLocalTime();
            this.onUpdate?.Invoke(
                this.format.Replace(
                    "{userData:itemSetId}", $"{this._fetcher?.ItemSet?[index].ItemSetId}"
                ).Replace(
                    "{userData:name}", $"{this._fetcher?.ItemSet?[index].Name}"
                ).Replace(
                    "{userData:inventoryName}", $"{this._fetcher?.ItemSet?[index].InventoryName}"
                ).Replace(
                    "{userData:itemName}", $"{this._fetcher?.ItemSet?[index].ItemName}"
                ).Replace(
                    "{userData:count}", $"{this._fetcher?.ItemSet?[index].Count}"
                ).Replace(
                    "{userData:sortValue}", $"{this._fetcher?.ItemSet?[index].SortValue}"
                ).Replace(
                    "{userData:expiresAt:yyyy}", expiresAt.ToString("yyyy")
                ).Replace(
                    "{userData:expiresAt:yy}", expiresAt.ToString("yy")
                ).Replace(
                    "{userData:expiresAt:MM}", expiresAt.ToString("MM")
                ).Replace(
                    "{userData:expiresAt:MMM}", expiresAt.ToString("MMM")
                ).Replace(
                    "{userData:expiresAt:dd}", expiresAt.ToString("dd")
                ).Replace(
                    "{userData:expiresAt:hh}", expiresAt.ToString("hh")
                ).Replace(
                    "{userData:expiresAt:HH}", expiresAt.ToString("HH")
                ).Replace(
                    "{userData:expiresAt:tt}", expiresAt.ToString("tt")
                ).Replace(
                    "{userData:expiresAt:mm}", expiresAt.ToString("mm")
                ).Replace(
                    "{userData:expiresAt:ss}", expiresAt.ToString("ss")
                ).Replace(
                    "{userData:referenceOf}", $"{this._fetcher?.ItemSet?[index].ReferenceOf}"
                ).Replace(
                    "{quantity}", $"{this._slider?.quantity}"
                )
            );
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2InventoryOwnItemSetQuantitySliderLabel
    {
        private Gs2InventoryItemSetQuantitySlider _slider;
        private Gs2InventoryOwnItemSetFetcher _fetcher;

        public void Awake()
        {
            this._slider = GetComponent<Gs2InventoryItemSetQuantitySlider>() ?? GetComponentInParent<Gs2InventoryItemSetQuantitySlider>();
            if (this._slider == null) {
                Debug.LogWarning($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventoryItemSetQuantitySlider.");
                enabled = false;
            }
            this._fetcher = GetComponent<Gs2InventoryOwnItemSetFetcher>() ?? GetComponentInParent<Gs2InventoryOwnItemSetFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventoryOwnItemSetFetcher.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2InventoryOwnItemSetFetcher>() ?? GetComponentInParent<Gs2InventoryOwnItemSetFetcher>(true);
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
            
            this._slider.onQuantityChanged.AddListener(OnValueChanged);
        }

        public void OnDisable()
        {
            if (this._onFetched != null) {
                this._fetcher.OnFetched.RemoveListener(this._onFetched);
                this._onFetched = null;
            }
            
            this._slider.onQuantityChanged.RemoveListener(OnValueChanged);
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2InventoryOwnItemSetQuantitySliderLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2InventoryOwnItemSetQuantitySliderLabel
    {
        public string format;

        public int index;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InventoryOwnItemSetQuantitySliderLabel
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