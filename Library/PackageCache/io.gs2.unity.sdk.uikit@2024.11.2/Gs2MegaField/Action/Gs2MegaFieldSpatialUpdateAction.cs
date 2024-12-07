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
using Gs2.Unity.Gs2MegaField.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2MegaField.Context;
using UnityEngine;
using UnityEngine.Events;
using Spatial = Gs2.Unity.Gs2MegaField.ScriptableObject.OwnSpatial;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2MegaField
{
    public partial class Gs2MegaFieldSpatialUpdateAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onUpdateStart.Invoke();

            
            var domain = clientHolder.Gs2.MegaField.Namespace(
                this._context.LayerModel.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            ).Spatial(
                this._context.LayerModel.AreaModelName,
                this._context.LayerModel.LayerModelName
            );
            var future = domain.UpdateFuture(
                Position,
                Scopes.ToArray()
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
                    }

                    this.onError.Invoke(future.Error, Retry);
                    yield break;
                }

                this.onError.Invoke(future.Error, null);
                yield break;
            }
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

    public partial class Gs2MegaFieldSpatialUpdateAction
    {
        private Gs2MegaFieldLayerModelContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2MegaFieldLayerModelContext>() ?? GetComponentInParent<Gs2MegaFieldLayerModelContext>();
            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MegaFieldLayerModelContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2MegaFieldLayerModelContext>() ?? GetComponentInParent<Gs2MegaFieldLayerModelContext>(true);
            if (_context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MegaFieldSpatialUpdateAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2MegaFieldSpatialUpdateAction
    {
        public bool WaitAsyncProcessComplete;
        public Gs2.Unity.Gs2MegaField.Model.EzMyPosition Position;
        public List<Gs2.Unity.Gs2MegaField.Model.EzScope> Scopes;

        public void SetPosition(Gs2.Unity.Gs2MegaField.Model.EzMyPosition value) {
            this.Position = value;
            this.onChangePosition.Invoke(this.Position);
            this.OnChange.Invoke();
        }

        public void SetScopes(List<Gs2.Unity.Gs2MegaField.Model.EzScope> value) {
            this.Scopes = value;
            this.onChangeScopes.Invoke(this.Scopes);
            this.OnChange.Invoke();
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MegaFieldSpatialUpdateAction
    {

        [Serializable]
        private class ChangePositionEvent : UnityEvent<Gs2.Unity.Gs2MegaField.Model.EzMyPosition>
        {

        }

        [SerializeField]
        private ChangePositionEvent onChangePosition = new ChangePositionEvent();
        public event UnityAction<Gs2.Unity.Gs2MegaField.Model.EzMyPosition> OnChangePosition
        {
            add => this.onChangePosition.AddListener(value);
            remove => this.onChangePosition.RemoveListener(value);
        }

        [Serializable]
        private class ChangeScopesEvent : UnityEvent<List<Gs2.Unity.Gs2MegaField.Model.EzScope>>
        {

        }

        [SerializeField]
        private ChangeScopesEvent onChangeScopes = new ChangeScopesEvent();
        public event UnityAction<List<Gs2.Unity.Gs2MegaField.Model.EzScope>> OnChangeScopes
        {
            add => this.onChangeScopes.AddListener(value);
            remove => this.onChangeScopes.RemoveListener(value);
        }

        [Serializable]
        private class UpdateStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private UpdateStartEvent onUpdateStart = new UpdateStartEvent();

        public event UnityAction OnUpdateStart
        {
            add => this.onUpdateStart.AddListener(value);
            remove => this.onUpdateStart.RemoveListener(value);
        }

        [Serializable]
        private class UpdateCompleteEvent : UnityEvent<List<EzSpatial>>
        {

        }

        [SerializeField]
        private UpdateCompleteEvent onUpdateComplete = new UpdateCompleteEvent();
        public event UnityAction<List<EzSpatial>> OnUpdateComplete
        {
            add => this.onUpdateComplete.AddListener(value);
            remove => this.onUpdateComplete.RemoveListener(value);
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
    public partial class Gs2MegaFieldSpatialUpdateAction
    {
        [MenuItem("GameObject/Game Server Services/MegaField/Spatial/Action/Update", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2MegaFieldSpatialUpdateAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2MegaField/Prefabs/Action/Gs2MegaFieldSpatialUpdateAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}