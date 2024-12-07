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
using Gs2.Core.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Inventory.Fetcher;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gs2.Unity.UiKit.Gs2Inventory
{
    /// <summary>
    /// Main
    /// </summary>

    [RequireComponent(typeof(Slider))]
	[AddComponentMenu("GS2 UIKit/Inventory/SimpleItem/View/QuantitySlider/Gs2InventorySimpleItemQuantitySlider")]
    public partial class Gs2InventorySimpleItemQuantitySlider : MonoBehaviour
    {
        private void OnFetched() {
            this._slider.maxValue = this._fetcher.SimpleItem.Count;
        }
        
        public void OnValueChanged(float value) {
            this.onQuantityChanged.Invoke(this.quantity = (long)value);
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2InventorySimpleItemQuantitySlider
    {
        private Slider _slider;
        private Gs2InventoryOwnSimpleItemFetcher _fetcher;

        public void Awake()
        {
            this._slider = GetComponent<Slider>() ?? GetComponentInParent<Slider>();
            if (this._slider == null) {
                Debug.LogWarning($"{gameObject.GetFullPath()}: Couldn't find the Slider.");
                enabled = false;
            }
            this._fetcher = GetComponent<Gs2InventoryOwnSimpleItemFetcher>() ?? GetComponentInParent<Gs2InventoryOwnSimpleItemFetcher>();
            if (this._fetcher == null) {
                Debug.LogWarning($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventoryOwnSimpleItemFetcher.");
                enabled = false;
            }

            this._slider.value = this.quantity;
        }

        public bool HasError()
        {
            this._fetcher = GetComponent<Gs2InventoryOwnSimpleItemFetcher>() ?? GetComponentInParent<Gs2InventoryOwnSimpleItemFetcher>(true);
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
            
            this._slider.onValueChanged.AddListener(OnValueChanged);
        }

        public void OnDisable()
        {
            if (this._onFetched != null) {
                this._fetcher.OnFetched.RemoveListener(this._onFetched);
                this._onFetched = null;
            }
            this._slider.onValueChanged.RemoveListener(OnValueChanged);
        }

        public void Add() {
            this._slider.value += 1;
        }

        public void Sub() {
            this._slider.value -= 1;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2InventorySimpleItemQuantitySlider
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2InventorySimpleItemQuantitySlider
    {
        public long quantity;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InventorySimpleItemQuantitySlider
    {
        public UnityEvent<long> onQuantityChanged = new UnityEvent<long>();
    }
}