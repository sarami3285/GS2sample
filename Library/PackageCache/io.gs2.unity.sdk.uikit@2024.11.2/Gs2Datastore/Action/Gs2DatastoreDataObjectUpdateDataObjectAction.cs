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
using Gs2.Unity.Gs2Datastore.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Datastore.Context;
using UnityEngine;
using UnityEngine.Events;
using DataObject = Gs2.Unity.Gs2Datastore.ScriptableObject.OwnDataObject;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Datastore
{
    public partial class Gs2DatastoreDataObjectUpdateDataObjectAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onUpdateDataObjectStart.Invoke();

            
            var domain = clientHolder.Gs2.Datastore.Namespace(
                this._context.DataObject.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            ).DataObject(
                this._context.DataObject.DataObjectName
            );
            var future = domain.UpdateDataObjectFuture(
                Scope,
                AllowUserIds.ToArray()
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

                        this.onUpdateDataObjectComplete.Invoke(future3.Result);
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

            this.onUpdateDataObjectComplete.Invoke(future2.Result);
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

    public partial class Gs2DatastoreDataObjectUpdateDataObjectAction
    {
        private Gs2DatastoreOwnDataObjectContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2DatastoreOwnDataObjectContext>() ?? GetComponentInParent<Gs2DatastoreOwnDataObjectContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2DatastoreOwnDataObjectContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2DatastoreOwnDataObjectContext>() ?? GetComponentInParent<Gs2DatastoreOwnDataObjectContext>(true);
            if (this._context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2DatastoreDataObjectUpdateDataObjectAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2DatastoreDataObjectUpdateDataObjectAction
    {
        public bool WaitAsyncProcessComplete;
        public string Scope;
        public List<string> AllowUserIds;

        public void SetScope(string value) {
            this.Scope = value;
            this.onChangeScope.Invoke(this.Scope);
            this.OnChange.Invoke();
        }

        public void SetAllowUserIds(List<string> value) {
            this.AllowUserIds = value;
            this.onChangeAllowUserIds.Invoke(this.AllowUserIds);
            this.OnChange.Invoke();
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2DatastoreDataObjectUpdateDataObjectAction
    {

        [Serializable]
        private class ChangeScopeEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeScopeEvent onChangeScope = new ChangeScopeEvent();
        public event UnityAction<string> OnChangeScope
        {
            add => this.onChangeScope.AddListener(value);
            remove => this.onChangeScope.RemoveListener(value);
        }

        [Serializable]
        private class ChangeAllowUserIdsEvent : UnityEvent<List<string>>
        {

        }

        [SerializeField]
        private ChangeAllowUserIdsEvent onChangeAllowUserIds = new ChangeAllowUserIdsEvent();
        public event UnityAction<List<string>> OnChangeAllowUserIds
        {
            add => this.onChangeAllowUserIds.AddListener(value);
            remove => this.onChangeAllowUserIds.RemoveListener(value);
        }

        [Serializable]
        private class UpdateDataObjectStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private UpdateDataObjectStartEvent onUpdateDataObjectStart = new UpdateDataObjectStartEvent();

        public event UnityAction OnUpdateDataObjectStart
        {
            add => this.onUpdateDataObjectStart.AddListener(value);
            remove => this.onUpdateDataObjectStart.RemoveListener(value);
        }

        [Serializable]
        private class UpdateDataObjectCompleteEvent : UnityEvent<EzDataObject>
        {

        }

        [SerializeField]
        private UpdateDataObjectCompleteEvent onUpdateDataObjectComplete = new UpdateDataObjectCompleteEvent();
        public event UnityAction<EzDataObject> OnUpdateDataObjectComplete
        {
            add => this.onUpdateDataObjectComplete.AddListener(value);
            remove => this.onUpdateDataObjectComplete.RemoveListener(value);
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
    public partial class Gs2DatastoreDataObjectUpdateDataObjectAction
    {
        [MenuItem("GameObject/Game Server Services/Datastore/DataObject/Action/UpdateDataObject", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2DatastoreDataObjectUpdateDataObjectAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Datastore/Prefabs/Action/Gs2DatastoreDataObjectUpdateDataObjectAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}