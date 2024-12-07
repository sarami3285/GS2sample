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
using Gs2.Unity.Gs2LoginReward.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2LoginReward.Context;
using UnityEngine;
using UnityEngine.Events;
using User = Gs2.Unity.Gs2LoginReward.ScriptableObject.User;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2LoginReward
{
    public partial class Gs2LoginRewardBonusReceiveAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onReceiveStart.Invoke();

            
            var domain = clientHolder.Gs2.LoginReward.Namespace(
                this._context.Namespace.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            ).Bonus(
            );
            var future = domain.ReceiveFuture(
                BonusModelName,
                Config.ToArray()
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
                        this.onReceiveComplete.Invoke();
                    }

                    this.onError.Invoke(future.Error, Retry);
                    yield break;
                }

                this.onError.Invoke(future.Error, null);
                yield break;
            }
            if (this.WaitAsyncProcessComplete && future.Result != null) {
                var transaction = future.Result;
                var future2 = transaction.WaitFuture();
                yield return future2;
            }
            this.onReceiveComplete.Invoke();
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

    public partial class Gs2LoginRewardBonusReceiveAction
    {
        private Gs2LoginRewardNamespaceContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2LoginRewardNamespaceContext>() ?? GetComponentInParent<Gs2LoginRewardNamespaceContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2LoginRewardNamespaceContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2LoginRewardNamespaceContext>() ?? GetComponentInParent<Gs2LoginRewardNamespaceContext>(true);
            if (this._context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2LoginRewardBonusReceiveAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2LoginRewardBonusReceiveAction
    {
        public bool WaitAsyncProcessComplete;
        public string BonusModelName;
        public List<Gs2.Unity.Gs2LoginReward.Model.EzConfig> Config;

        public void SetBonusModelName(string value) {
            this.BonusModelName = value;
            this.onChangeBonusModelName.Invoke(this.BonusModelName);
            this.OnChange.Invoke();
        }

        public void SetConfig(List<Gs2.Unity.Gs2LoginReward.Model.EzConfig> value) {
            this.Config = value;
            this.onChangeConfig.Invoke(this.Config);
            this.OnChange.Invoke();
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LoginRewardBonusReceiveAction
    {

        [Serializable]
        private class ChangeBonusModelNameEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeBonusModelNameEvent onChangeBonusModelName = new ChangeBonusModelNameEvent();
        public event UnityAction<string> OnChangeBonusModelName
        {
            add => this.onChangeBonusModelName.AddListener(value);
            remove => this.onChangeBonusModelName.RemoveListener(value);
        }

        [Serializable]
        private class ChangeConfigEvent : UnityEvent<List<Gs2.Unity.Gs2LoginReward.Model.EzConfig>>
        {

        }

        [SerializeField]
        private ChangeConfigEvent onChangeConfig = new ChangeConfigEvent();
        public event UnityAction<List<Gs2.Unity.Gs2LoginReward.Model.EzConfig>> OnChangeConfig
        {
            add => this.onChangeConfig.AddListener(value);
            remove => this.onChangeConfig.RemoveListener(value);
        }

        [Serializable]
        private class ReceiveStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private ReceiveStartEvent onReceiveStart = new ReceiveStartEvent();

        public event UnityAction OnReceiveStart
        {
            add => this.onReceiveStart.AddListener(value);
            remove => this.onReceiveStart.RemoveListener(value);
        }

        [Serializable]
        private class ReceiveCompleteEvent : UnityEvent
        {

        }

        [SerializeField]
        private ReceiveCompleteEvent onReceiveComplete = new ReceiveCompleteEvent();
        public event UnityAction OnReceiveComplete
        {
            add => this.onReceiveComplete.AddListener(value);
            remove => this.onReceiveComplete.RemoveListener(value);
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
    public partial class Gs2LoginRewardBonusReceiveAction
    {
        [MenuItem("GameObject/Game Server Services/LoginReward/Bonus/Action/Receive", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2LoginRewardBonusReceiveAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2LoginReward/Prefabs/Action/Gs2LoginRewardBonusReceiveAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}