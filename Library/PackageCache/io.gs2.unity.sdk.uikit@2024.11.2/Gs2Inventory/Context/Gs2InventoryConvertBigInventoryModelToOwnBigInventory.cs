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
using Gs2.Unity.Gs2Inventory.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Inventory.Context
{
    [AddComponentMenu("GS2 UIKit/Inventory/BigInventory/Context/Convert/Gs2InventoryConvertBigInventoryModelToOwnBigInventory")]
    public class Gs2InventoryConvertBigInventoryModelToOwnBigInventory : MonoBehaviour
    {
        private Gs2InventoryBigInventoryModelContext _originalContext;
        private Gs2InventoryOwnBigInventoryContext _context;

        public void Awake() {
            this._originalContext = GetComponent<Gs2InventoryBigInventoryModelContext>() ?? GetComponentInParent<Gs2InventoryBigInventoryModelContext>();
            if (this._originalContext == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventoryBigInventoryModelContext.");
                enabled = false;
            }
            this._context = GetComponent<Gs2InventoryOwnBigInventoryContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventoryOwnBigInventoryContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._originalContext = GetComponent<Gs2InventoryBigInventoryModelContext>() ?? GetComponentInParent<Gs2InventoryBigInventoryModelContext>();
            if (_originalContext == null) {
                return true;
            }
            this._context = GetComponent<Gs2InventoryOwnBigInventoryContext>();
            if (this._context == null) {
                return true;
            }
            return false;
        }

        private UnityAction _onUpdateContext;

        private void OnUpdateContext() {
            this._context.SetOwnBigInventory(
                OwnBigInventory.New(
                    _originalContext.BigInventoryModel.Namespace,
                    _originalContext.BigInventoryModel.inventoryName
                )
            );
        }

        public void OnEnable() {
            _onUpdateContext = () =>
            {
                OnUpdateContext();
            };
            this._originalContext.OnUpdate.AddListener(this._onUpdateContext);
        }

        public void OnDisable() {
            if (this._onUpdateContext != null) {
                this._originalContext.OnUpdate.RemoveListener(this._onUpdateContext);
                this._onUpdateContext = null;
            }
        }
    }
}