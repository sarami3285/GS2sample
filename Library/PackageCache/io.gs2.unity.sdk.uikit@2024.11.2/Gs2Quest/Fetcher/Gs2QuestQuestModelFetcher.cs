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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Gs2.Core.Exception;
using Gs2.Unity.Core.Exception;
using Gs2.Unity.Gs2Quest.Domain.Model;
using Gs2.Unity.Gs2Quest.Model;
using Gs2.Unity.Gs2Quest.ScriptableObject;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Core.Model;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Quest.Context;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Quest.Fetcher
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Quest/QuestModel/Fetcher/Gs2QuestQuestModelFetcher")]
    public partial class Gs2QuestQuestModelFetcher : MonoBehaviour, IAcquireActionsFetcher, IConsumeActionsFetcher, IVerifyActionsFetcher
    {
        private EzQuestModelDomain _domain;
        private ulong? _callbackId;

        private IEnumerator Fetch()
        {
            var retryWaitSecond = 1;
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);
            yield return new WaitUntil(() => Context != null && this.Context.QuestModel != null);

            this._domain = clientHolder.Gs2.Quest.Namespace(
                this.Context.QuestModel.NamespaceName
            ).QuestGroupModel(
                this.Context.QuestModel.QuestGroupName
            ).QuestModel(
                this.Context.QuestModel.QuestName
            );;
            var future = this._domain.SubscribeWithInitialCallFuture(
                item =>
                {
                    retryWaitSecond = 0;
                    QuestModel = item;
                    Fetched = true;
                    this.OnFetched.Invoke();
                }
            );
            yield return future;
            if (future.Error != null) {
                this.onError.Invoke(future.Error, null);
                yield break;
            }
            this._callbackId = future.Result;
        }

        public void OnUpdateContext() {
            OnDisable();
            Awake();
            OnEnable();
        }

        public void OnEnable()
        {
            StartCoroutine(nameof(Fetch));
            Context.OnUpdate.AddListener(OnUpdateContext);
        }

        public void OnDisable()
        {
            Context.OnUpdate.RemoveListener(OnUpdateContext);

            if (this._domain == null) {
                return;
            }
            if (!this._callbackId.HasValue) {
                return;
            }
            this._domain.Unsubscribe(
                this._callbackId.Value
            );
            this._callbackId = null;
        }

        public void SetTemporaryQuestModel(
            Gs2.Unity.Gs2Quest.Model.EzQuestModel questModel
        ) {
            QuestModel = questModel;
            this.OnFetched.Invoke();
        }

        public void RollbackTemporaryQuestModel(
        ) {
            OnUpdateContext();
        }

        public List<Unity.Core.Model.EzAcquireAction> AcquireActions(string context = "default") {
            if (!Fetched) {
                return new List<Unity.Core.Model.EzAcquireAction>();
            }
            return QuestModel.FirstCompleteAcquireActions.Denormalize();
        }

        bool IAcquireActionsFetcher.IsFetched() {
            return this.Fetched;
        }

        UnityEvent IAcquireActionsFetcher.OnFetchedEvent() {
            return this.OnFetched;
        }

        GameObject IAcquireActionsFetcher.GameObject() {
            return gameObject;
        }

        public List<Unity.Core.Model.EzConsumeAction> ConsumeActions(string context = "default") {
            if (!Fetched) {
                return new List<Unity.Core.Model.EzConsumeAction>();
            }
            return QuestModel.ConsumeActions.Denormalize();
        }

        bool IConsumeActionsFetcher.IsFetched() {
            return this.Fetched;
        }

        UnityEvent IConsumeActionsFetcher.OnFetchedEvent() {
            return this.OnFetched;
        }

        GameObject IConsumeActionsFetcher.GameObject() {
            return gameObject;
        }

        public List<Unity.Core.Model.EzVerifyAction> VerifyActions(string context = "default") {
            if (!Fetched) {
                return new List<Unity.Core.Model.EzVerifyAction>();
            }
            return QuestModel.VerifyActions.Denormalize();
        }

        bool IVerifyActionsFetcher.IsFetched() {
            return this.Fetched;
        }

        UnityEvent IVerifyActionsFetcher.OnFetchedEvent() {
            return this.OnFetched;
        }

        GameObject IVerifyActionsFetcher.GameObject() {
            return gameObject;
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2QuestQuestModelFetcher
    {
        public Gs2QuestQuestModelContext Context { get; private set; }

        public void Awake()
        {
            Context = GetComponent<Gs2QuestQuestModelContext>() ?? GetComponentInParent<Gs2QuestQuestModelContext>();
            if (Context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2QuestQuestModelContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            Context = GetComponent<Gs2QuestQuestModelContext>() ?? GetComponentInParent<Gs2QuestQuestModelContext>(true);
            if (Context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2QuestQuestModelFetcher
    {
        public Gs2.Unity.Gs2Quest.Model.EzQuestModel QuestModel { get; protected set; }
        public bool Fetched { get; protected set; }
        public UnityEvent OnFetched = new UnityEvent();
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2QuestQuestModelFetcher
    {

    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2QuestQuestModelFetcher
    {
        [SerializeField]
        internal ErrorEvent onError = new ErrorEvent();

        public event UnityAction<Gs2Exception, Func<IEnumerator>> OnError
        {
            add => onError.AddListener(value);
            remove => onError.RemoveListener(value);
        }
    }
}