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
using Gs2.Gs2StateMachine.Request;
using Gs2.Unity.Gs2StateMachine.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2StateMachine.Context;
using Gs2.Unity.UiKit.Gs2StateMachine.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2StateMachine.Label
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/StateMachine/Status/View/Label/Transaction/Gs2StateMachineStartStateMachineByUserIdLabel")]
    public partial class Gs2StateMachineStartStateMachineByUserIdLabel : MonoBehaviour
    {
        private void OnFetched()
        {
            if ((!this._fetcher?.Fetched ?? false) || this._fetcher.Request == null) {
                return;
            }
            if ((!this._userDataFetcher?.Fetched ?? false) || this._userDataFetcher.Status == null) {
                return;
            }
            this.onUpdate?.Invoke(
                this.format.Replace(
                    "{namespaceName}",
                    $"{this._fetcher.Request.NamespaceName}"
                ).Replace(
                    "{userId}",
                    $"{this._fetcher.Request.UserId}"
                ).Replace(
                    "{args}",
                    $"{this._fetcher.Request.Args}"
                ).Replace(
                    "{ttl}",
                    $"{this._fetcher.Request.Ttl}"
                ).Replace(
                    "{timeOffsetToken}",
                    $"{this._fetcher.Request.TimeOffsetToken}"
                ).Replace(
                    "{userData:statusId}",
                    $"{this._userDataFetcher.Status.StatusId}"
                ).Replace(
                    "{userData:name}",
                    $"{this._userDataFetcher.Status.Name}"
                ).Replace(
                    "{userData:enableSpeculativeExecution}",
                    $"{this._userDataFetcher.Status.EnableSpeculativeExecution}"
                ).Replace(
                    "{userData:stateMachineDefinition}",
                    $"{this._userDataFetcher.Status.StateMachineDefinition}"
                ).Replace(
                    "{userData:randomStatus}",
                    $"{this._userDataFetcher.Status.RandomStatus}"
                ).Replace(
                    "{userData:stacks}",
                    $"{this._userDataFetcher.Status.Stacks}"
                ).Replace(
                    "{userData:variables}",
                    $"{this._userDataFetcher.Status.Variables}"
                ).Replace(
                    "{userData:status}",
                    $"{this._userDataFetcher.Status.Status}"
                ).Replace(
                    "{userData:lastError}",
                    $"{this._userDataFetcher.Status.LastError}"
                ).Replace(
                    "{userData:transitionCount}",
                    $"{this._userDataFetcher.Status.TransitionCount}"
                )
            );
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2StateMachineStartStateMachineByUserIdLabel
    {
        private Gs2StateMachineStartStateMachineByUserIdFetcher _fetcher;
        private Gs2StateMachineOwnStatusFetcher _userDataFetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2StateMachineStartStateMachineByUserIdFetcher>() ?? GetComponentInParent<Gs2StateMachineStartStateMachineByUserIdFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2StateMachineStartStateMachineByUserIdFetcher.");
                enabled = false;
            }
            this._userDataFetcher = GetComponent<Gs2StateMachineOwnStatusFetcher>() ?? GetComponentInParent<Gs2StateMachineOwnStatusFetcher>();
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2StateMachineStartStateMachineByUserIdFetcher>() ?? GetComponentInParent<Gs2StateMachineStartStateMachineByUserIdFetcher>(true);
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

    public partial class Gs2StateMachineStartStateMachineByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2StateMachineStartStateMachineByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2StateMachineStartStateMachineByUserIdLabel
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