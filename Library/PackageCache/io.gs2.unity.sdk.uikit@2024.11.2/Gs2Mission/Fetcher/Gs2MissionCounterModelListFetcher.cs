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
using System.Linq;
using Gs2.Core.Exception;
using Gs2.Unity.Core.Exception;
using Gs2.Unity.Gs2Mission.Domain.Model;
using Gs2.Unity.Gs2Mission.Model;
using Gs2.Unity.Gs2Mission.ScriptableObject;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Mission.Context;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Mission.Fetcher
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Mission/CounterModel/Fetcher/Gs2MissionCounterModelListFetcher")]
    public partial class Gs2MissionCounterModelListFetcher : MonoBehaviour
    {
        private EzNamespaceDomain _domain;
        private ulong? _callbackId;

        private IEnumerator Load() {
            var retryWaitSecond = 1;
            var it = _domain.CounterModels();
            var items = new List<Gs2.Unity.Gs2Mission.Model.EzCounterModel>();
            while (it.HasNext())
            {
                yield return it.Next();
                if (it.Error != null)
                {
                    if (it.Error is BadRequestException || it.Error is NotFoundException)
                    {
                        onError.Invoke(it.Error, null);
                        Debug.LogError($"{gameObject.GetFullPath()}: {it.Error.Message}");
                        break;
                    }
                    else {
                        onError.Invoke(new CanIgnoreException(it.Error), null);
                    }
                    yield return new WaitForSeconds(retryWaitSecond);
                    retryWaitSecond *= 2;
                }
                else {
                    if (it.Current != null)
                    {
                        items.Add(it.Current);
                    } else {
                        break;
                    }
                }
            }

            retryWaitSecond = 1;
            CounterModels = items;
            Fetched = true;

            this.OnFetched.Invoke();
        }

        private IEnumerator Fetch()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);
            yield return new WaitUntil(() => Context != null && Context.Namespace != null);

            this._domain = clientHolder.Gs2.Mission.Namespace(
                this.Context.Namespace.NamespaceName
            );
            this._callbackId = this._domain.SubscribeCounterModels(
                items =>
                {
                    CounterModels = items.ToList();
                    this.OnFetched.Invoke();
                }
            );

            yield return Load();
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
            this._domain.UnsubscribeCounterModels(
                this._callbackId.Value
            );
            this._callbackId = null;
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2MissionCounterModelListFetcher
    {
        public Gs2MissionNamespaceContext Context { get; private set; }

        public UnityEvent OnFetched = new UnityEvent();

        public void Awake()
        {
            Context = GetComponent<Gs2MissionNamespaceContext>() ?? GetComponentInParent<Gs2MissionNamespaceContext>();
            if (Context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MissionNamespaceContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            Context = GetComponent<Gs2MissionNamespaceContext>() ?? GetComponentInParent<Gs2MissionNamespaceContext>(true);
            if (Context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2MissionCounterModelListFetcher
    {
        public List<Gs2.Unity.Gs2Mission.Model.EzCounterModel> CounterModels { get; private set; }
        public bool Fetched { get; private set; }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2MissionCounterModelListFetcher
    {

    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MissionCounterModelListFetcher
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