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
using Gs2.Unity.Gs2Ranking.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Ranking.Context;
using UnityEngine;
using UnityEngine.Events;
using Subscribe = Gs2.Unity.Gs2Ranking.ScriptableObject.OwnSubscribe;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Ranking
{
    public partial class Gs2RankingSubscribeSubscribeAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onSubscribeStart.Invoke();

            
            var domain = clientHolder.Gs2.Ranking.Namespace(
                this._context.Subscribe.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            ).RankingCategory(
                this._context.Subscribe.CategoryName
            );
            var future = domain.SubscribeFuture(
                TargetUserId
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
                        var future3 = future.Result.ModelFuture();
                        yield return future3;
                        if (future3.Error != null)
                        {
                            this.onError.Invoke(future3.Error, null);
                            yield break;
                        }

                        this.onSubscribeComplete.Invoke(future3.Result);
                    }

                    this.onError.Invoke(future.Error, Retry);
                    yield break;
                }

                this.onError.Invoke(future.Error, null);
                yield break;
            }
            var future2 = future.Result.ModelFuture();
            yield return future2;
            if (future2.Error != null)
            {
                this.onError.Invoke(future2.Error, null);
                yield break;
            }

            this.onSubscribeComplete.Invoke(future2.Result);
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

    public partial class Gs2RankingSubscribeSubscribeAction
    {
        private Gs2RankingOwnSubscribeContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2RankingOwnSubscribeContext>() ?? GetComponentInParent<Gs2RankingOwnSubscribeContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2RankingOwnSubscribeContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2RankingOwnSubscribeContext>() ?? GetComponentInParent<Gs2RankingOwnSubscribeContext>(true);
            if (this._context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2RankingSubscribeSubscribeAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2RankingSubscribeSubscribeAction
    {
        public bool WaitAsyncProcessComplete;
        public string TargetUserId;

        public void SetTargetUserId(string value) {
            this.TargetUserId = value;
            this.onChangeTargetUserId.Invoke(this.TargetUserId);
            this.OnChange.Invoke();
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2RankingSubscribeSubscribeAction
    {

        [Serializable]
        private class ChangeTargetUserIdEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeTargetUserIdEvent onChangeTargetUserId = new ChangeTargetUserIdEvent();
        public event UnityAction<string> OnChangeTargetUserId
        {
            add => this.onChangeTargetUserId.AddListener(value);
            remove => this.onChangeTargetUserId.RemoveListener(value);
        }

        [Serializable]
        private class SubscribeStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private SubscribeStartEvent onSubscribeStart = new SubscribeStartEvent();

        public event UnityAction OnSubscribeStart
        {
            add => this.onSubscribeStart.AddListener(value);
            remove => this.onSubscribeStart.RemoveListener(value);
        }

        [Serializable]
        private class SubscribeCompleteEvent : UnityEvent<EzSubscribeUser>
        {

        }

        [SerializeField]
        private SubscribeCompleteEvent onSubscribeComplete = new SubscribeCompleteEvent();
        public event UnityAction<EzSubscribeUser> OnSubscribeComplete
        {
            add => this.onSubscribeComplete.AddListener(value);
            remove => this.onSubscribeComplete.RemoveListener(value);
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
    public partial class Gs2RankingSubscribeSubscribeAction
    {
        [MenuItem("GameObject/Game Server Services/Ranking/Subscribe/Action/Subscribe", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2RankingSubscribeSubscribeAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Ranking/Prefabs/Action/Gs2RankingSubscribeSubscribeAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}