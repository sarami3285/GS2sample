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
    public partial class Gs2QuestProgressStartAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onStartStart.Invoke();

            
            var domain = clientHolder.Gs2.Quest.Namespace(
                this._context.Progress.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            );
            var future = domain.StartFuture(
                QuestGroupName,
                QuestName,
                Force,
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
                        this.onStartComplete.Invoke();
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
            this.onStartComplete.Invoke();
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

    public partial class Gs2QuestProgressStartAction
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

    public partial class Gs2QuestProgressStartAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2QuestProgressStartAction
    {
        public bool WaitAsyncProcessComplete;
        public string QuestGroupName;
        public string QuestName;
        public bool Force;
        public List<Gs2.Unity.Gs2Quest.Model.EzConfig> Config;

        public void SetQuestGroupName(string value) {
            this.QuestGroupName = value;
            this.onChangeQuestGroupName.Invoke(this.QuestGroupName);
            this.OnChange.Invoke();
        }

        public void SetQuestName(string value) {
            this.QuestName = value;
            this.onChangeQuestName.Invoke(this.QuestName);
            this.OnChange.Invoke();
        }

        public void SetForce(bool value) {
            this.Force = value;
            this.onChangeForce.Invoke(this.Force);
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
    public partial class Gs2QuestProgressStartAction
    {

        [Serializable]
        private class ChangeQuestGroupNameEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeQuestGroupNameEvent onChangeQuestGroupName = new ChangeQuestGroupNameEvent();
        public event UnityAction<string> OnChangeQuestGroupName
        {
            add => this.onChangeQuestGroupName.AddListener(value);
            remove => this.onChangeQuestGroupName.RemoveListener(value);
        }

        [Serializable]
        private class ChangeQuestNameEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeQuestNameEvent onChangeQuestName = new ChangeQuestNameEvent();
        public event UnityAction<string> OnChangeQuestName
        {
            add => this.onChangeQuestName.AddListener(value);
            remove => this.onChangeQuestName.RemoveListener(value);
        }

        [Serializable]
        private class ChangeForceEvent : UnityEvent<bool>
        {

        }

        [SerializeField]
        private ChangeForceEvent onChangeForce = new ChangeForceEvent();
        public event UnityAction<bool> OnChangeForce
        {
            add => this.onChangeForce.AddListener(value);
            remove => this.onChangeForce.RemoveListener(value);
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
        private class StartStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private StartStartEvent onStartStart = new StartStartEvent();

        public event UnityAction OnStartStart
        {
            add => this.onStartStart.AddListener(value);
            remove => this.onStartStart.RemoveListener(value);
        }

        [Serializable]
        private class StartCompleteEvent : UnityEvent
        {

        }

        [SerializeField]
        private StartCompleteEvent onStartComplete = new StartCompleteEvent();
        public event UnityAction OnStartComplete
        {
            add => this.onStartComplete.AddListener(value);
            remove => this.onStartComplete.RemoveListener(value);
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
    public partial class Gs2QuestProgressStartAction
    {
        [MenuItem("GameObject/Game Server Services/Quest/Progress/Action/Start", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2QuestProgressStartAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Quest/Prefabs/Action/Gs2QuestProgressStartAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}