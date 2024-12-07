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
using Gs2.Unity.Gs2Formation.Model;
using Gs2.Unity.Gs2Key.ScriptableObject;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Formation.Context;
using UnityEngine;
using UnityEngine.Events;
using Form = Gs2.Unity.Gs2Formation.ScriptableObject.OwnForm;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Formation
{
    public partial class Gs2FormationFormSetFormAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onSetFormStart.Invoke();

            
            var domain = clientHolder.Gs2.Formation.Namespace(
                this._context.Form.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            ).Mold(
                this._context.Form.MoldModelName
            ).Form(
                this._context.Form.Index
            );
            var future = domain.SetFormFuture(
                Slots.ToArray(),
                Key.Grn
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

                        this.onSetFormComplete.Invoke(future3.Result);
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

            this.onSetFormComplete.Invoke(future2.Result);
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

    public partial class Gs2FormationFormSetFormAction
    {
        private Gs2FormationOwnFormContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2FormationOwnFormContext>() ?? GetComponentInParent<Gs2FormationOwnFormContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FormationOwnFormContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2FormationOwnFormContext>() ?? GetComponentInParent<Gs2FormationOwnFormContext>(true);
            if (this._context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FormationFormSetFormAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2FormationFormSetFormAction
    {
        public bool WaitAsyncProcessComplete;
        public List<Gs2.Unity.Gs2Formation.Model.EzSlotWithSignature> Slots;
        public Key Key;

        public void SetSlots(List<Gs2.Unity.Gs2Formation.Model.EzSlotWithSignature> value) {
            this.Slots = value;
            this.onChangeSlots.Invoke(this.Slots);
            this.OnChange.Invoke();
        }

        public void SetKeyId(Key value) {
            this.Key = value;
            this.onChangeKeyId.Invoke(this.Key);
            this.OnChange.Invoke();
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FormationFormSetFormAction
    {

        [Serializable]
        private class ChangeSlotsEvent : UnityEvent<List<Gs2.Unity.Gs2Formation.Model.EzSlotWithSignature>>
        {

        }

        [SerializeField]
        private ChangeSlotsEvent onChangeSlots = new ChangeSlotsEvent();
        public event UnityAction<List<Gs2.Unity.Gs2Formation.Model.EzSlotWithSignature>> OnChangeSlots
        {
            add => this.onChangeSlots.AddListener(value);
            remove => this.onChangeSlots.RemoveListener(value);
        }

        [Serializable]
        private class ChangeKeyIdEvent : UnityEvent<Key>
        {

        }

        [SerializeField]
        private ChangeKeyIdEvent onChangeKeyId = new ChangeKeyIdEvent();
        public event UnityAction<Key> OnChangeKeyId
        {
            add => this.onChangeKeyId.AddListener(value);
            remove => this.onChangeKeyId.RemoveListener(value);
        }

        [Serializable]
        private class SetFormStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private SetFormStartEvent onSetFormStart = new SetFormStartEvent();

        public event UnityAction OnSetFormStart
        {
            add => this.onSetFormStart.AddListener(value);
            remove => this.onSetFormStart.RemoveListener(value);
        }

        [Serializable]
        private class SetFormCompleteEvent : UnityEvent<EzForm>
        {

        }

        [SerializeField]
        private SetFormCompleteEvent onSetFormComplete = new SetFormCompleteEvent();
        public event UnityAction<EzForm> OnSetFormComplete
        {
            add => this.onSetFormComplete.AddListener(value);
            remove => this.onSetFormComplete.RemoveListener(value);
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
    public partial class Gs2FormationFormSetFormAction
    {
        [MenuItem("GameObject/Game Server Services/Formation/Form/Action/SetForm", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2FormationFormSetFormAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Formation/Prefabs/Action/Gs2FormationFormSetFormAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}