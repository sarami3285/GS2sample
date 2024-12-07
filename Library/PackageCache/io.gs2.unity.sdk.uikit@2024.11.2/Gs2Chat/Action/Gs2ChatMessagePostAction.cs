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
using Room = Gs2.Unity.Gs2Chat.ScriptableObject.Room;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Chat
{
    public partial class Gs2ChatMessagePostAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onPostStart.Invoke();

            
            var domain = clientHolder.Gs2.Chat.Namespace(
                this._context.Room.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            ).Room(
                this._context.Room.RoomName,
                this._context.Room.Password
            );
            var future = domain.PostFuture(
                Metadata,
                Category
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

                        this.onPostComplete.Invoke(future3.Result);
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

            this.onPostComplete.Invoke(future2.Result);
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

    public partial class Gs2ChatMessagePostAction
    {
        private Gs2ChatRoomContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2ChatRoomContext>() ?? GetComponentInParent<Gs2ChatRoomContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ChatRoomContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2ChatRoomContext>() ?? GetComponentInParent<Gs2ChatRoomContext>(true);
            if (this._context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ChatMessagePostAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2ChatMessagePostAction
    {
        public bool WaitAsyncProcessComplete;
        public int Category;
        public string Metadata;

        public void SetCategory(int value) {
            this.Category = value;
            this.onChangeCategory.Invoke(this.Category);
            this.OnChange.Invoke();
        }

        public void DecreaseCategory() {
            this.Category -= 1;
            this.onChangeCategory.Invoke(this.Category);
            this.OnChange.Invoke();
        }

        public void IncreaseCategory() {
            this.Category += 1;
            this.onChangeCategory.Invoke(this.Category);
            this.OnChange.Invoke();
        }

        public void SetMetadata(string value) {
            this.Metadata = value;
            this.onChangeMetadata.Invoke(this.Metadata);
            this.OnChange.Invoke();
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ChatMessagePostAction
    {

        [Serializable]
        private class ChangeCategoryEvent : UnityEvent<int>
        {

        }

        [SerializeField]
        private ChangeCategoryEvent onChangeCategory = new ChangeCategoryEvent();
        public event UnityAction<int> OnChangeCategory
        {
            add => this.onChangeCategory.AddListener(value);
            remove => this.onChangeCategory.RemoveListener(value);
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
        private class PostStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private PostStartEvent onPostStart = new PostStartEvent();

        public event UnityAction OnPostStart
        {
            add => this.onPostStart.AddListener(value);
            remove => this.onPostStart.RemoveListener(value);
        }

        [Serializable]
        private class PostCompleteEvent : UnityEvent<EzMessage>
        {

        }

        [SerializeField]
        private PostCompleteEvent onPostComplete = new PostCompleteEvent();
        public event UnityAction<EzMessage> OnPostComplete
        {
            add => this.onPostComplete.AddListener(value);
            remove => this.onPostComplete.RemoveListener(value);
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
    public partial class Gs2ChatMessagePostAction
    {
        [MenuItem("GameObject/Game Server Services/Chat/Message/Action/Post", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2ChatMessagePostAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Chat/Prefabs/Action/Gs2ChatMessagePostAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}