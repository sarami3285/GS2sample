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
using Gs2.Unity.Gs2Account.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Account.Context;
using UnityEngine;
using UnityEngine.Events;
using Account = Gs2.Unity.Gs2Account.ScriptableObject.OwnAccount;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Account
{
    public partial class Gs2AccountTakeOverAddTakeOverSettingAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onAddTakeOverSettingStart.Invoke();

            
            var domain = clientHolder.Gs2.Account.Namespace(
                this._context.Account.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            ).TakeOver(
                this.Type
            );
            var future = domain.AddTakeOverSettingFuture(
                UserIdentifier,
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

                        this.onAddTakeOverSettingComplete.Invoke(future3.Result);
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

            this.onAddTakeOverSettingComplete.Invoke(future2.Result);
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

    public partial class Gs2AccountTakeOverAddTakeOverSettingAction
    {
        private Gs2AccountOwnAccountContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2AccountOwnAccountContext>() ?? GetComponentInParent<Gs2AccountOwnAccountContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2AccountOwnAccountContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2AccountOwnAccountContext>() ?? GetComponentInParent<Gs2AccountOwnAccountContext>(true);
            if (this._context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2AccountTakeOverAddTakeOverSettingAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2AccountTakeOverAddTakeOverSettingAction
    {
        public bool WaitAsyncProcessComplete;
        public int Type;
        public string UserIdentifier;
        public string Password;

        public void SetType(int value) {
            this.Type = value;
            this.onChangeType.Invoke(this.Type);
            this.OnChange.Invoke();
        }

        public void DecreaseType() {
            this.Type -= 1;
            this.onChangeType.Invoke(this.Type);
            this.OnChange.Invoke();
        }

        public void IncreaseType() {
            this.Type += 1;
            this.onChangeType.Invoke(this.Type);
            this.OnChange.Invoke();
        }

        public void SetUserIdentifier(string value) {
            this.UserIdentifier = value;
            this.onChangeUserIdentifier.Invoke(this.UserIdentifier);
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
    public partial class Gs2AccountTakeOverAddTakeOverSettingAction
    {

        [Serializable]
        private class ChangeTypeEvent : UnityEvent<int>
        {

        }

        [SerializeField]
        private ChangeTypeEvent onChangeType = new ChangeTypeEvent();
        public event UnityAction<int> OnChangeType
        {
            add => this.onChangeType.AddListener(value);
            remove => this.onChangeType.RemoveListener(value);
        }

        [Serializable]
        private class ChangeUserIdentifierEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeUserIdentifierEvent onChangeUserIdentifier = new ChangeUserIdentifierEvent();
        public event UnityAction<string> OnChangeUserIdentifier
        {
            add => this.onChangeUserIdentifier.AddListener(value);
            remove => this.onChangeUserIdentifier.RemoveListener(value);
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
        private class AddTakeOverSettingStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private AddTakeOverSettingStartEvent onAddTakeOverSettingStart = new AddTakeOverSettingStartEvent();

        public event UnityAction OnAddTakeOverSettingStart
        {
            add => this.onAddTakeOverSettingStart.AddListener(value);
            remove => this.onAddTakeOverSettingStart.RemoveListener(value);
        }

        [Serializable]
        private class AddTakeOverSettingCompleteEvent : UnityEvent<EzTakeOver>
        {

        }

        [SerializeField]
        private AddTakeOverSettingCompleteEvent onAddTakeOverSettingComplete = new AddTakeOverSettingCompleteEvent();
        public event UnityAction<EzTakeOver> OnAddTakeOverSettingComplete
        {
            add => this.onAddTakeOverSettingComplete.AddListener(value);
            remove => this.onAddTakeOverSettingComplete.RemoveListener(value);
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
    public partial class Gs2AccountTakeOverAddTakeOverSettingAction
    {
        [MenuItem("GameObject/Game Server Services/Account/TakeOver/Action/AddTakeOverSetting", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2AccountTakeOverAddTakeOverSettingAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Account/Prefabs/Action/Gs2AccountTakeOverAddTakeOverSettingAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}