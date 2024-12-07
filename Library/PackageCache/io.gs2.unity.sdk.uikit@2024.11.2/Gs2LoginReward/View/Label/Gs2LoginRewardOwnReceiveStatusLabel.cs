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
using Gs2.Unity.UiKit.Gs2LoginReward.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2LoginReward
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/LoginReward/ReceiveStatus/View/Label/Gs2LoginRewardOwnReceiveStatusLabel")]
    public partial class Gs2LoginRewardOwnReceiveStatusLabel : MonoBehaviour
    {
        private void OnFetched()
        {
            var lastReceivedAt = this._fetcher.ReceiveStatus.LastReceivedAt == null ? DateTime.Now : _fetcher.ReceiveStatus.LastReceivedAt.ToLocalTime();
            this.onUpdate?.Invoke(
                this.format.Replace(
                    "{bonusModelName}", $"{this._fetcher?.ReceiveStatus?.BonusModelName}"
                ).Replace(
                    "{receivedSteps}", $"{this._fetcher?.ReceiveStatus?.ReceivedSteps}"
                ).Replace(
                    "{lastReceivedAt:yyyy}", lastReceivedAt.ToString("yyyy")
                ).Replace(
                    "{lastReceivedAt:yy}", lastReceivedAt.ToString("yy")
                ).Replace(
                    "{lastReceivedAt:MM}", lastReceivedAt.ToString("MM")
                ).Replace(
                    "{lastReceivedAt:MMM}", lastReceivedAt.ToString("MMM")
                ).Replace(
                    "{lastReceivedAt:dd}", lastReceivedAt.ToString("dd")
                ).Replace(
                    "{lastReceivedAt:hh}", lastReceivedAt.ToString("hh")
                ).Replace(
                    "{lastReceivedAt:HH}", lastReceivedAt.ToString("HH")
                ).Replace(
                    "{lastReceivedAt:tt}", lastReceivedAt.ToString("tt")
                ).Replace(
                    "{lastReceivedAt:mm}", lastReceivedAt.ToString("mm")
                ).Replace(
                    "{lastReceivedAt:ss}", lastReceivedAt.ToString("ss")
                )
            );
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2LoginRewardOwnReceiveStatusLabel
    {
        private Gs2LoginRewardOwnReceiveStatusFetcher _fetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2LoginRewardOwnReceiveStatusFetcher>() ?? GetComponentInParent<Gs2LoginRewardOwnReceiveStatusFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2LoginRewardOwnReceiveStatusFetcher.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2LoginRewardOwnReceiveStatusFetcher>() ?? GetComponentInParent<Gs2LoginRewardOwnReceiveStatusFetcher>(true);
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

    public partial class Gs2LoginRewardOwnReceiveStatusLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2LoginRewardOwnReceiveStatusLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LoginRewardOwnReceiveStatusLabel
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