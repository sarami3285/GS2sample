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
using Gs2.Gs2Mission.Request;
using Gs2.Unity.Gs2Mission.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Mission.Context;
using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Mission.Label
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Mission/Complete/View/Label/Transaction/Gs2MissionVerifyCompleteByUserIdLabel")]
    public partial class Gs2MissionVerifyCompleteByUserIdLabel : MonoBehaviour
    {
        private void OnFetched()
        {
            if (this._userDataFetcher == null) {
                var context = gameObject.AddComponent<Gs2MissionOwnCompleteContext>();
                context.SetOwnComplete(
                    OwnComplete.New(
                        Namespace.New(
                            this._fetcher.Request.NamespaceName
                        ),
                        this._fetcher.Request.MissionGroupName
                    )
                );
                this._userDataFetcher = gameObject.AddComponent<Gs2MissionOwnCompleteFetcher>();
                this._userDataFetcher.OnFetched.AddListener(this._onFetched);
            }
            if ((!this._fetcher?.Fetched ?? false) || this._fetcher.Request == null) {
                return;
            }
            if ((!this._userDataFetcher?.Fetched ?? false) || this._userDataFetcher.Complete == null) {
                return;
            }
            this.onUpdate?.Invoke(
                this.format.Replace(
                    "{namespaceName}",
                    $"{this._fetcher.Request.NamespaceName}"
                ).Replace(
                    "{missionGroupName}",
                    $"{this._fetcher.Request.MissionGroupName}"
                ).Replace(
                    "{userId}",
                    $"{this._fetcher.Request.UserId}"
                ).Replace(
                    "{verifyType}",
                    $"{this._fetcher.Request.VerifyType}"
                ).Replace(
                    "{missionTaskName}",
                    $"{this._fetcher.Request.MissionTaskName}"
                ).Replace(
                    "{multiplyValueSpecifyingQuantity}",
                    $"{this._fetcher.Request.MultiplyValueSpecifyingQuantity}"
                ).Replace(
                    "{timeOffsetToken}",
                    $"{this._fetcher.Request.TimeOffsetToken}"
                ).Replace(
                    "{userData:missionGroupName}",
                    $"{this._userDataFetcher.Complete.MissionGroupName}"
                ).Replace(
                    "{userData:completedMissionTaskNames}",
                    $"{this._userDataFetcher.Complete.CompletedMissionTaskNames}"
                ).Replace(
                    "{userData:receivedMissionTaskNames}",
                    $"{this._userDataFetcher.Complete.ReceivedMissionTaskNames}"
                )
            );
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2MissionVerifyCompleteByUserIdLabel
    {
        private Gs2MissionVerifyCompleteByUserIdFetcher _fetcher;
        private Gs2MissionOwnCompleteFetcher _userDataFetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2MissionVerifyCompleteByUserIdFetcher>() ?? GetComponentInParent<Gs2MissionVerifyCompleteByUserIdFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MissionVerifyCompleteByUserIdFetcher.");
                enabled = false;
            }
            this._userDataFetcher = GetComponent<Gs2MissionOwnCompleteFetcher>() ?? GetComponentInParent<Gs2MissionOwnCompleteFetcher>();
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2MissionVerifyCompleteByUserIdFetcher>() ?? GetComponentInParent<Gs2MissionVerifyCompleteByUserIdFetcher>(true);
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

    public partial class Gs2MissionVerifyCompleteByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MissionVerifyCompleteByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MissionVerifyCompleteByUserIdLabel
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