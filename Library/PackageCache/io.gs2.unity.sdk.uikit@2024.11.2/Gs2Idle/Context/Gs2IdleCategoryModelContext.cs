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

using Gs2.Unity.Gs2Idle.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Idle.Context
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Idle/CategoryModel/Context/Gs2IdleCategoryModelContext")]
    public partial class Gs2IdleCategoryModelContext : MonoBehaviour
    {
        public void Start() {
            if (CategoryModel == null) {
                Debug.LogWarning($"{gameObject.GetFullPath()}: CategoryModel is not set in Gs2IdleCategoryModelContext.");
            }
        }

        public virtual bool HasError() {
            if (CategoryModel == null) {
                if (GetComponentInParent<Gs2IdleCategoryModelList>(true) != null) {
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

    public partial class Gs2IdleCategoryModelContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2IdleCategoryModelContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2IdleCategoryModelContext
    {
        [SerializeField]
        private CategoryModel _categoryModel;
        public CategoryModel CategoryModel
        {
            get => _categoryModel;
            set => SetCategoryModel(value);
        }

        public void SetCategoryModel(CategoryModel categoryModel) {
            if (categoryModel == null) return;

            this._categoryModel = categoryModel;

            this.OnUpdate.Invoke();
        }

        public UnityEvent OnUpdate = new UnityEvent();
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2IdleCategoryModelContext
    {

    }
}