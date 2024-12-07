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
    public partial class Gs2ExchangeExchangeIncrementalExchangeAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onIncrementalExchangeStart.Invoke();

            
            var domain = clientHolder.Gs2.Exchange.Namespace(
                this._context.Namespace.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            ).Exchange(
            );
            var future = domain.IncrementalExchangeFuture(
                RateName,
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
                        this.onIncrementalExchangeComplete.Invoke();
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
            this.onIncrementalExchangeComplete.Invoke();
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

    public partial class Gs2ExchangeExchangeIncrementalExchangeAction
    {
        private Gs2ExchangeNamespaceContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2ExchangeNamespaceContext>() ?? GetComponentInParent<Gs2ExchangeNamespaceContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ExchangeNamespaceContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2ExchangeNamespaceContext>() ?? GetComponentInParent<Gs2ExchangeNamespaceContext>(true);
            if (this._context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ExchangeExchangeIncrementalExchangeAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2ExchangeExchangeIncrementalExchangeAction
    {
        public bool WaitAsyncProcessComplete;
        public string RateName;
        public int Count;
        public List<Gs2.Unity.Gs2Exchange.Model.EzConfig> Config;

        public void SetRateName(string value) {
            this.RateName = value;
            this.onChangeRateName.Invoke(this.RateName);
            this.OnChange.Invoke();
        }

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
    public partial class Gs2ExchangeExchangeIncrementalExchangeAction
    {

        [Serializable]
        private class ChangeRateNameEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeRateNameEvent onChangeRateName = new ChangeRateNameEvent();
        public event UnityAction<string> OnChangeRateName
        {
            add => this.onChangeRateName.AddListener(value);
            remove => this.onChangeRateName.RemoveListener(value);
        }

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
        private class IncrementalExchangeStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private IncrementalExchangeStartEvent onIncrementalExchangeStart = new IncrementalExchangeStartEvent();

        public event UnityAction OnIncrementalExchangeStart
        {
            add => this.onIncrementalExchangeStart.AddListener(value);
            remove => this.onIncrementalExchangeStart.RemoveListener(value);
        }

        [Serializable]
        private class IncrementalExchangeCompleteEvent : UnityEvent
        {

        }

        [SerializeField]
        private IncrementalExchangeCompleteEvent onIncrementalExchangeComplete = new IncrementalExchangeCompleteEvent();
        public event UnityAction OnIncrementalExchangeComplete
        {
            add => this.onIncrementalExchangeComplete.AddListener(value);
            remove => this.onIncrementalExchangeComplete.RemoveListener(value);
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
    public partial class Gs2ExchangeExchangeIncrementalExchangeAction
    {
        [MenuItem("GameObject/Game Server Services/Exchange/Exchange/Action/IncrementalExchange", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2ExchangeExchangeIncrementalExchangeAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Exchange/Prefabs/Action/Gs2ExchangeExchangeIncrementalExchangeAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}