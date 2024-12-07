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
using Gs2.Unity.Gs2StateMachine.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2StateMachine.Context;
using UnityEngine;
using UnityEngine.Events;
using Status = Gs2.Unity.Gs2StateMachine.ScriptableObject.OwnStatus;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2StateMachine
{
    public partial class Gs2StateMachineStatusEmitAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onEmitStart.Invoke();

            
            var domain = clientHolder.Gs2.StateMachine.Namespace(
                this._context.Status.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            ).Status(
                this._context.Status.StatusName
            );
            var future = domain.EmitFuture(
                EventName,
                Args
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

                        this.onEmitComplete.Invoke(future3.Result);
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

            this.onEmitComplete.Invoke(future2.Result);
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

    public partial class Gs2StateMachineStatusEmitAction
    {
        private Gs2StateMachineOwnStatusContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2StateMachineOwnStatusContext>() ?? GetComponentInParent<Gs2StateMachineOwnStatusContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2StateMachineOwnStatusContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2StateMachineOwnStatusContext>() ?? GetComponentInParent<Gs2StateMachineOwnStatusContext>(true);
            if (this._context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2StateMachineStatusEmitAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2StateMachineStatusEmitAction
    {
        public bool WaitAsyncProcessComplete;
        public string EventName;
        public string Args;

        public void SetEventName(string value) {
            this.EventName = value;
            this.onChangeEventName.Invoke(this.EventName);
            this.OnChange.Invoke();
        }

        public void SetArgs(string value) {
            this.Args = value;
            this.onChangeArgs.Invoke(this.Args);
            this.OnChange.Invoke();
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2StateMachineStatusEmitAction
    {

        [Serializable]
        private class ChangeEventNameEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeEventNameEvent onChangeEventName = new ChangeEventNameEvent();
        public event UnityAction<string> OnChangeEventName
        {
            add => this.onChangeEventName.AddListener(value);
            remove => this.onChangeEventName.RemoveListener(value);
        }

        [Serializable]
        private class ChangeArgsEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeArgsEvent onChangeArgs = new ChangeArgsEvent();
        public event UnityAction<string> OnChangeArgs
        {
            add => this.onChangeArgs.AddListener(value);
            remove => this.onChangeArgs.RemoveListener(value);
        }

        [Serializable]
        private class EmitStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private EmitStartEvent onEmitStart = new EmitStartEvent();

        public event UnityAction OnEmitStart
        {
            add => this.onEmitStart.AddListener(value);
            remove => this.onEmitStart.RemoveListener(value);
        }

        [Serializable]
        private class EmitCompleteEvent : UnityEvent<EzStatus>
        {

        }

        [SerializeField]
        private EmitCompleteEvent onEmitComplete = new EmitCompleteEvent();
        public event UnityAction<EzStatus> OnEmitComplete
        {
            add => this.onEmitComplete.AddListener(value);
            remove => this.onEmitComplete.RemoveListener(value);
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
    public partial class Gs2StateMachineStatusEmitAction
    {
        [MenuItem("GameObject/Game Server Services/StateMachine/Status/Action/Emit", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2StateMachineStatusEmitAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2StateMachine/Prefabs/Action/Gs2StateMachineStatusEmitAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}