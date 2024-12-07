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
using Gs2.Gs2Stamina.Request;
using Gs2.Unity.Gs2Stamina.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Stamina.Context;
using Gs2.Unity.UiKit.Gs2Stamina.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Stamina.Label
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Stamina/Stamina/View/Label/Transaction/Gs2StaminaSetMaxValueByUserIdLabel")]
    public partial class Gs2StaminaSetMaxValueByUserIdLabel : MonoBehaviour
    {
        private void OnFetched()
        {
            if (this._userDataFetcher == null) {
                var context = gameObject.AddComponent<Gs2StaminaOwnStaminaContext>();
                context.SetOwnStamina(
                    OwnStamina.New(
                        Namespace.New(
                            this._fetcher.Request.NamespaceName
                        ),
                        this._fetcher.Request.StaminaName
                    )
                );
                this._userDataFetcher = gameObject.AddComponent<Gs2StaminaOwnStaminaFetcher>();
                this._userDataFetcher.OnFetched.AddListener(this._onFetched);
            }
            if ((!this._fetcher?.Fetched ?? false) || this._fetcher.Request == null) {
                return;
            }
            if ((!this._userDataFetcher?.Fetched ?? false) || this._userDataFetcher.Stamina == null) {
                return;
            }
            this.onUpdate?.Invoke(
                this.format.Replace(
                    "{namespaceName}",
                    $"{this._fetcher.Request.NamespaceName}"
                ).Replace(
                    "{staminaName}",
                    $"{this._fetcher.Request.StaminaName}"
                ).Replace(
                    "{userId}",
                    $"{this._fetcher.Request.UserId}"
                ).Replace(
                    "{maxValue}",
                    $"{this._fetcher.Request.MaxValue}"
                ).Replace(
                    "{timeOffsetToken}",
                    $"{this._fetcher.Request.TimeOffsetToken}"
                ).Replace(
                    "{userData:staminaName}",
                    $"{this._userDataFetcher.Stamina.StaminaName}"
                ).Replace(
                    "{userData:value}",
                    $"{this._userDataFetcher.Stamina.Value}"
                ).Replace(
                    "{userData:overflowValue}",
                    $"{this._userDataFetcher.Stamina.OverflowValue}"
                ).Replace(
                    "{userData:maxValue}",
                    $"{this._userDataFetcher.Stamina.MaxValue}"
                ).Replace(
                    "{userData:maxValue:changed}",
                    $"{this._userDataFetcher.Stamina.MaxValue + this._fetcher.Request.MaxValue}"
                ).Replace(
                    "{userData:recoverIntervalMinutes}",
                    $"{this._userDataFetcher.Stamina.RecoverIntervalMinutes}"
                ).Replace(
                    "{userData:recoverValue}",
                    $"{this._userDataFetcher.Stamina.RecoverValue}"
                ).Replace(
                    "{userData:nextRecoverAt}",
                    $"{this._userDataFetcher.Stamina.NextRecoverAt}"
                )
            );
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2StaminaSetMaxValueByUserIdLabel
    {
        private Gs2StaminaSetMaxValueByUserIdFetcher _fetcher;
        private Gs2StaminaOwnStaminaFetcher _userDataFetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2StaminaSetMaxValueByUserIdFetcher>() ?? GetComponentInParent<Gs2StaminaSetMaxValueByUserIdFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2StaminaSetMaxValueByUserIdFetcher.");
                enabled = false;
            }
            this._userDataFetcher = GetComponent<Gs2StaminaOwnStaminaFetcher>() ?? GetComponentInParent<Gs2StaminaOwnStaminaFetcher>();
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2StaminaSetMaxValueByUserIdFetcher>() ?? GetComponentInParent<Gs2StaminaSetMaxValueByUserIdFetcher>(true);
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

    public partial class Gs2StaminaSetMaxValueByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2StaminaSetMaxValueByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2StaminaSetMaxValueByUserIdLabel
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