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
using Gs2.Unity.Gs2Inbox.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Inbox.Context;
using UnityEngine;
using UnityEngine.Events;
using Message = Gs2.Unity.Gs2Inbox.ScriptableObject.OwnMessage;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Inbox
{
    public partial class Gs2InboxMessageReceiveGlobalMessageAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onReceiveGlobalMessageStart.Invoke();

            
            var domain = clientHolder.Gs2.Inbox.Namespace(
                this._context.Message.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            );
            var future = domain.ReceiveGlobalMessageFuture(
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
                        var items2 = new List<EzMessage>();
                        foreach (var domain_ in future.Result) {
                            var future3 = domain_.ModelFuture();
                            yield return future3;
                            if (future3.Error != null)
                            {
                                this.onError.Invoke(future3.Error, null);
                                yield break;
                            }
                            items2.Add(future3.Result);
                        }

                        this.onReceiveGlobalMessageComplete.Invoke(items2);
                    }

                    this.onError.Invoke(future.Error, Retry);
                    yield break;
                }

                this.onError.Invoke(future.Error, null);
                yield break;
            }
            var items = new List<EzMessage>();
            foreach (var domain_ in future.Result) {
                var future2 = domain_.ModelFuture();
                yield return future2;
                if (future2.Error != null)
                {
                    this.onError.Invoke(future2.Error, null);
                    yield break;
                }
                items.Add(future2.Result);
            }

            this.onReceiveGlobalMessageComplete.Invoke(items);
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

    public partial class Gs2InboxMessageReceiveGlobalMessageAction
    {
        private Gs2InboxOwnMessageContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2InboxOwnMessageContext>() ?? GetComponentInParent<Gs2InboxOwnMessageContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InboxOwnMessageContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2InboxOwnMessageContext>() ?? GetComponentInParent<Gs2InboxOwnMessageContext>(true);
            if (this._context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2InboxMessageReceiveGlobalMessageAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2InboxMessageReceiveGlobalMessageAction
    {
        public bool WaitAsyncProcessComplete;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InboxMessageReceiveGlobalMessageAction
    {

        [Serializable]
        private class ReceiveGlobalMessageStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private ReceiveGlobalMessageStartEvent onReceiveGlobalMessageStart = new ReceiveGlobalMessageStartEvent();

        public event UnityAction OnReceiveGlobalMessageStart
        {
            add => this.onReceiveGlobalMessageStart.AddListener(value);
            remove => this.onReceiveGlobalMessageStart.RemoveListener(value);
        }

        [Serializable]
        private class ReceiveGlobalMessageCompleteEvent : UnityEvent<List<EzMessage>>
        {

        }

        [SerializeField]
        private ReceiveGlobalMessageCompleteEvent onReceiveGlobalMessageComplete = new ReceiveGlobalMessageCompleteEvent();
        public event UnityAction<List<EzMessage>> OnReceiveGlobalMessageComplete
        {
            add => this.onReceiveGlobalMessageComplete.AddListener(value);
            remove => this.onReceiveGlobalMessageComplete.RemoveListener(value);
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
    public partial class Gs2InboxMessageReceiveGlobalMessageAction
    {
        [MenuItem("GameObject/Game Server Services/Inbox/Message/Action/ReceiveGlobalMessage", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2InboxMessageReceiveGlobalMessageAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Inbox/Prefabs/Action/Gs2InboxMessageReceiveGlobalMessageAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}