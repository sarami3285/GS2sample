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
 *
 * deny overwrite
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
using System.Linq;
using System.Text;
using Gs2.Core.Exception;
using Gs2.Unity.Core.Exception;
using Gs2.Unity.Gs2Showcase.Domain.Model;
using Gs2.Unity.Gs2Showcase.Model;
using Gs2.Unity.Gs2Showcase.ScriptableObject;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Showcase.Context;
using UnityEngine;
using UnityEngine.Events;
using EzAcquireAction = Gs2.Unity.Core.Model.EzAcquireAction;
using EzConsumeAction = Gs2.Unity.Core.Model.EzConsumeAction;

namespace Gs2.Unity.UiKit.Gs2Showcase.Fetcher
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Showcase/DisplayItem/Fetcher/Gs2ShowcaseOwnDisplayItemFetcher")]
    public partial class Gs2ShowcaseOwnDisplayItemFetcher : MonoBehaviour, IAcquireActionsFetcher, IConsumeActionsFetcher
    {
        private EzDisplayItemGameSessionDomain _domain;
        private ulong? _callbackId;

        private IEnumerator Fetch()
        {
            var retryWaitSecond = 1;
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);
            yield return new WaitUntil(() => Context != null && this.Context.DisplayItem != null);

            _domain = clientHolder.Gs2.Showcase.Namespace(
                this.Context.DisplayItem.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            ).Showcase(
                this.Context.DisplayItem.ShowcaseName
            ).DisplayItem(
                this.Context.DisplayItem.DisplayItemId
            );
            this._callbackId = this._domain.Subscribe(
                item =>
                {
                    DisplayItem = item;
                    Fetched = true;
                    this.OnFetched.Invoke();
                }
            );

            while (true) {
                var future = this._domain.ModelFuture();
                yield return future;
                if (future.Error != null) {
                    yield return new WaitForSeconds(retryWaitSecond);
                    retryWaitSecond *= 2;
                }
                else {
                    DisplayItem = future.Result;
                    Fetched = true;
                    this.OnFetched.Invoke();
                    break;
                }
            }
        }

        public List<EzAcquireAction> AcquireActions(string context = "default") {
            if (!Fetched) {
                return new List<Unity.Core.Model.EzAcquireAction>();
            }
            return DisplayItem.SalesItem.AcquireActions;
        }

        bool IAcquireActionsFetcher.IsFetched() {
            return Fetched;
        }

        UnityEvent IAcquireActionsFetcher.OnFetchedEvent() {
            return this.OnFetched;
        }

        GameObject IAcquireActionsFetcher.GameObject() {
            return gameObject;
        }

        public List<EzConsumeAction> ConsumeActions(string context = "default") {
            if (!Fetched) {
                return new List<Unity.Core.Model.EzConsumeAction>();
            }
            return DisplayItem.SalesItem.ConsumeActions;
        }

        bool IConsumeActionsFetcher.IsFetched() {
            return Fetched;
        }

        UnityEvent IConsumeActionsFetcher.OnFetchedEvent() {
            return this.OnFetched;
        }

        GameObject IConsumeActionsFetcher.GameObject() {
            return gameObject;
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ShowcaseOwnDisplayItemFetcher
    {
        public Gs2ShowcaseOwnDisplayItemContext Context { get; private set; }

        public void Awake()
        {
            Context = GetComponent<Gs2ShowcaseOwnDisplayItemContext>() ?? GetComponentInParent<Gs2ShowcaseOwnDisplayItemContext>();
            if (Context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ShowcaseOwnDisplayItemContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            Context = GetComponent<Gs2ShowcaseOwnDisplayItemContext>() ?? GetComponentInParent<Gs2ShowcaseOwnDisplayItemContext>(true);
            if (Context == null) {
                return true;
            }
            return false;
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
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ShowcaseOwnDisplayItemFetcher
    {
        public EzDisplayItem DisplayItem { get; protected set; }
        public bool Fetched { get; protected set; }
        public UnityEvent OnFetched = new UnityEvent();
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ShowcaseOwnDisplayItemFetcher
    {

    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ShowcaseOwnDisplayItemFetcher
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