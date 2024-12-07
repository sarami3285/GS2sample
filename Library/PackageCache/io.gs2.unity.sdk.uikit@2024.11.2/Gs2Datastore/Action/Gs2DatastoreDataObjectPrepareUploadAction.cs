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
    public partial class Gs2DatastoreDataObjectPrepareUploadAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onPrepareUploadStart.Invoke();

            
            var domain = clientHolder.Gs2.Datastore.Namespace(
                this._context.DataObject.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            );
            var future = domain.PrepareUploadFuture(
                Name,
                Scope,
                ContentType,
                AllowUserIds.ToArray(),
                UpdateIfExists
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

                        this.onPrepareUploadComplete.Invoke(future3.Result);
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

            this.onPrepareUploadComplete.Invoke(future2.Result);
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

    public partial class Gs2DatastoreDataObjectPrepareUploadAction
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

    public partial class Gs2DatastoreDataObjectPrepareUploadAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2DatastoreDataObjectPrepareUploadAction
    {
        public bool WaitAsyncProcessComplete;
        public string Name;
        public string Scope;
        public string ContentType;
        public List<string> AllowUserIds;
        public bool UpdateIfExists;

        public void SetName(string value) {
            this.Name = value;
            this.onChangeName.Invoke(this.Name);
            this.OnChange.Invoke();
        }

        public void SetScope(string value) {
            this.Scope = value;
            this.onChangeScope.Invoke(this.Scope);
            this.OnChange.Invoke();
        }

        public void SetContentType(string value) {
            this.ContentType = value;
            this.onChangeContentType.Invoke(this.ContentType);
            this.OnChange.Invoke();
        }

        public void SetAllowUserIds(List<string> value) {
            this.AllowUserIds = value;
            this.onChangeAllowUserIds.Invoke(this.AllowUserIds);
            this.OnChange.Invoke();
        }

        public void SetUpdateIfExists(bool value) {
            this.UpdateIfExists = value;
            this.onChangeUpdateIfExists.Invoke(this.UpdateIfExists);
            this.OnChange.Invoke();
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2DatastoreDataObjectPrepareUploadAction
    {

        [Serializable]
        private class ChangeNameEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeNameEvent onChangeName = new ChangeNameEvent();
        public event UnityAction<string> OnChangeName
        {
            add => this.onChangeName.AddListener(value);
            remove => this.onChangeName.RemoveListener(value);
        }

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
        private class ChangeContentTypeEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeContentTypeEvent onChangeContentType = new ChangeContentTypeEvent();
        public event UnityAction<string> OnChangeContentType
        {
            add => this.onChangeContentType.AddListener(value);
            remove => this.onChangeContentType.RemoveListener(value);
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
        private class ChangeUpdateIfExistsEvent : UnityEvent<bool>
        {

        }

        [SerializeField]
        private ChangeUpdateIfExistsEvent onChangeUpdateIfExists = new ChangeUpdateIfExistsEvent();
        public event UnityAction<bool> OnChangeUpdateIfExists
        {
            add => this.onChangeUpdateIfExists.AddListener(value);
            remove => this.onChangeUpdateIfExists.RemoveListener(value);
        }

        [Serializable]
        private class PrepareUploadStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private PrepareUploadStartEvent onPrepareUploadStart = new PrepareUploadStartEvent();

        public event UnityAction OnPrepareUploadStart
        {
            add => this.onPrepareUploadStart.AddListener(value);
            remove => this.onPrepareUploadStart.RemoveListener(value);
        }

        [Serializable]
        private class PrepareUploadCompleteEvent : UnityEvent<EzDataObject>
        {

        }

        [SerializeField]
        private PrepareUploadCompleteEvent onPrepareUploadComplete = new PrepareUploadCompleteEvent();
        public event UnityAction<EzDataObject> OnPrepareUploadComplete
        {
            add => this.onPrepareUploadComplete.AddListener(value);
            remove => this.onPrepareUploadComplete.RemoveListener(value);
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
    public partial class Gs2DatastoreDataObjectPrepareUploadAction
    {
        [MenuItem("GameObject/Game Server Services/Datastore/DataObject/Action/PrepareUpload", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2DatastoreDataObjectPrepareUploadAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Datastore/Prefabs/Action/Gs2DatastoreDataObjectPrepareUploadAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}