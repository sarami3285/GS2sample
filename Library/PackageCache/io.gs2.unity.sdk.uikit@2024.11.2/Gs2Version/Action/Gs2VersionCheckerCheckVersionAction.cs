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
using Gs2.Unity.Gs2Version.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Version.Context;
using UnityEngine;
using UnityEngine.Events;
using User = Gs2.Unity.Gs2Version.ScriptableObject.User;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Version
{
    public partial class Gs2VersionCheckerCheckVersionAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onCheckVersionStart.Invoke();

            
            var domain = clientHolder.Gs2.Version.Namespace(
                this._context.Namespace.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            ).Checker(
            );
            var future = domain.CheckVersionFuture(
                TargetVersions.ToArray()
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
                        this.onCheckVersionComplete.Invoke(
                            future.Result.ProjectToken, 
                            future.Result.Warnings.ToList(), 
                            future.Result.Errors.ToList());
                    }

                    this.onError.Invoke(future.Error, Retry);
                    yield break;
                }

                this.onError.Invoke(future.Error, null);
                yield break;
            }
            this.onCheckVersionComplete.Invoke(
                future.Result.ProjectToken, 
                future.Result.Warnings.ToList(), 
                future.Result.Errors.ToList());
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

    public partial class Gs2VersionCheckerCheckVersionAction
    {
        private Gs2VersionNamespaceContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2VersionNamespaceContext>() ?? GetComponentInParent<Gs2VersionNamespaceContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2VersionNamespaceContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2VersionNamespaceContext>() ?? GetComponentInParent<Gs2VersionNamespaceContext>(true);
            if (this._context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2VersionCheckerCheckVersionAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2VersionCheckerCheckVersionAction
    {
        public bool WaitAsyncProcessComplete;
        public List<Gs2.Unity.Gs2Version.Model.EzTargetVersion> TargetVersions;

        public void SetTargetVersions(List<Gs2.Unity.Gs2Version.Model.EzTargetVersion> value) {
            this.TargetVersions = value;
            this.onChangeTargetVersions.Invoke(this.TargetVersions);
            this.OnChange.Invoke();
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2VersionCheckerCheckVersionAction
    {

        [Serializable]
        private class ChangeTargetVersionsEvent : UnityEvent<List<Gs2.Unity.Gs2Version.Model.EzTargetVersion>>
        {

        }

        [SerializeField]
        private ChangeTargetVersionsEvent onChangeTargetVersions = new ChangeTargetVersionsEvent();
        public event UnityAction<List<Gs2.Unity.Gs2Version.Model.EzTargetVersion>> OnChangeTargetVersions
        {
            add => this.onChangeTargetVersions.AddListener(value);
            remove => this.onChangeTargetVersions.RemoveListener(value);
        }

        [Serializable]
        private class CheckVersionStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private CheckVersionStartEvent onCheckVersionStart = new CheckVersionStartEvent();

        public event UnityAction OnCheckVersionStart
        {
            add => this.onCheckVersionStart.AddListener(value);
            remove => this.onCheckVersionStart.RemoveListener(value);
        }

        [Serializable]
        private class CheckVersionCompleteEvent : UnityEvent<string, List<Gs2.Unity.Gs2Version.Model.EzStatus>, List<Gs2.Unity.Gs2Version.Model.EzStatus>>
        {

        }

        [SerializeField]
        private CheckVersionCompleteEvent onCheckVersionComplete = new CheckVersionCompleteEvent();
        public event UnityAction<string, List<Gs2.Unity.Gs2Version.Model.EzStatus>, List<Gs2.Unity.Gs2Version.Model.EzStatus>> OnCheckVersionComplete
        {
            add => this.onCheckVersionComplete.AddListener(value);
            remove => this.onCheckVersionComplete.RemoveListener(value);
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
    public partial class Gs2VersionCheckerCheckVersionAction
    {
        [MenuItem("GameObject/Game Server Services/Version/Checker/Action/CheckVersion", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2VersionCheckerCheckVersionAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Version/Prefabs/Action/Gs2VersionCheckerCheckVersionAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}