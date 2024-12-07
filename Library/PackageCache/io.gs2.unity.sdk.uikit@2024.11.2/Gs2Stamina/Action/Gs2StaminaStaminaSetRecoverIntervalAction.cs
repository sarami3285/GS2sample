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
using Gs2.Unity.Gs2Stamina.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Stamina.Context;
using UnityEngine;
using UnityEngine.Events;
using Stamina = Gs2.Unity.Gs2Stamina.ScriptableObject.OwnStamina;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Stamina
{
    public partial class Gs2StaminaStaminaSetRecoverIntervalAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onSetRecoverIntervalStart.Invoke();

            
            var domain = clientHolder.Gs2.Stamina.Namespace(
                this._context.Stamina.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            ).Stamina(
                this._context.Stamina.StaminaName
            );
            var future = domain.SetRecoverIntervalFuture(
                SignedStatusBody,
                SignedStatusSignature,
                KeyId
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

                        this.onSetRecoverIntervalComplete.Invoke(future3.Result);
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

            this.onSetRecoverIntervalComplete.Invoke(future2.Result);
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

    public partial class Gs2StaminaStaminaSetRecoverIntervalAction
    {
        private Gs2StaminaOwnStaminaContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2StaminaOwnStaminaContext>() ?? GetComponentInParent<Gs2StaminaOwnStaminaContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2StaminaOwnStaminaContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2StaminaOwnStaminaContext>() ?? GetComponentInParent<Gs2StaminaOwnStaminaContext>(true);
            if (this._context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2StaminaStaminaSetRecoverIntervalAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2StaminaStaminaSetRecoverIntervalAction
    {
        public bool WaitAsyncProcessComplete;
        public string KeyId;
        public string SignedStatusBody;
        public string SignedStatusSignature;

        public void SetKeyId(string value) {
            this.KeyId = value;
            this.onChangeKeyId.Invoke(this.KeyId);
            this.OnChange.Invoke();
        }

        public void SetSignedStatusBody(string value) {
            this.SignedStatusBody = value;
            this.onChangeSignedStatusBody.Invoke(this.SignedStatusBody);
            this.OnChange.Invoke();
        }

        public void SetSignedStatusSignature(string value) {
            this.SignedStatusSignature = value;
            this.onChangeSignedStatusSignature.Invoke(this.SignedStatusSignature);
            this.OnChange.Invoke();
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2StaminaStaminaSetRecoverIntervalAction
    {

        [Serializable]
        private class ChangeKeyIdEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeKeyIdEvent onChangeKeyId = new ChangeKeyIdEvent();
        public event UnityAction<string> OnChangeKeyId
        {
            add => this.onChangeKeyId.AddListener(value);
            remove => this.onChangeKeyId.RemoveListener(value);
        }

        [Serializable]
        private class ChangeSignedStatusBodyEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeSignedStatusBodyEvent onChangeSignedStatusBody = new ChangeSignedStatusBodyEvent();
        public event UnityAction<string> OnChangeSignedStatusBody
        {
            add => this.onChangeSignedStatusBody.AddListener(value);
            remove => this.onChangeSignedStatusBody.RemoveListener(value);
        }

        [Serializable]
        private class ChangeSignedStatusSignatureEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeSignedStatusSignatureEvent onChangeSignedStatusSignature = new ChangeSignedStatusSignatureEvent();
        public event UnityAction<string> OnChangeSignedStatusSignature
        {
            add => this.onChangeSignedStatusSignature.AddListener(value);
            remove => this.onChangeSignedStatusSignature.RemoveListener(value);
        }

        [Serializable]
        private class SetRecoverIntervalStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private SetRecoverIntervalStartEvent onSetRecoverIntervalStart = new SetRecoverIntervalStartEvent();

        public event UnityAction OnSetRecoverIntervalStart
        {
            add => this.onSetRecoverIntervalStart.AddListener(value);
            remove => this.onSetRecoverIntervalStart.RemoveListener(value);
        }

        [Serializable]
        private class SetRecoverIntervalCompleteEvent : UnityEvent<EzStamina>
        {

        }

        [SerializeField]
        private SetRecoverIntervalCompleteEvent onSetRecoverIntervalComplete = new SetRecoverIntervalCompleteEvent();
        public event UnityAction<EzStamina> OnSetRecoverIntervalComplete
        {
            add => this.onSetRecoverIntervalComplete.AddListener(value);
            remove => this.onSetRecoverIntervalComplete.RemoveListener(value);
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
    public partial class Gs2StaminaStaminaSetRecoverIntervalAction
    {
        [MenuItem("GameObject/Game Server Services/Stamina/Stamina/Action/SetRecoverInterval", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2StaminaStaminaSetRecoverIntervalAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Stamina/Prefabs/Action/Gs2StaminaStaminaSetRecoverIntervalAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}