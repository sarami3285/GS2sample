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
using Gs2.Unity.Gs2Chat.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Chat.Context;
using UnityEngine;
using UnityEngine.Events;
using User = Gs2.Unity.Gs2Chat.ScriptableObject.User;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Chat
{
    public partial class Gs2ChatRoomCreateRoomAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onCreateRoomStart.Invoke();

            
            var domain = clientHolder.Gs2.Chat.Namespace(
                this._context.Namespace.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            );
            var future = domain.CreateRoomFuture(
                Name,
                Metadata,
                Password,
                WhiteListUserIds.ToArray()
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

                        this.onCreateRoomComplete.Invoke(future3.Result);
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

            this.onCreateRoomComplete.Invoke(future2.Result);
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

    public partial class Gs2ChatRoomCreateRoomAction
    {
        private Gs2ChatNamespaceContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2ChatNamespaceContext>() ?? GetComponentInParent<Gs2ChatNamespaceContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ChatNamespaceContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2ChatNamespaceContext>() ?? GetComponentInParent<Gs2ChatNamespaceContext>(true);
            if (this._context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ChatRoomCreateRoomAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2ChatRoomCreateRoomAction
    {
        public bool WaitAsyncProcessComplete;
        public string Name;
        public string Metadata;
        public string Password;
        public List<string> WhiteListUserIds;

        public void SetName(string value) {
            this.Name = value;
            this.onChangeName.Invoke(this.Name);
            this.OnChange.Invoke();
        }

        public void SetMetadata(string value) {
            this.Metadata = value;
            this.onChangeMetadata.Invoke(this.Metadata);
            this.OnChange.Invoke();
        }

        public void SetPassword(string value) {
            this.Password = value;
            this.onChangePassword.Invoke(this.Password);
            this.OnChange.Invoke();
        }

        public void SetWhiteListUserIds(List<string> value) {
            this.WhiteListUserIds = value;
            this.onChangeWhiteListUserIds.Invoke(this.WhiteListUserIds);
            this.OnChange.Invoke();
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ChatRoomCreateRoomAction
    {

        [Serializable]
        private class ChangeNameEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeNameEvent onChangeName = new ChangeNameEvent();
        public event UnityAction<string> OnChangeName
        {
            add => this.onChangeName.AddListener(value);
            remove => this.onChangeName.RemoveListener(value);
        }

        [Serializable]
        private class ChangeMetadataEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeMetadataEvent onChangeMetadata = new ChangeMetadataEvent();
        public event UnityAction<string> OnChangeMetadata
        {
            add => this.onChangeMetadata.AddListener(value);
            remove => this.onChangeMetadata.RemoveListener(value);
        }

        [Serializable]
        private class ChangePasswordEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangePasswordEvent onChangePassword = new ChangePasswordEvent();
        public event UnityAction<string> OnChangePassword
        {
            add => this.onChangePassword.AddListener(value);
            remove => this.onChangePassword.RemoveListener(value);
        }

        [Serializable]
        private class ChangeWhiteListUserIdsEvent : UnityEvent<List<string>>
        {

        }

        [SerializeField]
        private ChangeWhiteListUserIdsEvent onChangeWhiteListUserIds = new ChangeWhiteListUserIdsEvent();
        public event UnityAction<List<string>> OnChangeWhiteListUserIds
        {
            add => this.onChangeWhiteListUserIds.AddListener(value);
            remove => this.onChangeWhiteListUserIds.RemoveListener(value);
        }

        [Serializable]
        private class CreateRoomStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private CreateRoomStartEvent onCreateRoomStart = new CreateRoomStartEvent();

        public event UnityAction OnCreateRoomStart
        {
            add => this.onCreateRoomStart.AddListener(value);
            remove => this.onCreateRoomStart.RemoveListener(value);
        }

        [Serializable]
        private class CreateRoomCompleteEvent : UnityEvent<EzRoom>
        {

        }

        [SerializeField]
        private CreateRoomCompleteEvent onCreateRoomComplete = new CreateRoomCompleteEvent();
        public event UnityAction<EzRoom> OnCreateRoomComplete
        {
            add => this.onCreateRoomComplete.AddListener(value);
            remove => this.onCreateRoomComplete.RemoveListener(value);
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
    public partial class Gs2ChatRoomCreateRoomAction
    {
        [MenuItem("GameObject/Game Server Services/Chat/Room/Action/CreateRoom", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2ChatRoomCreateRoomAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Chat/Prefabs/Action/Gs2ChatRoomCreateRoomAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}