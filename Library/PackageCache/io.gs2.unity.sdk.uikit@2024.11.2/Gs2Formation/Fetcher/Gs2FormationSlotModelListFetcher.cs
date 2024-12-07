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
using Gs2.Unity.Gs2Formation.Domain.Model;
using Gs2.Unity.Gs2Formation.Model;
using Gs2.Unity.Gs2Formation.ScriptableObject;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Formation.Context;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Formation.Fetcher
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Formation/SlotModel/Fetcher/Gs2FormationSlotModelListFetcher")]
    public partial class Gs2FormationSlotModelListFetcher : MonoBehaviour
    {
        private EzMoldModelDomain _domain;
        private ulong? _callbackId;

        private IEnumerator Fetch()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);
            yield return new WaitUntil(() => Context != null && Context.FormModel != null);

            this._domain = clientHolder.Gs2.Formation.Namespace(
                this.Context.FormModel.NamespaceName
            ).MoldModel(
                this.Context.FormModel.MoldModelName
            );
            this._callbackId = this._domain.Subscribe(
                item =>
                {
                    SlotModels = item.FormModel.Slots;
                    Fetched = true;
                    this.OnFetched.Invoke();
                }
            );
            var future = this._domain.ModelFuture();
            yield return future;
            if (future.Error != null)
            {
                if (future.Error is BadRequestException || future.Error is NotFoundException)
                {
                    onError.Invoke(future.Error, null);
                    Debug.LogError($"{gameObject.GetFullPath()}: {future.Error.Message}");
                }
                else {
                    onError.Invoke(new CanIgnoreException(future.Error), null);
                }
                yield break;
            }

            SlotModels = future.Result.FormModel.Slots;
            Fetched = true;
            this.OnFetched.Invoke();
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
    /// Dependent components
    /// </summary>
    
    public partial class Gs2FormationSlotModelListFetcher
    {
        public Gs2FormationFormModelContext Context { get; private set; }

        public UnityEvent OnFetched = new UnityEvent();

        public void Awake()
        {
            Context = GetComponent<Gs2FormationFormModelContext>() ?? GetComponentInParent<Gs2FormationFormModelContext>();
            if (Context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FormationFormModelContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            Context = GetComponent<Gs2FormationFormModelContext>() ?? GetComponentInParent<Gs2FormationFormModelContext>(true);
            if (Context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2FormationSlotModelListFetcher
    {
        public List<Gs2.Unity.Gs2Formation.Model.EzSlotModel> SlotModels { get; private set; }
        public bool Fetched { get; private set; }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2FormationSlotModelListFetcher
    {

    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FormationSlotModelListFetcher
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