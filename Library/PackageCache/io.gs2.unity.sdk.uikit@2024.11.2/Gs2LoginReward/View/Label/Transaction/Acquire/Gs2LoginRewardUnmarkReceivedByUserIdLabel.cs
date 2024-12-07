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
using Gs2.Gs2LoginReward.Request;
using Gs2.Unity.Gs2LoginReward.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2LoginReward.Context;
using Gs2.Unity.UiKit.Gs2LoginReward.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2LoginReward.Label
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/LoginReward/ReceiveStatus/View/Label/Transaction/Gs2LoginRewardUnmarkReceivedByUserIdLabel")]
    public partial class Gs2LoginRewardUnmarkReceivedByUserIdLabel : MonoBehaviour
    {
        private void OnFetched()
        {
            if (this._userDataFetcher == null) {
                var context = gameObject.AddComponent<Gs2LoginRewardOwnReceiveStatusContext>();
                context.SetOwnReceiveStatus(
                    OwnReceiveStatus.New(
                        Namespace.New(
                            this._fetcher.Request.NamespaceName
                        ),
                        this._fetcher.Request.BonusModelName
                    )
                );
                this._userDataFetcher = gameObject.AddComponent<Gs2LoginRewardOwnReceiveStatusFetcher>();
                this._userDataFetcher.OnFetched.AddListener(this._onFetched);
            }
            if ((!this._fetcher?.Fetched ?? false) || this._fetcher.Request == null) {
                return;
            }
            if ((!this._userDataFetcher?.Fetched ?? false) || this._userDataFetcher.ReceiveStatus == null) {
                return;
            }
            this.onUpdate?.Invoke(
                this.format.Replace(
                    "{namespaceName}",
                    $"{this._fetcher.Request.NamespaceName}"
                ).Replace(
                    "{bonusModelName}",
                    $"{this._fetcher.Request.BonusModelName}"
                ).Replace(
                    "{userId}",
                    $"{this._fetcher.Request.UserId}"
                ).Replace(
                    "{stepNumber}",
                    $"{this._fetcher.Request.StepNumber}"
                ).Replace(
                    "{timeOffsetToken}",
                    $"{this._fetcher.Request.TimeOffsetToken}"
                ).Replace(
                    "{userData:bonusModelName}",
                    $"{this._userDataFetcher.ReceiveStatus.BonusModelName}"
                ).Replace(
                    "{userData:receivedSteps}",
                    $"{this._userDataFetcher.ReceiveStatus.ReceivedSteps}"
                ).Replace(
                    "{userData:lastReceivedAt}",
                    $"{this._userDataFetcher.ReceiveStatus.LastReceivedAt}"
                )
            );
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2LoginRewardUnmarkReceivedByUserIdLabel
    {
        private Gs2LoginRewardUnmarkReceivedByUserIdFetcher _fetcher;
        private Gs2LoginRewardOwnReceiveStatusFetcher _userDataFetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2LoginRewardUnmarkReceivedByUserIdFetcher>() ?? GetComponentInParent<Gs2LoginRewardUnmarkReceivedByUserIdFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2LoginRewardUnmarkReceivedByUserIdFetcher.");
                enabled = false;
            }
            this._userDataFetcher = GetComponent<Gs2LoginRewardOwnReceiveStatusFetcher>() ?? GetComponentInParent<Gs2LoginRewardOwnReceiveStatusFetcher>();
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2LoginRewardUnmarkReceivedByUserIdFetcher>() ?? GetComponentInParent<Gs2LoginRewardUnmarkReceivedByUserIdFetcher>(true);
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

    public partial class Gs2LoginRewardUnmarkReceivedByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2LoginRewardUnmarkReceivedByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LoginRewardUnmarkReceivedByUserIdLabel
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