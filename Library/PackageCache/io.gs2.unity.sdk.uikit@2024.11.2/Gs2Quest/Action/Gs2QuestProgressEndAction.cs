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
using Gs2.Unity.Gs2Quest.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Quest.Context;
using UnityEngine;
using UnityEngine.Events;
using Progress = Gs2.Unity.Gs2Quest.ScriptableObject.OwnProgress;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Quest
{
    public partial class Gs2QuestProgressEndAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onEndStart.Invoke();

            
            var domain = clientHolder.Gs2.Quest.Namespace(
                this._context.Progress.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            ).Progress(
            );
            var future = domain.EndFuture(
                IsComplete,
                Rewards.ToArray(),
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
                        this.onEndComplete.Invoke();
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
            this.onEndComplete.Invoke();
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

    public partial class Gs2QuestProgressEndAction
    {
        private Gs2QuestOwnProgressContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2QuestOwnProgressContext>() ?? GetComponentInParent<Gs2QuestOwnProgressContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2QuestOwnProgressContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2QuestOwnProgressContext>() ?? GetComponentInParent<Gs2QuestOwnProgressContext>(true);
            if (this._context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2QuestProgressEndAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2QuestProgressEndAction
    {
        public bool WaitAsyncProcessComplete;
        public List<Gs2.Unity.Gs2Quest.Model.EzReward> Rewards;
        public bool IsComplete;
        public List<Gs2.Unity.Gs2Quest.Model.EzConfig> Config;

        public void SetRewards(List<Gs2.Unity.Gs2Quest.Model.EzReward> value) {
            this.Rewards = value;
            this.onChangeRewards.Invoke(this.Rewards);
            this.OnChange.Invoke();
        }

        public void SetIsComplete(bool value) {
            this.IsComplete = value;
            this.onChangeIsComplete.Invoke(this.IsComplete);
            this.OnChange.Invoke();
        }

        public void SetConfig(List<Gs2.Unity.Gs2Quest.Model.EzConfig> value) {
            this.Config = value;
            this.onChangeConfig.Invoke(this.Config);
            this.OnChange.Invoke();
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2QuestProgressEndAction
    {

        [Serializable]
        private class ChangeRewardsEvent : UnityEvent<List<Gs2.Unity.Gs2Quest.Model.EzReward>>
        {

        }

        [SerializeField]
        private ChangeRewardsEvent onChangeRewards = new ChangeRewardsEvent();
        public event UnityAction<List<Gs2.Unity.Gs2Quest.Model.EzReward>> OnChangeRewards
        {
            add => this.onChangeRewards.AddListener(value);
            remove => this.onChangeRewards.RemoveListener(value);
        }

        [Serializable]
        private class ChangeIsCompleteEvent : UnityEvent<bool>
        {

        }

        [SerializeField]
        private ChangeIsCompleteEvent onChangeIsComplete = new ChangeIsCompleteEvent();
        public event UnityAction<bool> OnChangeIsComplete
        {
            add => this.onChangeIsComplete.AddListener(value);
            remove => this.onChangeIsComplete.RemoveListener(value);
        }

        [Serializable]
        private class ChangeConfigEvent : UnityEvent<List<Gs2.Unity.Gs2Quest.Model.EzConfig>>
        {

        }

        [SerializeField]
        private ChangeConfigEvent onChangeConfig = new ChangeConfigEvent();
        public event UnityAction<List<Gs2.Unity.Gs2Quest.Model.EzConfig>> OnChangeConfig
        {
            add => this.onChangeConfig.AddListener(value);
            remove => this.onChangeConfig.RemoveListener(value);
        }

        [Serializable]
        private class EndStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private EndStartEvent onEndStart = new EndStartEvent();

        public event UnityAction OnEndStart
        {
            add => this.onEndStart.AddListener(value);
            remove => this.onEndStart.RemoveListener(value);
        }

        [Serializable]
        private class EndCompleteEvent : UnityEvent
        {

        }

        [SerializeField]
        private EndCompleteEvent onEndComplete = new EndCompleteEvent();
        public event UnityAction OnEndComplete
        {
            add => this.onEndComplete.AddListener(value);
            remove => this.onEndComplete.RemoveListener(value);
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
    public partial class Gs2QuestProgressEndAction
    {
        [MenuItem("GameObject/Game Server Services/Quest/Progress/Action/End", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2QuestProgressEndAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Quest/Prefabs/Action/Gs2QuestProgressEndAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}