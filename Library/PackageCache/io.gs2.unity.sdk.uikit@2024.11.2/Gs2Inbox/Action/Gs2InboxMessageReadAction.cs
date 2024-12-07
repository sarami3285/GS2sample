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
    public partial class Gs2InboxMessageReadAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onReadStart.Invoke();

            
            var domain = clientHolder.Gs2.Inbox.Namespace(
                this._context.Message.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            ).Message(
                this._context.Message.MessageName
            );
            var future = domain.ReadFuture(
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
                        this.onReadComplete.Invoke();
                    }

                    this.onError.Invoke(future.Error, Retry);
                    yield break;
                }

                this.onError.Invoke(future.Error, null);
                yield break;
            }
            if (future.Result != null) {
                if (this.WaitAsyncProcessComplete && future.Result != null) {
                    var transaction = future.Result;
                    var future2 = transaction.WaitFuture();
                    yield return future2;
                }
            }
            this.onReadComplete.Invoke();
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

    public partial class Gs2InboxMessageReadAction
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

    public partial class Gs2InboxMessageReadAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2InboxMessageReadAction
    {
        public bool WaitAsyncProcessComplete;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InboxMessageReadAction
    {

        [Serializable]
        private class ReadStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private ReadStartEvent onReadStart = new ReadStartEvent();

        public event UnityAction OnReadStart
        {
            add => this.onReadStart.AddListener(value);
            remove => this.onReadStart.RemoveListener(value);
        }

        [Serializable]
        private class ReadCompleteEvent : UnityEvent
        {

        }

        [SerializeField]
        private ReadCompleteEvent onReadComplete = new ReadCompleteEvent();
        public event UnityAction OnReadComplete
        {
            add => this.onReadComplete.AddListener(value);
            remove => this.onReadComplete.RemoveListener(value);
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
    public partial class Gs2InboxMessageReadAction
    {
        [MenuItem("GameObject/Game Server Services/Inbox/Message/Action/Read", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2InboxMessageReadAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Inbox/Prefabs/Action/Gs2InboxMessageReadAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}