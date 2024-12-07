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
using Gs2.Unity.Gs2Showcase.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Showcase.Context;
using UnityEngine;
using UnityEngine.Events;
using Showcase = Gs2.Unity.Gs2Showcase.ScriptableObject.OwnShowcase;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Showcase
{
    public partial class Gs2ShowcaseShowcaseBuyAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => this._clientHolder.Initialized);
            yield return new WaitUntil(() => this._gameSessionHolder.Initialized);
            
            var domain = this._clientHolder.Gs2.Showcase.Namespace(
                this._context.Showcase.NamespaceName
            ).Me(
                this._gameSessionHolder.GameSession
            ).Showcase(
                this._context.Showcase.ShowcaseName
            ).DisplayItem(
                DisplayItemId
            );
            var future = domain.BuyFuture(
                Quantity,
                Config.ToArray()
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

                    this.onError.Invoke(future.Error, Retry);
                    yield break;
                }

                this.onError.Invoke(future.Error, null);
                yield break;
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

    public partial class Gs2ShowcaseShowcaseBuyAction
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2ShowcaseOwnShowcaseContext _context;

        public void Awake()
        {
            this._clientHolder = Gs2ClientHolder.Instance;
            this._gameSessionHolder = Gs2GameSessionHolder.Instance;
            this._context = GetComponent<Gs2ShowcaseOwnShowcaseContext>() ?? GetComponentInParent<Gs2ShowcaseOwnShowcaseContext>();

            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ShowcaseOwnShowcaseContext.");
                enabled = false;
            }
        }

        public bool HasError()
        {
            this._context = GetComponent<Gs2ShowcaseOwnShowcaseContext>() ?? GetComponentInParent<Gs2ShowcaseOwnShowcaseContext>(true);
            if (_context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ShowcaseShowcaseBuyAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2ShowcaseShowcaseBuyAction
    {
        public string DisplayItemId;
        public int Quantity;
        public List<Gs2.Unity.Gs2Showcase.Model.EzConfig> Config;

        public void SetDisplayItemId(string value) {
            DisplayItemId = value;
            this.onChangeDisplayItemId.Invoke(DisplayItemId);
        }

        public void SetQuantity(int value) {
            Quantity = value;
            this.onChangeQuantity.Invoke(Quantity);
        }

        public void DecreaseQuantity() {
            Quantity -= 1;
            this.onChangeQuantity.Invoke(Quantity);
        }

        public void IncreaseQuantity() {
            Quantity += 1;
            this.onChangeQuantity.Invoke(Quantity);
        }

        public void SetConfig(List<Gs2.Unity.Gs2Showcase.Model.EzConfig> value) {
            Config = value;
            this.onChangeConfig.Invoke(Config);
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ShowcaseShowcaseBuyAction
    {

        [Serializable]
        private class ChangeDisplayItemIdEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeDisplayItemIdEvent onChangeDisplayItemId = new ChangeDisplayItemIdEvent();
        public event UnityAction<string> OnChangeDisplayItemId
        {
            add => this.onChangeDisplayItemId.AddListener(value);
            remove => this.onChangeDisplayItemId.RemoveListener(value);
        }

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
    public partial class Gs2ShowcaseShowcaseBuyAction
    {
        [MenuItem("GameObject/Game Server Services/Showcase/Showcase/Action/Buy", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2ShowcaseShowcaseBuyAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Showcase/Prefabs/Action/Gs2ShowcaseShowcaseBuyAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}