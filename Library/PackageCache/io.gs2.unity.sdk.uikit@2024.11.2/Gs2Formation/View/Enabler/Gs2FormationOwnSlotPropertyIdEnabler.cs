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
// ReSharper disable CheckPropertyIdspace
// ReSharper disable RedundantPropertyIdQualifier
// ReSharper disable RedundantAssignment
// ReSharper disable NotAccessedVariable
// ReSharper disable RedundantUsingDirective
// ReSharper disable Unity.NoNullPropagation
// ReSharper disable InconsistentNaming

#pragma warning disable CS0472

using System.Collections.Generic;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Formation.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Formation.Enabler
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Formation/Slot/View/Enabler/Properties/PropertyId/Gs2FormationOwnSlotPropertyIdEnabler")]
    public partial class Gs2FormationOwnSlotPropertyIdEnabler : MonoBehaviour
    {
        private void OnFetched()
        {
            switch(this.expression)
            {
                case Expression.In:
                    this.target.SetActive(this.enablePropertyIds.Contains(this._fetcher.Slot?.PropertyId));
                    break;
                case Expression.NotIn:
                    this.target.SetActive(!this.enablePropertyIds.Contains(this._fetcher.Slot?.PropertyId));
                    break;
                case Expression.StartsWith:
                    this.target.SetActive(this._fetcher.Slot?.PropertyId?.StartsWith(this.enablePropertyId) ?? false);
                    break;
                case Expression.EndsWith:
                    this.target.SetActive(this._fetcher.Slot?.PropertyId?.EndsWith(this.enablePropertyId) ?? false);
                    break;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2FormationOwnSlotPropertyIdEnabler
    {
        private Gs2FormationOwnSlotFetcher _fetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2FormationOwnSlotFetcher>() ?? GetComponentInParent<Gs2FormationOwnSlotFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FormationOwnSlotFetcher.");
                enabled = false;
            }
            if (this.target == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: target is not set.");
                enabled = false;
            }
            if (this._fetcher.Fetched) {
                OnFetched();
            }
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2FormationOwnSlotFetcher>() ?? GetComponentInParent<Gs2FormationOwnSlotFetcher>(true);
            if (this._fetcher == null) {
                return true;
            }
            if (this.target == null) {
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

    public partial class Gs2FormationOwnSlotPropertyIdEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2FormationOwnSlotPropertyIdEnabler
    {
        public enum Expression {
            In,
            NotIn,
            StartsWith,
            EndsWith,
        }

        public Expression expression;

        public List<string> enablePropertyIds;

        public string enablePropertyId;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FormationOwnSlotPropertyIdEnabler
    {
        
    }
}