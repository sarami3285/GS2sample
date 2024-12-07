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
// ReSharper disable CheckNamespace
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantAssignment
// ReSharper disable NotAccessedVariable
// ReSharper disable RedundantUsingDirective
// ReSharper disable Unity.NoNullPropagation
// ReSharper disable InconsistentNaming

#pragma warning disable CS0472

using Gs2.Unity.Gs2Inventory.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Inventory.Context
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Inventory/ItemSet/Context/Gs2InventoryOwnItemSetContext")]
    public partial class Gs2InventoryOwnItemSetContext : Gs2InventoryItemModelContext
    {
        public new void Start() {
            base.Start();
        }
        public override bool HasError() {
            var hasError = base.HasError();
            if (ItemSet == null || hasError) {
                if (GetComponentInParent<Gs2InventoryOwnItemSetList>(true) != null) {
                    return false;
                }
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2InventoryOwnItemSetContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2InventoryOwnItemSetContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2InventoryOwnItemSetContext
    {
        [SerializeField]
        private OwnItemSet _itemSet;
        public OwnItemSet ItemSet
        {
            get => _itemSet;
            set => SetOwnItemSet(value);
        }

        public void SetOwnItemSet(OwnItemSet itemSet) {
            if (itemSet == null) return;
            
            this.ItemModel = ItemModel.New(
                InventoryModel.New(
                    Namespace.New(
                        itemSet.NamespaceName
                    ),
                    itemSet.InventoryName
                ),
                itemSet.ItemName
            );
            this._itemSet = itemSet;

            this.OnUpdate.Invoke();
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InventoryOwnItemSetContext
    {

    }
}