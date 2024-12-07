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
using Gs2.Unity.Gs2Datastore.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Datastore.Context;
using UnityEngine;
using UnityEngine.Events;
using DataObject = Gs2.Unity.Gs2Datastore.ScriptableObject.DataObject;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Datastore
{
    public partial class Gs2DatastoreDataObjectPrepareDownloadByUserIdAndDataObjectNameAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onPrepareDownloadByUserIdAndDataObjectNameStart.Invoke();

            
            var domain = clientHolder.Gs2.Datastore.Namespace(
                this._context.DataObject.NamespaceName
            ).User(
                this._context.DataObject.UserId
            ).DataObject(
                this._context.DataObject.DataObjectName
            );
            var future = domain.PrepareDownloadByUserIdAndDataObjectNameFuture(
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

                        this.onPrepareDownloadByUserIdAndDataObjectNameComplete.Invoke(future3.Result);
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

            this.onPrepareDownloadByUserIdAndDataObjectNameComplete.Invoke(future2.Result);
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

    public partial class Gs2DatastoreDataObjectPrepareDownloadByUserIdAndDataObjectNameAction
    {
        private Gs2DatastoreDataObjectContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2DatastoreDataObjectContext>() ?? GetComponentInParent<Gs2DatastoreDataObjectContext>();
            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2DatastoreDataObjectContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2DatastoreDataObjectContext>() ?? GetComponentInParent<Gs2DatastoreDataObjectContext>(true);
            if (_context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2DatastoreDataObjectPrepareDownloadByUserIdAndDataObjectNameAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2DatastoreDataObjectPrepareDownloadByUserIdAndDataObjectNameAction
    {
        public bool WaitAsyncProcessComplete;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2DatastoreDataObjectPrepareDownloadByUserIdAndDataObjectNameAction
    {

        [Serializable]
        private class PrepareDownloadByUserIdAndDataObjectNameStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private PrepareDownloadByUserIdAndDataObjectNameStartEvent onPrepareDownloadByUserIdAndDataObjectNameStart = new PrepareDownloadByUserIdAndDataObjectNameStartEvent();

        public event UnityAction OnPrepareDownloadByUserIdAndDataObjectNameStart
        {
            add => this.onPrepareDownloadByUserIdAndDataObjectNameStart.AddListener(value);
            remove => this.onPrepareDownloadByUserIdAndDataObjectNameStart.RemoveListener(value);
        }

        [Serializable]
        private class PrepareDownloadByUserIdAndDataObjectNameCompleteEvent : UnityEvent<EzDataObject>
        {

        }

        [SerializeField]
        private PrepareDownloadByUserIdAndDataObjectNameCompleteEvent onPrepareDownloadByUserIdAndDataObjectNameComplete = new PrepareDownloadByUserIdAndDataObjectNameCompleteEvent();
        public event UnityAction<EzDataObject> OnPrepareDownloadByUserIdAndDataObjectNameComplete
        {
            add => this.onPrepareDownloadByUserIdAndDataObjectNameComplete.AddListener(value);
            remove => this.onPrepareDownloadByUserIdAndDataObjectNameComplete.RemoveListener(value);
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
    public partial class Gs2DatastoreDataObjectPrepareDownloadByUserIdAndDataObjectNameAction
    {
        [MenuItem("GameObject/Game Server Services/Datastore/DataObject/Action/PrepareDownloadByUserIdAndDataObjectName", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2DatastoreDataObjectPrepareDownloadByUserIdAndDataObjectNameAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Datastore/Prefabs/Action/Gs2DatastoreDataObjectPrepareDownloadByUserIdAndDataObjectNameAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}