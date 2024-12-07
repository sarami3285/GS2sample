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
using Gs2.Gs2JobQueue.Request;
using Gs2.Unity.Gs2JobQueue.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2JobQueue.Context;
using Gs2.Unity.UiKit.Gs2JobQueue.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2JobQueue.Label
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/JobQueue/Job/View/Label/Transaction/Gs2JobQueueDeleteJobByUserIdLabel")]
    public partial class Gs2JobQueueDeleteJobByUserIdLabel : MonoBehaviour
    {
        private void OnFetched()
        {
            if (this._userDataFetcher == null) {
                var context = gameObject.AddComponent<Gs2JobQueueOwnJobContext>();
                context.SetOwnJob(
                    OwnJob.New(
                        Namespace.New(
                            this._fetcher.Request.NamespaceName
                        ),
                        this._fetcher.Request.JobName
                    )
                );
                this._userDataFetcher = gameObject.AddComponent<Gs2JobQueueOwnJobFetcher>();
                this._userDataFetcher.OnFetched.AddListener(this._onFetched);
            }
            if ((!this._fetcher?.Fetched ?? false) || this._fetcher.Request == null) {
                return;
            }
            if ((!this._userDataFetcher?.Fetched ?? false) || this._userDataFetcher.Job == null) {
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
                    "{jobName}",
                    $"{this._fetcher.Request.JobName}"
                ).Replace(
                    "{timeOffsetToken}",
                    $"{this._fetcher.Request.TimeOffsetToken}"
                ).Replace(
                    "{userData:jobId}",
                    $"{this._userDataFetcher.Job.JobId}"
                ).Replace(
                    "{userData:scriptId}",
                    $"{this._userDataFetcher.Job.ScriptId}"
                ).Replace(
                    "{userData:args}",
                    $"{this._userDataFetcher.Job.Args}"
                ).Replace(
                    "{userData:currentRetryCount}",
                    $"{this._userDataFetcher.Job.CurrentRetryCount}"
                ).Replace(
                    "{userData:maxTryCount}",
                    $"{this._userDataFetcher.Job.MaxTryCount}"
                )
            );
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2JobQueueDeleteJobByUserIdLabel
    {
        private Gs2JobQueueDeleteJobByUserIdFetcher _fetcher;
        private Gs2JobQueueOwnJobFetcher _userDataFetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2JobQueueDeleteJobByUserIdFetcher>() ?? GetComponentInParent<Gs2JobQueueDeleteJobByUserIdFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2JobQueueDeleteJobByUserIdFetcher.");
                enabled = false;
            }
            this._userDataFetcher = GetComponent<Gs2JobQueueOwnJobFetcher>() ?? GetComponentInParent<Gs2JobQueueOwnJobFetcher>();
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2JobQueueDeleteJobByUserIdFetcher>() ?? GetComponentInParent<Gs2JobQueueDeleteJobByUserIdFetcher>(true);
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

    public partial class Gs2JobQueueDeleteJobByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2JobQueueDeleteJobByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2JobQueueDeleteJobByUserIdLabel
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