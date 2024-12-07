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
using Gs2.Unity.Gs2Inventory.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Inventory.Context;
using UnityEngine;
using UnityEngine.Events;
using SimpleItem = Gs2.Unity.Gs2Inventory.ScriptableObject.OwnSimpleItem;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Inventory
{
    public partial class Gs2InventorySimpleItemConsumeSimpleItemsAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onConsumeSimpleItemsStart.Invoke();

            
            var domain = clientHolder.Gs2.Inventory.Namespace(
                this._context.SimpleItem.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            ).SimpleInventory(
                this._context.SimpleItem.InventoryName
            );
            var future = domain.ConsumeSimpleItemsFuture(
                ConsumeCounts.ToArray()
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
                        var simpleItems = new List<EzSimpleItem>();
                        foreach (var r in future.Result) {
                            var future3 = r.ModelFuture();
                            yield return future3;
                            if (future3.Error != null)
                            {
                                this.onError.Invoke(future3.Error, null);
                                yield break;
                            }
                            simpleItems.Add(future3.Result);
                        }
                        this.onConsumeSimpleItemsComplete.Invoke(simpleItems);
                    }

                    this.onError.Invoke(future.Error, Retry);
                    yield break;
                }

                this.onError.Invoke(future.Error, null);
                yield break;
            }
            var simpleItems = new List<EzSimpleItem>();
            foreach (var r in future.Result) {
                var future2 = r.ModelFuture();
                yield return future2;
                if (future2.Error != null)
                {
                    this.onError.Invoke(future2.Error, null);
                    yield break;
                }
                simpleItems.Add(future2.Result);
            }
            this.onConsumeSimpleItemsComplete.Invoke(simpleItems);
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

    public partial class Gs2InventorySimpleItemConsumeSimpleItemsAction
    {
        private Gs2InventoryOwnSimpleItemContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2InventoryOwnSimpleItemContext>() ?? GetComponentInParent<Gs2InventoryOwnSimpleItemContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventoryOwnSimpleItemContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2InventoryOwnSimpleItemContext>() ?? GetComponentInParent<Gs2InventoryOwnSimpleItemContext>(true);
            if (this._context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2InventorySimpleItemConsumeSimpleItemsAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2InventorySimpleItemConsumeSimpleItemsAction
    {
        public bool WaitAsyncProcessComplete;
        public List<Gs2.Unity.Gs2Inventory.Model.EzConsumeCount> ConsumeCounts;

        public void SetConsumeCounts(List<Gs2.Unity.Gs2Inventory.Model.EzConsumeCount> value) {
            this.ConsumeCounts = value;
            this.onChangeConsumeCounts.Invoke(this.ConsumeCounts);
            this.OnChange.Invoke();
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InventorySimpleItemConsumeSimpleItemsAction
    {

        [Serializable]
        private class ChangeConsumeCountsEvent : UnityEvent<List<Gs2.Unity.Gs2Inventory.Model.EzConsumeCount>>
        {

        }

        [SerializeField]
        private ChangeConsumeCountsEvent onChangeConsumeCounts = new ChangeConsumeCountsEvent();
        public event UnityAction<List<Gs2.Unity.Gs2Inventory.Model.EzConsumeCount>> OnChangeConsumeCounts
        {
            add => this.onChangeConsumeCounts.AddListener(value);
            remove => this.onChangeConsumeCounts.RemoveListener(value);
        }

        [Serializable]
        private class ConsumeSimpleItemsStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private ConsumeSimpleItemsStartEvent onConsumeSimpleItemsStart = new ConsumeSimpleItemsStartEvent();

        public event UnityAction OnConsumeSimpleItemsStart
        {
            add => this.onConsumeSimpleItemsStart.AddListener(value);
            remove => this.onConsumeSimpleItemsStart.RemoveListener(value);
        }

        [Serializable]
        private class ConsumeSimpleItemsCompleteEvent : UnityEvent<List<EzSimpleItem>>
        {

        }

        [SerializeField]
        private ConsumeSimpleItemsCompleteEvent onConsumeSimpleItemsComplete = new ConsumeSimpleItemsCompleteEvent();
        public event UnityAction<List<EzSimpleItem>> OnConsumeSimpleItemsComplete
        {
            add => this.onConsumeSimpleItemsComplete.AddListener(value);
            remove => this.onConsumeSimpleItemsComplete.RemoveListener(value);
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
    public partial class Gs2InventorySimpleItemConsumeSimpleItemsAction
    {
        [MenuItem("GameObject/Game Server Services/Inventory/SimpleItem/Action/ConsumeSimpleItems", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2InventorySimpleItemConsumeSimpleItemsAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Inventory/Prefabs/Action/Gs2InventorySimpleItemConsumeSimpleItemsAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}