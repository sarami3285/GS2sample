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
using Gs2.Gs2Money.Request;
using Gs2.Unity.Gs2Showcase.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Showcase.Context;
using Gs2.Unity.UiKit.Gs2Showcase.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using DisplayItem = Gs2.Unity.Gs2Showcase.ScriptableObject.OwnDisplayItem;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Showcase
{
    public partial class Gs2ShowcaseDisplayItemBuyAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            var config = new List<Gs2.Unity.Gs2Showcase.Model.EzConfig>(Config);
            
#if GS2_ENABLE_PURCHASING

            PurchaseParameters purchaseParameters = null;
            var needReceipt = this._fetcher.DisplayItem.SalesItem.ConsumeActions.FirstOrDefault(
                v => v.Action == "Gs2Money:RecordReceipt"
            );
            if (needReceipt != null) {
                var request = RecordReceiptRequest.FromJson(JsonMapper.ToObject(needReceipt.Request));
                var iapFuture = new IAPUtil().BuyFuture(request.ContentsId);
                yield return iapFuture;
                if (iapFuture.Error != null) {
                    this.onError.Invoke(iapFuture.Error, Process);
                    yield break;
                }
                purchaseParameters = iapFuture.Result;
                config.Add(new EzConfig {
                    Key = "receipt",
                    Value = purchaseParameters.receipt,
                });
            }

#endif

            var domain = clientHolder.Gs2.Showcase.Namespace(
                this._fetcher.Context.DisplayItem.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            ).Showcase(
                this._fetcher.Context.DisplayItem.ShowcaseName
            ).DisplayItem(
                this._fetcher.DisplayItem.DisplayItemId
            );
            var future = domain.BuyFuture(
                Quantity,
                config.ToArray()
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
                        this.onBuyComplete.Invoke();
                    }

#if GS2_ENABLE_PURCHASING
                    if (purchaseParameters != null) {
                        purchaseParameters.controller.ConfirmPendingPurchase(purchaseParameters.product);
                    }
#endif

                    this.onError.Invoke(future.Error, Retry);
                    yield break;
                }

                this.onError.Invoke(future.Error, null);
                yield break;
            }
            
#if GS2_ENABLE_PURCHASING
            if (purchaseParameters != null) {
                purchaseParameters.controller.ConfirmPendingPurchase(purchaseParameters.product);
            }
#endif

            if (this.WaitAsyncProcessComplete && future.Result != null) {
                var transaction = future.Result;
                var future2 = transaction.WaitFuture();
                yield return future2;
            }
            
            this.onBuyComplete.Invoke();
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

    public partial class Gs2ShowcaseDisplayItemBuyAction
    {
        private Gs2ShowcaseOwnDisplayItemFetcher _fetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2ShowcaseOwnDisplayItemFetcher>() ?? GetComponentInParent<Gs2ShowcaseOwnDisplayItemFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ShowcaseOwnDisplayItemFetcher.");
                enabled = false;
            }
        }

        public bool HasError()
        {
            this._fetcher = GetComponent<Gs2ShowcaseOwnDisplayItemFetcher>() ?? GetComponentInParent<Gs2ShowcaseOwnDisplayItemFetcher>(true);
            if (this._fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ShowcaseDisplayItemBuyAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2ShowcaseDisplayItemBuyAction
    {
        public bool WaitAsyncProcessComplete;
        public int Quantity;
        public List<Gs2.Unity.Gs2Showcase.Model.EzConfig> Config;

        public void SetQuantity(int value) {
            this.Quantity = value;
            this.onChangeQuantity.Invoke(this.Quantity);
            this.OnChange.Invoke();
        }

        public void DecreaseQuantity() {
            this.Quantity -= 1;
            this.onChangeQuantity.Invoke(this.Quantity);
        }

        public void IncreaseQuantity() {
            this.Quantity += 1;
            this.onChangeQuantity.Invoke(this.Quantity);
            this.OnChange.Invoke();
        }

        public void SetConfig(List<Gs2.Unity.Gs2Showcase.Model.EzConfig> value) {
            Config = value;
            this.onChangeConfig.Invoke(Config);
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ShowcaseDisplayItemBuyAction
    {

        [Serializable]
        private class ChangeQuantityEvent : UnityEvent<int>
        {

        }

        [SerializeField]
        private ChangeQuantityEvent onChangeQuantity = new ChangeQuantityEvent();
        public event UnityAction<int> OnChangeQuantity
        {
            add => this.onChangeQuantity.AddListener(value);
            remove => this.onChangeQuantity.RemoveListener(value);
        }

        [Serializable]
        private class ChangeConfigEvent : UnityEvent<List<Gs2.Unity.Gs2Showcase.Model.EzConfig>>
        {

        }

        [SerializeField]
        private ChangeConfigEvent onChangeConfig = new ChangeConfigEvent();
        public event UnityAction<List<Gs2.Unity.Gs2Showcase.Model.EzConfig>> OnChangeConfig
        {
            add => this.onChangeConfig.AddListener(value);
            remove => this.onChangeConfig.RemoveListener(value);
        }

        [Serializable]
        private class BuyCompleteEvent : UnityEvent
        {

        }

        [SerializeField]
        private BuyCompleteEvent onBuyComplete = new BuyCompleteEvent();
        public event UnityAction OnBuyComplete
        {
            add => this.onBuyComplete.AddListener(value);
            remove => this.onBuyComplete.RemoveListener(value);
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
    public partial class Gs2ShowcaseDisplayItemBuyAction
    {
        [MenuItem("GameObject/Game Server Services/Showcase/DisplayItem/Action/Buy", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2ShowcaseDisplayItemBuyAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Showcase/Prefabs/Action/Gs2ShowcaseDisplayItemBuyAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}