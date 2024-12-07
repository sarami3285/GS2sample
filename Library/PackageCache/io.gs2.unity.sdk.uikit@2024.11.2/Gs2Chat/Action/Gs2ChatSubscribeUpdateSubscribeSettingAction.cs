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
using Subscribe = Gs2.Unity.Gs2Chat.ScriptableObject.OwnSubscribe;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Chat
{
    public partial class Gs2ChatSubscribeUpdateSubscribeSettingAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onUpdateSubscribeSettingStart.Invoke();

            
            var domain = clientHolder.Gs2.Chat.Namespace(
                this._context.Subscribe.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            ).Subscribe(
                this._context.Subscribe.RoomName
            );
            var future = domain.UpdateSubscribeSettingFuture(
                NotificationTypes.ToArray()
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

                        this.onUpdateSubscribeSettingComplete.Invoke(future3.Result);
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

            this.onUpdateSubscribeSettingComplete.Invoke(future2.Result);
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

    public partial class Gs2ChatSubscribeUpdateSubscribeSettingAction
    {
        private Gs2ChatOwnSubscribeContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2ChatOwnSubscribeContext>() ?? GetComponentInParent<Gs2ChatOwnSubscribeContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ChatOwnSubscribeContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2ChatOwnSubscribeContext>() ?? GetComponentInParent<Gs2ChatOwnSubscribeContext>(true);
            if (this._context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ChatSubscribeUpdateSubscribeSettingAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2ChatSubscribeUpdateSubscribeSettingAction
    {
        public bool WaitAsyncProcessComplete;
        public List<Gs2.Unity.Gs2Chat.Model.EzNotificationType> NotificationTypes;

        public void SetNotificationTypes(List<Gs2.Unity.Gs2Chat.Model.EzNotificationType> value) {
            this.NotificationTypes = value;
            this.onChangeNotificationTypes.Invoke(this.NotificationTypes);
            this.OnChange.Invoke();
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ChatSubscribeUpdateSubscribeSettingAction
    {

        [Serializable]
        private class ChangeNotificationTypesEvent : UnityEvent<List<Gs2.Unity.Gs2Chat.Model.EzNotificationType>>
        {

        }

        [SerializeField]
        private ChangeNotificationTypesEvent onChangeNotificationTypes = new ChangeNotificationTypesEvent();
        public event UnityAction<List<Gs2.Unity.Gs2Chat.Model.EzNotificationType>> OnChangeNotificationTypes
        {
            add => this.onChangeNotificationTypes.AddListener(value);
            remove => this.onChangeNotificationTypes.RemoveListener(value);
        }

        [Serializable]
        private class UpdateSubscribeSettingStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private UpdateSubscribeSettingStartEvent onUpdateSubscribeSettingStart = new UpdateSubscribeSettingStartEvent();

        public event UnityAction OnUpdateSubscribeSettingStart
        {
            add => this.onUpdateSubscribeSettingStart.AddListener(value);
            remove => this.onUpdateSubscribeSettingStart.RemoveListener(value);
        }

        [Serializable]
        private class UpdateSubscribeSettingCompleteEvent : UnityEvent<EzSubscribe>
        {

        }

        [SerializeField]
        private UpdateSubscribeSettingCompleteEvent onUpdateSubscribeSettingComplete = new UpdateSubscribeSettingCompleteEvent();
        public event UnityAction<EzSubscribe> OnUpdateSubscribeSettingComplete
        {
            add => this.onUpdateSubscribeSettingComplete.AddListener(value);
            remove => this.onUpdateSubscribeSettingComplete.RemoveListener(value);
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
    public partial class Gs2ChatSubscribeUpdateSubscribeSettingAction
    {
        [MenuItem("GameObject/Game Server Services/Chat/Subscribe/Action/UpdateSubscribeSetting", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2ChatSubscribeUpdateSubscribeSettingAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Chat/Prefabs/Action/Gs2ChatSubscribeUpdateSubscribeSettingAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}