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

using Gs2.Unity.Gs2Showcase.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Showcase.Context
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Showcase/SalesItemGroup/Context/Gs2ShowcaseOwnSalesItemGroupContext")]
    public partial class Gs2ShowcaseOwnSalesItemGroupContext : MonoBehaviour
    {
        public void Start() {
            if (SalesItemGroup == null) {
                Debug.LogWarning($"{gameObject.GetFullPath()}: SalesItemGroup is not set in Gs2ShowcaseOwnSalesItemGroupContext.");
            }
        }
        public virtual bool HasError() {
            if (SalesItemGroup == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ShowcaseOwnSalesItemGroupContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ShowcaseOwnSalesItemGroupContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ShowcaseOwnSalesItemGroupContext
    {
        [SerializeField]
        private OwnSalesItemGroup _salesItemGroup;
        public OwnSalesItemGroup SalesItemGroup
        {
            get => _salesItemGroup;
            set => SetOwnSalesItemGroup(value);
        }

        public void SetOwnSalesItemGroup(OwnSalesItemGroup salesItemGroup) {
            if (salesItemGroup == null) return;
            this._salesItemGroup = salesItemGroup;

            this.OnUpdate.Invoke();
        }

        public UnityEvent OnUpdate = new UnityEvent();
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ShowcaseOwnSalesItemGroupContext
    {

    }
}