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
// ReSharper disable UnusedAutoPropertyAccessor.Local
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
using Gs2.Core.Exception;
using Gs2.Unity.Gs2Exchange.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Exchange.Context;
using UnityEngine;
using UnityEngine.Events;
using User = Gs2.Unity.Gs2Exchange.ScriptableObject.User;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Exchange
{
    public partial class Gs2ExchangeExchangeExchangeAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onExchangeStart.Invoke();

            
            var domain = clientHolder.Gs2.Exchange.Namespace(
                this._context.RateModel.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            ).Exchange(
            );
            var future = domain.ExchangeFuture(
                this._context.RateModel.RateName,
                Count,
                Config.ToArray()
            );
            yield return future;
            if (future.Error != null)
            {
                if (future.Error is TransactionException e)
                {
                    IEnumerator Retry()
                    {
                        var retryFuture = e.Retry();
                        yield return retryFuture;
                        if (retryFuture.Error != null)
                        {
                            this.onError.Invoke(future.Error, Retry);
                            yield break;
                        }
                        this.onExchangeComplete.Invoke();
                    }

                    this.onError.Invoke(future.Error, Retry);
                    yield break;
                }

                this.onError.Invoke(future.Error, null);
                yield break;
            }
            if (this.WaitAsyncProcessComplete && future.Result != null) {
                var transaction = future.Result;
                var future2 = transaction.WaitFuture();
                yield return future2;
            }
            this.onExchangeComplete.Invoke();
        }

        public void OnEnable()
        {
            Gs2ClientHolder.Instance.StartCoroutine(Process());
        }

        public void OnDisable()
        {
            
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ExchangeExchangeExchangeAction
    {
        private Gs2ExchangeRateModelContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2ExchangeRateModelContext>() ?? GetComponentInParent<Gs2ExchangeRateModelContext>();
            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ExchangeRateModelContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2ExchangeRateModelContext>() ?? GetComponentInParent<Gs2ExchangeRateModelContext>(true);
            if (_context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ExchangeExchangeExchangeAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2ExchangeExchangeExchangeAction
    {
        public bool WaitAsyncProcessComplete;
        public int Count;
        public List<Gs2.Unity.Gs2Exchange.Model.EzConfig> Config;

        public void SetCount(int value) {
            this.Count = value;
            this.onChangeCount.Invoke(this.Count);
            this.OnChange.Invoke();
        }

        public void DecreaseCount() {
            this.Count -= 1;
            this.onChangeCount.Invoke(this.Count);
            this.OnChange.Invoke();
        }

        public void IncreaseCount() {
            this.Count += 1;
            this.onChangeCount.Invoke(this.Count);
            this.OnChange.Invoke();
        }

        public void SetConfig(List<Gs2.Unity.Gs2Exchange.Model.EzConfig> value) {
            this.Config = value;
            this.onChangeConfig.Invoke(this.Config);
            this.OnChange.Invoke();
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExchangeExchangeExchangeAction
    {

        [Serializable]
        private class ChangeCountEvent : UnityEvent<int>
        {

        }

        [SerializeField]
        private ChangeCountEvent onChangeCount = new ChangeCountEvent();
        public event UnityAction<int> OnChangeCount
        {
            add => this.onChangeCount.AddListener(value);
            remove => this.onChangeCount.RemoveListener(value);
        }

        [Serializable]
        private class ChangeConfigEvent : UnityEvent<List<Gs2.Unity.Gs2Exchange.Model.EzConfig>>
        {

        }

        [SerializeField]
        private ChangeConfigEvent onChangeConfig = new ChangeConfigEvent();
        public event UnityAction<List<Gs2.Unity.Gs2Exchange.Model.EzConfig>> OnChangeConfig
        {
            add => this.onChangeConfig.AddListener(value);
            remove => this.onChangeConfig.RemoveListener(value);
        }

        [Serializable]
        private class ExchangeStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private ExchangeStartEvent onExchangeStart = new ExchangeStartEvent();

        public event UnityAction OnExchangeStart
        {
            add => this.onExchangeStart.AddListener(value);
            remove => this.onExchangeStart.RemoveListener(value);
        }

        [Serializable]
        private class ExchangeCompleteEvent : UnityEvent
        {

        }

        [SerializeField]
        private ExchangeCompleteEvent onExchangeComplete = new ExchangeCompleteEvent();
        public event UnityAction OnExchangeComplete
        {
            add => this.onExchangeComplete.AddListener(value);
            remove => this.onExchangeComplete.RemoveListener(value);
        }

        public UnityEvent OnChange = new UnityEvent();

        [SerializeField]
        internal ErrorEvent onError = new ErrorEvent();

        public event UnityAction<Gs2Exception, Func<IEnumerator>> OnError
        {
            add => this.onError.AddListener(value);
            remove => this.onError.RemoveListener(value);
        }
    }

#if UNITY_EDITOR

    /// <summary>
    /// Context Menu
    /// </summary>
    public partial class Gs2ExchangeExchangeExchangeAction
    {
        [MenuItem("GameObject/Game Server Services/Exchange/Exchange/Action/Exchange", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2ExchangeExchangeExchangeAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Exchange/Prefabs/Action/Gs2ExchangeExchangeExchangeAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}