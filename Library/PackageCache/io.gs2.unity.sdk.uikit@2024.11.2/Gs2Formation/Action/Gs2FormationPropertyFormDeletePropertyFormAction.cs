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
using Gs2.Unity.Gs2Formation.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Formation.Context;
using UnityEngine;
using UnityEngine.Events;
using PropertyForm = Gs2.Unity.Gs2Formation.ScriptableObject.OwnPropertyForm;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Formation
{
    public partial class Gs2FormationPropertyFormDeletePropertyFormAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onDeletePropertyFormStart.Invoke();

            
            var domain = clientHolder.Gs2.Formation.Namespace(
                this._context.PropertyForm.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            ).PropertyForm(
                this._context.PropertyForm.PropertyFormModelName,
                this._context.PropertyForm.PropertyId
            );
            var future = domain.DeletePropertyFormFuture(
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

                        this.onDeletePropertyFormComplete.Invoke(future3.Result);
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

            this.onDeletePropertyFormComplete.Invoke(future2.Result);
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

    public partial class Gs2FormationPropertyFormDeletePropertyFormAction
    {
        private Gs2FormationOwnPropertyFormContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2FormationOwnPropertyFormContext>() ?? GetComponentInParent<Gs2FormationOwnPropertyFormContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FormationOwnPropertyFormContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2FormationOwnPropertyFormContext>() ?? GetComponentInParent<Gs2FormationOwnPropertyFormContext>(true);
            if (this._context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FormationPropertyFormDeletePropertyFormAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2FormationPropertyFormDeletePropertyFormAction
    {
        public bool WaitAsyncProcessComplete;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FormationPropertyFormDeletePropertyFormAction
    {

        [Serializable]
        private class DeletePropertyFormStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private DeletePropertyFormStartEvent onDeletePropertyFormStart = new DeletePropertyFormStartEvent();

        public event UnityAction OnDeletePropertyFormStart
        {
            add => this.onDeletePropertyFormStart.AddListener(value);
            remove => this.onDeletePropertyFormStart.RemoveListener(value);
        }

        [Serializable]
        private class DeletePropertyFormCompleteEvent : UnityEvent<EzPropertyForm>
        {

        }

        [SerializeField]
        private DeletePropertyFormCompleteEvent onDeletePropertyFormComplete = new DeletePropertyFormCompleteEvent();
        public event UnityAction<EzPropertyForm> OnDeletePropertyFormComplete
        {
            add => this.onDeletePropertyFormComplete.AddListener(value);
            remove => this.onDeletePropertyFormComplete.RemoveListener(value);
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
    public partial class Gs2FormationPropertyFormDeletePropertyFormAction
    {
        [MenuItem("GameObject/Game Server Services/Formation/PropertyForm/Action/DeletePropertyForm", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2FormationPropertyFormDeletePropertyFormAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Formation/Prefabs/Action/Gs2FormationPropertyFormDeletePropertyFormAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}