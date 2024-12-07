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
using Gs2.Unity.Gs2Enchant.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Enchant.Context;
using UnityEngine;
using UnityEngine.Events;
using RarityParameterStatus = Gs2.Unity.Gs2Enchant.ScriptableObject.OwnRarityParameterStatus;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Enchant
{
    public partial class Gs2EnchantRarityParameterStatusVerifyRarityParameterStatusAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onVerifyRarityParameterStatusStart.Invoke();

            
            var domain = clientHolder.Gs2.Enchant.Namespace(
                this._context.RarityParameterStatus.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            ).RarityParameterStatus(
                this._context.RarityParameterStatus.ParameterName,
                this._context.RarityParameterStatus.PropertyId
            );
            var future = domain.VerifyRarityParameterStatusFuture(
                VerifyType,
                ParameterValueName,
                ParameterCount
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

                        this.onVerifyRarityParameterStatusComplete.Invoke(future3.Result);
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

            this.onVerifyRarityParameterStatusComplete.Invoke(future2.Result);
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

    public partial class Gs2EnchantRarityParameterStatusVerifyRarityParameterStatusAction
    {
        private Gs2EnchantOwnRarityParameterStatusContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2EnchantOwnRarityParameterStatusContext>() ?? GetComponentInParent<Gs2EnchantOwnRarityParameterStatusContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2EnchantOwnRarityParameterStatusContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2EnchantOwnRarityParameterStatusContext>() ?? GetComponentInParent<Gs2EnchantOwnRarityParameterStatusContext>(true);
            if (this._context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2EnchantRarityParameterStatusVerifyRarityParameterStatusAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2EnchantRarityParameterStatusVerifyRarityParameterStatusAction
    {
        public bool WaitAsyncProcessComplete;
        public string VerifyType;
        public string ParameterValueName;
        public int ParameterCount;

        public void SetVerifyType(string value) {
            this.VerifyType = value;
            this.onChangeVerifyType.Invoke(this.VerifyType);
            this.OnChange.Invoke();
        }

        public void SetParameterValueName(string value) {
            this.ParameterValueName = value;
            this.onChangeParameterValueName.Invoke(this.ParameterValueName);
            this.OnChange.Invoke();
        }

        public void SetParameterCount(int value) {
            this.ParameterCount = value;
            this.onChangeParameterCount.Invoke(this.ParameterCount);
            this.OnChange.Invoke();
        }

        public void DecreaseParameterCount() {
            this.ParameterCount -= 1;
            this.onChangeParameterCount.Invoke(this.ParameterCount);
            this.OnChange.Invoke();
        }

        public void IncreaseParameterCount() {
            this.ParameterCount += 1;
            this.onChangeParameterCount.Invoke(this.ParameterCount);
            this.OnChange.Invoke();
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2EnchantRarityParameterStatusVerifyRarityParameterStatusAction
    {

        [Serializable]
        private class ChangeVerifyTypeEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeVerifyTypeEvent onChangeVerifyType = new ChangeVerifyTypeEvent();
        public event UnityAction<string> OnChangeVerifyType
        {
            add => this.onChangeVerifyType.AddListener(value);
            remove => this.onChangeVerifyType.RemoveListener(value);
        }

        [Serializable]
        private class ChangeParameterValueNameEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeParameterValueNameEvent onChangeParameterValueName = new ChangeParameterValueNameEvent();
        public event UnityAction<string> OnChangeParameterValueName
        {
            add => this.onChangeParameterValueName.AddListener(value);
            remove => this.onChangeParameterValueName.RemoveListener(value);
        }

        [Serializable]
        private class ChangeParameterCountEvent : UnityEvent<int>
        {

        }

        [SerializeField]
        private ChangeParameterCountEvent onChangeParameterCount = new ChangeParameterCountEvent();
        public event UnityAction<int> OnChangeParameterCount
        {
            add => this.onChangeParameterCount.AddListener(value);
            remove => this.onChangeParameterCount.RemoveListener(value);
        }

        [Serializable]
        private class VerifyRarityParameterStatusStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private VerifyRarityParameterStatusStartEvent onVerifyRarityParameterStatusStart = new VerifyRarityParameterStatusStartEvent();

        public event UnityAction OnVerifyRarityParameterStatusStart
        {
            add => this.onVerifyRarityParameterStatusStart.AddListener(value);
            remove => this.onVerifyRarityParameterStatusStart.RemoveListener(value);
        }

        [Serializable]
        private class VerifyRarityParameterStatusCompleteEvent : UnityEvent<EzRarityParameterStatus>
        {

        }

        [SerializeField]
        private VerifyRarityParameterStatusCompleteEvent onVerifyRarityParameterStatusComplete = new VerifyRarityParameterStatusCompleteEvent();
        public event UnityAction<EzRarityParameterStatus> OnVerifyRarityParameterStatusComplete
        {
            add => this.onVerifyRarityParameterStatusComplete.AddListener(value);
            remove => this.onVerifyRarityParameterStatusComplete.RemoveListener(value);
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
    public partial class Gs2EnchantRarityParameterStatusVerifyRarityParameterStatusAction
    {
        [MenuItem("GameObject/Game Server Services/Enchant/RarityParameterStatus/Action/VerifyRarityParameterStatus", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2EnchantRarityParameterStatusVerifyRarityParameterStatusAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Enchant/Prefabs/Action/Gs2EnchantRarityParameterStatusVerifyRarityParameterStatusAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}