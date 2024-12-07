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
using System.Collections.Generic;
using Gs2.Gs2Grade.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Grade.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Grade.Enabler
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Grade/Status/View/Enabler/Transaction/Gs2GradeSubGradeByUserIdValueEnabler")]
    public partial class Gs2GradeSubGradeByUserIdValueEnabler : MonoBehaviour
    {
        private void OnFetched()
        {
            if (!this._fetcher.Fetched || this._fetcher.Request == null) {
                return;
            }
            switch(this.expression)
            {
                case Expression.In:
                    this.target.SetActive(this._fetcher.Request.GradeValue != null && this.enableGradeValues.Contains(this._fetcher.Request.GradeValue.Value));
                    break;
                case Expression.NotIn:
                    this.target.SetActive(this._fetcher.Request.GradeValue != null && !this.enableGradeValues.Contains(this._fetcher.Request.GradeValue.Value));
                    break;
                case Expression.Less:
                    this.target.SetActive(this.enableGradeValue > this._fetcher.Request.GradeValue);
                    break;
                case Expression.LessEqual:
                    this.target.SetActive(this.enableGradeValue >= this._fetcher.Request.GradeValue);
                    break;
                case Expression.Greater:
                    this.target.SetActive(this.enableGradeValue < this._fetcher.Request.GradeValue);
                    break;
                case Expression.GreaterEqual:
                    this.target.SetActive(this.enableGradeValue <= this._fetcher.Request.GradeValue);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2GradeSubGradeByUserIdValueEnabler
    {
        private Gs2GradeSubGradeByUserIdFetcher _fetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2GradeSubGradeByUserIdFetcher>() ?? GetComponentInParent<Gs2GradeSubGradeByUserIdFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2GradeSubGradeByUserIdFetcher.");
                enabled = false;
            }
            if (this.target == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: target is not set.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2GradeSubGradeByUserIdFetcher>() ?? GetComponentInParent<Gs2GradeSubGradeByUserIdFetcher>(true);
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

    public partial class Gs2GradeSubGradeByUserIdValueEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2GradeSubGradeByUserIdValueEnabler
    {
        public enum Expression {
            In,
            NotIn,
            Less,
            LessEqual,
            Greater,
            GreaterEqual,
        }

        public Expression expression;

        public List<long> enableGradeValues;

        public long enableGradeValue;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2GradeSubGradeByUserIdValueEnabler
    {

    }
}