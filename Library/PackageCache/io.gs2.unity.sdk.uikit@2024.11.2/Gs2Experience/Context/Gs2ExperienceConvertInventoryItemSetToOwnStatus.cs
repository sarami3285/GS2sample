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
using Gs2.Unity.Gs2Experience.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Inventory.Context;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Experience.Context
{
    [AddComponentMenu("GS2 UIKit/Experience/Status/Context/Convert/Gs2ExperienceConvertInventoryItemSetToOwnStatus")]
    public class Gs2ExperienceConvertInventoryItemSetToOwnStatus : MonoBehaviour
    {
        private Gs2InventoryOwnItemSetContext _originalContext;
        private Gs2ExperienceOwnStatusContext _context;

        public void Awake() {
            this._originalContext = GetComponent<Gs2InventoryOwnItemSetContext>() ?? GetComponentInParent<Gs2InventoryOwnItemSetContext>();
            if (this._originalContext == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventoryOwnItemSetContext.");
                enabled = false;
            }
            this._context = GetComponent<Gs2ExperienceOwnStatusContext>() ?? GetComponentInParent<Gs2ExperienceOwnStatusContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ExperienceOwnStatusContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._originalContext = GetComponent<Gs2InventoryOwnItemSetContext>() ?? GetComponentInParent<Gs2InventoryOwnItemSetContext>();
            if (_originalContext == null) {
                return true;
            }
            this._context = GetComponent<Gs2ExperienceOwnStatusContext>() ?? GetComponentInParent<Gs2ExperienceOwnStatusContext>();
            if (this._context == null) {
                return true;
            }
            return false;
        }

        public ExperienceModel experienceModel;
        public string propertyIdSuffix;
        private UnityAction _onUpdateContext;

        private void OnUpdateContext() {
            if (this._originalContext.ItemSet != null) {
                this._context.SetOwnStatus(
                    OwnStatus.New(
                        this.experienceModel.Namespace,
                        this.experienceModel.ExperienceName,
                        this._originalContext.ItemSet.Grn + this.propertyIdSuffix
                    )
                );
            }
        }

        public void OnEnable() {
            this._onUpdateContext = () =>
            {
                OnUpdateContext();
            };
            this._originalContext.OnUpdate.AddListener(this._onUpdateContext);
            OnUpdateContext();
        }

        public void OnDisable() {
            if (this._onUpdateContext != null) {
                this._originalContext.OnUpdate.RemoveListener(this._onUpdateContext);
                this._onUpdateContext = null;
            }
        }
    }
}