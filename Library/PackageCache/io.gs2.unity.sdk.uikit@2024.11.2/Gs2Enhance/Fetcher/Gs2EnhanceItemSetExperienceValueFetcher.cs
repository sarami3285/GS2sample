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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gs2.Core.Exception;
using Gs2.Unity.Core.Exception;
using Gs2.Unity.Gs2Enhance.Domain.Model;
using Gs2.Unity.Gs2Enhance.Model;
using Gs2.Unity.Gs2Enhance.ScriptableObject;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Enhance.Context;
using Gs2.Unity.UiKit.Gs2Experience.Fetcher;
using Gs2.Unity.UiKit.Gs2Inventory.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Gs2.Unity.UiKit.Gs2Enhance.Fetcher
{
    /// <summary>
    /// Main
    /// </summary>

    public partial class Gs2EnhanceItemSetExperienceValueFetcher : MonoBehaviour
    {
        private void OnUpdate() {
            if (!_rateModelFetcher.Fetched || !_ownItemSetFetcher.Fetched || !_itemModelFetcher.Fetched || !_ownStatusFetcher.Fetched) return;
            try {
                var v = this._rateModelFetcher.RateModel.AcquireExperienceHierarchy.Aggregate(
                    JsonMapper.ToObject(_itemModelFetcher.ItemModel.Metadata),
                    (current, element) => current[element]);
                long.TryParse(v.ToString(), out this._unitValue);
            }
            catch {
                // ignored
            }
            this._currentExperienceValue = _ownStatusFetcher.Status.ExperienceValue;
            this._maxExperienceValue = _experienceModelFetcher.ExperienceModel.RankThreshold.Values[(int)_ownStatusFetcher.Status.RankCapValue - 1];
            this._maxQuantity = (this._maxExperienceValue - this._currentExperienceValue + this._unitValue - 1) / this._unitValue;
            this._targetId = _ownItemSetFetcher.ItemSet.FirstOrDefault()?.ItemSetId;
            this.Fetched = true;
            this.OnFetched.Invoke();
        }

        public void OnEnable()
        {
            _rateModelFetcher.OnFetched.AddListener(OnUpdate);
            _ownItemSetFetcher.OnFetched.AddListener(OnUpdate);
            _itemModelFetcher.OnFetched.AddListener(OnUpdate);
            _ownStatusFetcher.OnFetched.AddListener(OnUpdate);
        }

        public void OnDisable()
        {
            _rateModelFetcher.OnFetched.RemoveListener(OnUpdate);
            _ownItemSetFetcher.OnFetched.RemoveListener(OnUpdate);
            _itemModelFetcher.OnFetched.RemoveListener(OnUpdate);
            _ownStatusFetcher.OnFetched.RemoveListener(OnUpdate);
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2EnhanceItemSetExperienceValueFetcher
    {
        public Gs2EnhanceRateModelFetcher _rateModelFetcher { get; private set; }
        public Gs2InventoryOwnItemSetFetcher _ownItemSetFetcher { get; private set; }
        public Gs2InventoryItemModelFetcher _itemModelFetcher { get; private set; }
        public Gs2ExperienceExperienceModelFetcher _experienceModelFetcher { get; private set; }
        public Gs2ExperienceOwnStatusFetcher _ownStatusFetcher { get; private set; }

        public void Awake()
        {
            _rateModelFetcher = GetComponent<Gs2EnhanceRateModelFetcher>() ?? GetComponentInParent<Gs2EnhanceRateModelFetcher>();
            if (_rateModelFetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2EnhanceRateModelFetcher.");
                enabled = false;
            }
            _ownItemSetFetcher = GetComponent<Gs2InventoryOwnItemSetFetcher>() ?? GetComponentInParent<Gs2InventoryOwnItemSetFetcher>();
            if (_ownItemSetFetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventoryOwnItemSetFetcher.");
                enabled = false;
            }
            _itemModelFetcher = GetComponent<Gs2InventoryItemModelFetcher>() ?? GetComponentInParent<Gs2InventoryItemModelFetcher>();
            if (_itemModelFetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventoryItemModelFetcher.");
                enabled = false;
            }
            _experienceModelFetcher = GetComponent<Gs2ExperienceExperienceModelFetcher>() ?? GetComponentInParent<Gs2ExperienceExperienceModelFetcher>();
            if (_experienceModelFetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ExperienceExperienceModelFetcher.");
                enabled = false;
            }
            _ownStatusFetcher = GetComponent<Gs2ExperienceOwnStatusFetcher>() ?? GetComponentInParent<Gs2ExperienceOwnStatusFetcher>();
            if (_ownStatusFetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ExperienceOwnStatusFetcher.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            _rateModelFetcher = GetComponent<Gs2EnhanceRateModelFetcher>() ?? GetComponentInParent<Gs2EnhanceRateModelFetcher>(true);
            if (_rateModelFetcher == null) {
                return true;
            }
            _ownItemSetFetcher = GetComponent<Gs2InventoryOwnItemSetFetcher>() ?? GetComponentInParent<Gs2InventoryOwnItemSetFetcher>();
            if (_ownItemSetFetcher == null) {
                return true;
            }
            _itemModelFetcher = GetComponent<Gs2InventoryItemModelFetcher>() ?? GetComponentInParent<Gs2InventoryItemModelFetcher>(true);
            if (_itemModelFetcher == null) {
                return true;
            }
            _ownStatusFetcher = GetComponent<Gs2ExperienceOwnStatusFetcher>() ?? GetComponentInParent<Gs2ExperienceOwnStatusFetcher>(true);
            if (_ownStatusFetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2EnhanceItemSetExperienceValueFetcher
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2EnhanceItemSetExperienceValueFetcher
    {
        private string _targetId;
        public string TargetId => this._targetId;
        private long _unitValue;
        public long UnitValue => this._unitValue;
        private long _currentExperienceValue;
        public long CurrentExperienceValue => this._currentExperienceValue;
        private long _maxExperienceValue;
        public long MaxExperienceValue => this._maxExperienceValue;
        private long _maxQuantity;
        public long MaxQuantity => this._maxQuantity;

        public bool Fetched;
        public UnityEvent OnFetched = new UnityEvent();
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2EnhanceItemSetExperienceValueFetcher
    {
        [SerializeField]
        internal ErrorEvent onError = new ErrorEvent();

        public event UnityAction<Gs2Exception, Func<IEnumerator>> OnError
        {
            add => onError.AddListener(value);
            remove => onError.RemoveListener(value);
        }
    }
}