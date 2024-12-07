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

using System;
using System.Linq;
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

	[AddComponentMenu("GS2 UIKit/Mission/Counter/View/Progress/Gs2MissionMissionTaskProgress")]
    public partial class Gs2MissionMissionTaskProgress : MonoBehaviour
    {
        private void OnFetched()
        {
            if (_fetcher?.Counter == null) return;
            if (_groupModelFetcher?.MissionGroupModel == null) return;
            if (_modelFetcher?.MissionTaskModel == null) return;
            
            var scopedValue = this._fetcher.Counter.Values.FirstOrDefault(v => v.ResetType == this._groupModelFetcher.MissionGroupModel.ResetType);
            if (scopedValue.Value >= this._modelFetcher.MissionTaskModel.TargetValue) {
                onUpdate?.Invoke(1);
            }
            else {
                onUpdate?.Invoke(
                    Math.Min((float)scopedValue.Value / this._modelFetcher.MissionTaskModel.TargetValue, 1)
                );
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2MissionMissionTaskProgress
    {
        private Gs2MissionMissionGroupModelFetcher _groupModelFetcher;
        private Gs2MissionMissionTaskModelFetcher _modelFetcher;
        private Gs2MissionOwnCounterFetcher _fetcher;

        public void Awake()
        {
            this._groupModelFetcher = GetComponent<Gs2MissionMissionGroupModelFetcher>() ?? GetComponentInParent<Gs2MissionMissionGroupModelFetcher>();
            if (this._groupModelFetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MissionMissionGroupModelFetcher.");
                enabled = false;
            }

            this._modelFetcher = GetComponent<Gs2MissionMissionTaskModelFetcher>() ?? GetComponentInParent<Gs2MissionMissionTaskModelFetcher>();
            if (this._modelFetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MissionMissionTaskModelFetcher.");
                enabled = false;
            }

            this._fetcher = GetComponent<Gs2MissionOwnCounterFetcher>() ?? GetComponentInParent<Gs2MissionOwnCounterFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MissionOwnCounterFetcher.");
                enabled = false;
            }
        }

        public bool HasError()
        {
            this._groupModelFetcher = GetComponent<Gs2MissionMissionGroupModelFetcher>() ?? GetComponentInParent<Gs2MissionMissionGroupModelFetcher>(true);
            if (this._groupModelFetcher == null) {
                return true;
            }
            this._modelFetcher = GetComponent<Gs2MissionMissionTaskModelFetcher>() ?? GetComponentInParent<Gs2MissionMissionTaskModelFetcher>(true);
            if (this._modelFetcher == null) {
                return true;
            }
            this._fetcher = GetComponent<Gs2MissionOwnCounterFetcher>() ?? GetComponentInParent<Gs2MissionOwnCounterFetcher>(true);
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
            if (this._groupModelFetcher != null) {
                this._groupModelFetcher.OnFetched.AddListener(this._onFetched);
                if (this._groupModelFetcher.Fetched) {
                    OnFetched();
                }
            }
            if (this._modelFetcher != null) {
                this._modelFetcher.OnFetched.AddListener(this._onFetched);
                if (this._modelFetcher.Fetched) {
                    OnFetched();
                }
            }
        }

        public void OnDisable()
        {
            if (this._onFetched != null) {
                this._fetcher.OnFetched.RemoveListener(this._onFetched);
                if (this._groupModelFetcher != null) {
                    this._groupModelFetcher.OnFetched.RemoveListener(this._onFetched);
                }
                if (this._modelFetcher != null) {
                    this._modelFetcher.OnFetched.RemoveListener(this._onFetched);
                }
                this._onFetched = null;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MissionMissionTaskProgress
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MissionMissionTaskProgress
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MissionMissionTaskProgress
    {
        [Serializable]
        private class UpdateEvent : UnityEvent<float>
        {

        }

        [SerializeField]
        private UpdateEvent onUpdate = new UpdateEvent();

        public event UnityAction<float> OnUpdate
        {
            add => onUpdate.AddListener(value);
            remove => onUpdate.RemoveListener(value);
        }
    }
}