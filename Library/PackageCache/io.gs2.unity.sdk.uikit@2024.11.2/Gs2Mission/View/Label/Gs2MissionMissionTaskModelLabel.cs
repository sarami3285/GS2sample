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
using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Mission
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Mission/MissionTaskModel/View/Label/Gs2MissionMissionTaskModelLabel")]
    public partial class Gs2MissionMissionTaskModelLabel : MonoBehaviour
    {
        private void OnFetched()
        {
            this.onUpdate?.Invoke(
                this.format.Replace(
                    "{name}", $"{this._fetcher?.MissionTaskModel?.Name}"
                ).Replace(
                    "{metadata}", $"{this._fetcher?.MissionTaskModel?.Metadata}"
                ).Replace(
                    "{verifyCompleteType}", $"{this._fetcher?.MissionTaskModel?.VerifyCompleteType}"
                ).Replace(
                    "{targetCounter}", $"{this._fetcher?.MissionTaskModel?.TargetCounter}"
                ).Replace(
                    "{verifyCompleteConsumeActions}", $"{this._fetcher?.MissionTaskModel?.VerifyCompleteConsumeActions}"
                ).Replace(
                    "{completeAcquireActions}", $"{this._fetcher?.MissionTaskModel?.CompleteAcquireActions}"
                ).Replace(
                    "{challengePeriodEventId}", $"{this._fetcher?.MissionTaskModel?.ChallengePeriodEventId}"
                ).Replace(
                    "{premiseMissionTaskName}", $"{this._fetcher?.MissionTaskModel?.PremiseMissionTaskName}"
                ).Replace(
                    "{counterName}", $"{this._fetcher?.MissionTaskModel?.CounterName}"
                ).Replace(
                    "{targetResetType}", $"{this._fetcher?.MissionTaskModel?.TargetResetType}"
                ).Replace(
                    "{targetValue}", $"{this._fetcher?.MissionTaskModel?.TargetValue}"
                )
            );
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2MissionMissionTaskModelLabel
    {
        private Gs2MissionMissionTaskModelFetcher _fetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2MissionMissionTaskModelFetcher>() ?? GetComponentInParent<Gs2MissionMissionTaskModelFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MissionMissionTaskModelFetcher.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2MissionMissionTaskModelFetcher>() ?? GetComponentInParent<Gs2MissionMissionTaskModelFetcher>(true);
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

    public partial class Gs2MissionMissionTaskModelLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MissionMissionTaskModelLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MissionMissionTaskModelLabel
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