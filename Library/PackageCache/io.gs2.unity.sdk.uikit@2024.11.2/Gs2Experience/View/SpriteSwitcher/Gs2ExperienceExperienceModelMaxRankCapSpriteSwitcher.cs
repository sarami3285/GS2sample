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
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Experience.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Experience
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Experience/ExperienceModel/View/SpriteSwitcher/Properties/MaxRankCap/Gs2ExperienceExperienceModelMaxRankCapSpriteSwitcher")]
    public partial class Gs2ExperienceExperienceModelMaxRankCapSpriteSwitcher : MonoBehaviour
    {
        private void OnFetched()
        {
            switch(this.expression)
            {
                case Expression.In:
                    if (this.applyMaxRankCaps.Contains(this._fetcher.ExperienceModel.MaxRankCap)) {
                        this.onUpdate.Invoke(this.sprite);
                    }
                    break;
                case Expression.NotIn:
                    if (!this.applyMaxRankCaps.Contains(this._fetcher.ExperienceModel.MaxRankCap)) {
                        this.onUpdate.Invoke(this.sprite);
                    }
                    break;
                case Expression.Less:
                    if (this.applyMaxRankCap > this._fetcher.ExperienceModel.MaxRankCap) {
                        this.onUpdate.Invoke(this.sprite);
                    }
                    break;
                case Expression.LessEqual:
                    if (this.applyMaxRankCap >= this._fetcher.ExperienceModel.MaxRankCap) {
                        this.onUpdate.Invoke(this.sprite);
                    }
                    break;
                case Expression.Greater:
                    if (this.applyMaxRankCap < this._fetcher.ExperienceModel.MaxRankCap) {
                        this.onUpdate.Invoke(this.sprite);
                    }
                    break;
                case Expression.GreaterEqual:
                    if (this.applyMaxRankCap <= this._fetcher.ExperienceModel.MaxRankCap) {
                        this.onUpdate.Invoke(this.sprite);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ExperienceExperienceModelMaxRankCapSpriteSwitcher
    {
        private Gs2ExperienceExperienceModelFetcher _fetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2ExperienceExperienceModelFetcher>() ?? GetComponentInParent<Gs2ExperienceExperienceModelFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ExperienceExperienceModelFetcher.");
                enabled = false;
            }
            if (this.sprite == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: sprite is not set.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2ExperienceExperienceModelFetcher>() ?? GetComponentInParent<Gs2ExperienceExperienceModelFetcher>(true);
            if (this._fetcher == null) {
                return true;
            }
            if (this.sprite == null) {
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

    public partial class Gs2ExperienceExperienceModelMaxRankCapSpriteSwitcher
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ExperienceExperienceModelMaxRankCapSpriteSwitcher
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

        public List<long> applyMaxRankCaps;

        public long applyMaxRankCap;

        public Sprite sprite;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExperienceExperienceModelMaxRankCapSpriteSwitcher
    {
        [Serializable]
        private class UpdateEvent : UnityEvent<Sprite>
        {

        }

        [SerializeField]
        private UpdateEvent onUpdate = new UpdateEvent();

        public event UnityAction<Sprite> OnUpdate
        {
            add => onUpdate.AddListener(value);
            remove => onUpdate.RemoveListener(value);
        }
    }
}