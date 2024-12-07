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

using System.Collections.Generic;
using Gs2.Unity.Gs2Enhance.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Enhance.Context;
using Gs2.Unity.UiKit.Gs2Enhance.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Enhance
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Enhance/RateModel/View/Gs2EnhanceRateModelList")]
    public partial class Gs2EnhanceRateModelList : MonoBehaviour
    {
        private List<Gs2EnhanceRateModelContext> _children;

        private void OnFetched() {
            for (var i = 0; i < this._children.Count; i++) {
                if (i < this._fetcher.RateModels.Count) {
                    this._children[i].SetRateModel(
                        RateModel.New(
                            this._fetcher.Context.Namespace,
                            this._fetcher.RateModels[i].Name
                        )
                    );
                    this._children[i].gameObject.SetActive(true);
                }
                else {
                    this._children[i].gameObject.SetActive(false);
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2EnhanceRateModelList
    {
        private Gs2EnhanceRateModelListFetcher _fetcher;

        private void Initialize() {
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.RateModel = RateModel.New(
                    this._fetcher.Context.Namespace,
                    ""
                );
                node.gameObject.SetActive(false);
                this._children.Add(node);
            }
        }

        public void Awake()
        {
            if (this.prefab == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2EnhanceRateModelContext Prefab.");
                enabled = false;
                return;
            }

            this._fetcher = GetComponent<Gs2EnhanceRateModelListFetcher>() ?? GetComponentInParent<Gs2EnhanceRateModelListFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2EnhanceRateModelListFetcher.");
                enabled = false;
            }

            this._children = new List<Gs2EnhanceRateModelContext>();
            this.prefab.gameObject.SetActive(false);

            Invoke(nameof(Initialize), 0);
        }

        public virtual bool HasError()
        {
            if (this.prefab == null) {
                return true;
            }
            this._fetcher = GetComponent<Gs2EnhanceRateModelListFetcher>() ?? GetComponentInParent<Gs2EnhanceRateModelListFetcher>(true);
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

    public partial class Gs2EnhanceRateModelList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2EnhanceRateModelList
    {
        public Gs2EnhanceRateModelContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2EnhanceRateModelList
    {

    }
}