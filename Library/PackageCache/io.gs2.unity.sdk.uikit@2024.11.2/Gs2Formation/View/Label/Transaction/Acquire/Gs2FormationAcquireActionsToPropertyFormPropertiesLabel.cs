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
// ReSharper disable CheckNamespace
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantAssignment
// ReSharper disable NotAccessedVariable
// ReSharper disable RedundantUsingDirective
// ReSharper disable Unity.NoNullPropagation
// ReSharper disable InconsistentNaming

#pragma warning disable CS0472

using System;
using Gs2.Gs2Formation.Request;
using Gs2.Unity.Gs2Formation.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Formation.Context;
using Gs2.Unity.UiKit.Gs2Formation.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Formation.Label
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Formation/PropertyForm/View/Label/Transaction/Gs2FormationAcquireActionsToPropertyFormPropertiesLabel")]
    public partial class Gs2FormationAcquireActionsToPropertyFormPropertiesLabel : MonoBehaviour
    {
        private void OnFetched()
        {
            if (this._userDataFetcher == null) {
                var context = gameObject.AddComponent<Gs2FormationOwnPropertyFormContext>();
                context.SetOwnPropertyForm(
                    OwnPropertyForm.New(
                        Namespace.New(
                            this._fetcher.Request.NamespaceName
                        ),
                        this._fetcher.Request.PropertyFormModelName,
                        this._fetcher.Request.PropertyId
                    )
                );
                this._userDataFetcher = gameObject.AddComponent<Gs2FormationOwnPropertyFormFetcher>();
                this._userDataFetcher.OnFetched.AddListener(this._onFetched);
            }
            if ((!this._fetcher?.Fetched ?? false) || this._fetcher.Request == null) {
                return;
            }
            if ((!this._userDataFetcher?.Fetched ?? false) || this._userDataFetcher.PropertyForm == null) {
                return;
            }
            this.onUpdate?.Invoke(
                this.format.Replace(
                    "{namespaceName}",
                    $"{this._fetcher.Request.NamespaceName}"
                ).Replace(
                    "{userId}",
                    $"{this._fetcher.Request.UserId}"
                ).Replace(
                    "{propertyFormModelName}",
                    $"{this._fetcher.Request.PropertyFormModelName}"
                ).Replace(
                    "{propertyId}",
                    $"{this._fetcher.Request.PropertyId}"
                ).Replace(
                    "{acquireAction}",
                    $"{this._fetcher.Request.AcquireAction}"
                ).Replace(
                    "{config}",
                    $"{this._fetcher.Request.Config}"
                ).Replace(
                    "{timeOffsetToken}",
                    $"{this._fetcher.Request.TimeOffsetToken}"
                ).Replace(
                    "{userData:name}",
                    $"{this._userDataFetcher.PropertyForm.Name}"
                ).Replace(
                    "{userData:propertyId}",
                    $"{this._userDataFetcher.PropertyForm.PropertyId}"
                ).Replace(
                    "{userData:slots}",
                    $"{this._userDataFetcher.PropertyForm.Slots}"
                )
            );
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2FormationAcquireActionsToPropertyFormPropertiesLabel
    {
        private Gs2FormationAcquireActionsToPropertyFormPropertiesFetcher _fetcher;
        private Gs2FormationOwnPropertyFormFetcher _userDataFetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2FormationAcquireActionsToPropertyFormPropertiesFetcher>() ?? GetComponentInParent<Gs2FormationAcquireActionsToPropertyFormPropertiesFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FormationAcquireActionsToPropertyFormPropertiesFetcher.");
                enabled = false;
            }
            this._userDataFetcher = GetComponent<Gs2FormationOwnPropertyFormFetcher>() ?? GetComponentInParent<Gs2FormationOwnPropertyFormFetcher>();
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2FormationAcquireActionsToPropertyFormPropertiesFetcher>() ?? GetComponentInParent<Gs2FormationAcquireActionsToPropertyFormPropertiesFetcher>(true);
            if (this._fetcher == null) {
                return true;
            }
            return false;
        }

        private UnityAction _onFetched;

        public void OnEnable()
        {
            this._onFetched = () =>
            {
                OnFetched();
            };
            this._fetcher.OnFetched.AddListener(this._onFetched);
            if (this._fetcher.Fetched) {
                OnFetched();
            }
            if (this._userDataFetcher != null) {
                this._userDataFetcher.OnFetched.AddListener(this._onFetched);
                if (this._userDataFetcher.Fetched) {
                    OnFetched();
                }
            }
        }

        public void OnDisable()
        {
            if (this._onFetched != null) {
                this._fetcher.OnFetched.RemoveListener(this._onFetched);
                if (this._userDataFetcher != null) {
                    this._userDataFetcher.OnFetched.RemoveListener(this._onFetched);
                }
                this._onFetched = null;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FormationAcquireActionsToPropertyFormPropertiesLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2FormationAcquireActionsToPropertyFormPropertiesLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FormationAcquireActionsToPropertyFormPropertiesLabel
    {
        [Serializable]
        private class UpdateEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private UpdateEvent onUpdate = new UpdateEvent();

        public event UnityAction<string> OnUpdate
        {
            add => this.onUpdate.AddListener(value);
            remove => this.onUpdate.RemoveListener(value);
        }
    }
}