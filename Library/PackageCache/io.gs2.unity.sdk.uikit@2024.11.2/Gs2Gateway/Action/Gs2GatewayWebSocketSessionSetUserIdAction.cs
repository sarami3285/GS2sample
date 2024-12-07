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
using Gs2.Unity.Gs2Gateway.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Gateway.Context;
using UnityEngine;
using UnityEngine.Events;
using WebSocketSession = Gs2.Unity.Gs2Gateway.ScriptableObject.OwnWebSocketSession;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Gateway
{
    public partial class Gs2GatewayWebSocketSessionSetUserIdAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onSetUserIdStart.Invoke();

            
            var domain = clientHolder.Gs2.Gateway.Namespace(
                this._context.WebSocketSession.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            ).WebSocketSession(
            );
            var future = domain.SetUserIdFuture(
                AllowConcurrentAccess
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

                        this.onSetUserIdComplete.Invoke(future3.Result);
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

            this.onSetUserIdComplete.Invoke(future2.Result);
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

    public partial class Gs2GatewayWebSocketSessionSetUserIdAction
    {
        private Gs2GatewayOwnWebSocketSessionContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2GatewayOwnWebSocketSessionContext>() ?? GetComponentInParent<Gs2GatewayOwnWebSocketSessionContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2GatewayOwnWebSocketSessionContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2GatewayOwnWebSocketSessionContext>() ?? GetComponentInParent<Gs2GatewayOwnWebSocketSessionContext>(true);
            if (this._context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2GatewayWebSocketSessionSetUserIdAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2GatewayWebSocketSessionSetUserIdAction
    {
        public bool WaitAsyncProcessComplete;
        public bool AllowConcurrentAccess;

        public void SetAllowConcurrentAccess(bool value) {
            this.AllowConcurrentAccess = value;
            this.onChangeAllowConcurrentAccess.Invoke(this.AllowConcurrentAccess);
            this.OnChange.Invoke();
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2GatewayWebSocketSessionSetUserIdAction
    {

        [Serializable]
        private class ChangeAllowConcurrentAccessEvent : UnityEvent<bool>
        {

        }

        [SerializeField]
        private ChangeAllowConcurrentAccessEvent onChangeAllowConcurrentAccess = new ChangeAllowConcurrentAccessEvent();
        public event UnityAction<bool> OnChangeAllowConcurrentAccess
        {
            add => this.onChangeAllowConcurrentAccess.AddListener(value);
            remove => this.onChangeAllowConcurrentAccess.RemoveListener(value);
        }

        [Serializable]
        private class SetUserIdStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private SetUserIdStartEvent onSetUserIdStart = new SetUserIdStartEvent();

        public event UnityAction OnSetUserIdStart
        {
            add => this.onSetUserIdStart.AddListener(value);
            remove => this.onSetUserIdStart.RemoveListener(value);
        }

        [Serializable]
        private class SetUserIdCompleteEvent : UnityEvent<EzWebSocketSession>
        {

        }

        [SerializeField]
        private SetUserIdCompleteEvent onSetUserIdComplete = new SetUserIdCompleteEvent();
        public event UnityAction<EzWebSocketSession> OnSetUserIdComplete
        {
            add => this.onSetUserIdComplete.AddListener(value);
            remove => this.onSetUserIdComplete.RemoveListener(value);
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
    public partial class Gs2GatewayWebSocketSessionSetUserIdAction
    {
        [MenuItem("GameObject/Game Server Services/Gateway/WebSocketSession/Action/SetUserId", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2GatewayWebSocketSessionSetUserIdAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Gateway/Prefabs/Action/Gs2GatewayWebSocketSessionSetUserIdAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}