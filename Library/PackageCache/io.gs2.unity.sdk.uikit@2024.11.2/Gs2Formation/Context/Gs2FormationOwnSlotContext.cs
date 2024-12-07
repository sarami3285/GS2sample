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

using Gs2.Unity.Gs2Formation.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Formation.Context
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Formation/Slot/Context/Gs2FormationOwnSlotContext")]
    public partial class Gs2FormationOwnSlotContext : Gs2FormationFormModelContext
    {
        public new void Start() {
            if (Slot == null) {
                Debug.LogWarning($"{gameObject.GetFullPath()}: Slot is not set in Gs2FormationOwnSlotContext.");
            }
        }

        public override bool HasError() {
            if (Slot == null) {
                if (GetComponentInParent<Gs2FormationOwnSlotList>(true) != null) {
                    return false;
                }
                else if (GetComponentInParent<Gs2FormationConvertSlotModelToOwnSlot>(true) != null) {
                    return false;
                }
                else if (GetComponentInParent<Gs2FormationOwnSlotList>(true) != null) {
                    return false;
                }
                else {
                    return true;
                }
            }
            return false;
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2FormationOwnSlotContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FormationOwnSlotContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2FormationOwnSlotContext
    {
        [SerializeField]
        private OwnSlot _slot;
        public OwnSlot Slot
        {
            get => _slot;
            set => SetOwnSlot(value);
        }

        public void SetOwnSlot(OwnSlot OwnSlot) {
            this._slot = OwnSlot;

            this.OnUpdate.Invoke();
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FormationOwnSlotContext
    {

    }
}