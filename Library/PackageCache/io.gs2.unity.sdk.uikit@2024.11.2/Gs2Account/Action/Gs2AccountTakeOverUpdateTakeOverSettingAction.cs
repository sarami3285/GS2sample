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
using Gs2.Unity.Gs2Account.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Account.Context;
using UnityEngine;
using UnityEngine.Events;
using TakeOver = Gs2.Unity.Gs2Account.ScriptableObject.OwnTakeOver;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Account
{
    public partial class Gs2AccountTakeOverUpdateTakeOverSettingAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onUpdateTakeOverSettingStart.Invoke();

            
            var domain = clientHolder.Gs2.Account.Namespace(
                this._context.TakeOver.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            ).TakeOver(
                this._context.TakeOver.Type
            );
            var future = domain.UpdateTakeOverSettingFuture(
                OldPassword,
                Password
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

                        this.onUpdateTakeOverSettingComplete.Invoke(future3.Result);
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

            this.onUpdateTakeOverSettingComplete.Invoke(future2.Result);
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

    public partial class Gs2AccountTakeOverUpdateTakeOverSettingAction
    {
        private Gs2AccountOwnTakeOverContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2AccountOwnTakeOverContext>() ?? GetComponentInParent<Gs2AccountOwnTakeOverContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2AccountOwnTakeOverContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2AccountOwnTakeOverContext>() ?? GetComponentInParent<Gs2AccountOwnTakeOverContext>(true);
            if (this._context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2AccountTakeOverUpdateTakeOverSettingAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2AccountTakeOverUpdateTakeOverSettingAction
    {
        public bool WaitAsyncProcessComplete;
        public string OldPassword;
        public string Password;

        public void SetOldPassword(string value) {
            this.OldPassword = value;
            this.onChangeOldPassword.Invoke(this.OldPassword);
            this.OnChange.Invoke();
        }

        public void SetPassword(string value) {
            this.Password = value;
            this.onChangePassword.Invoke(this.Password);
            this.OnChange.Invoke();
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2AccountTakeOverUpdateTakeOverSettingAction
    {

        [Serializable]
        private class ChangeOldPasswordEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeOldPasswordEvent onChangeOldPassword = new ChangeOldPasswordEvent();
        public event UnityAction<string> OnChangeOldPassword
        {
            add => this.onChangeOldPassword.AddListener(value);
            remove => this.onChangeOldPassword.RemoveListener(value);
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
        private class UpdateTakeOverSettingStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private UpdateTakeOverSettingStartEvent onUpdateTakeOverSettingStart = new UpdateTakeOverSettingStartEvent();

        public event UnityAction OnUpdateTakeOverSettingStart
        {
            add => this.onUpdateTakeOverSettingStart.AddListener(value);
            remove => this.onUpdateTakeOverSettingStart.RemoveListener(value);
        }

        [Serializable]
        private class UpdateTakeOverSettingCompleteEvent : UnityEvent<EzTakeOver>
        {

        }

        [SerializeField]
        private UpdateTakeOverSettingCompleteEvent onUpdateTakeOverSettingComplete = new UpdateTakeOverSettingCompleteEvent();
        public event UnityAction<EzTakeOver> OnUpdateTakeOverSettingComplete
        {
            add => this.onUpdateTakeOverSettingComplete.AddListener(value);
            remove => this.onUpdateTakeOverSettingComplete.RemoveListener(value);
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
    public partial class Gs2AccountTakeOverUpdateTakeOverSettingAction
    {
        [MenuItem("GameObject/Game Server Services/Account/TakeOver/Action/UpdateTakeOverSetting", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2AccountTakeOverUpdateTakeOverSettingAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Account/Prefabs/Action/Gs2AccountTakeOverUpdateTakeOverSettingAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}