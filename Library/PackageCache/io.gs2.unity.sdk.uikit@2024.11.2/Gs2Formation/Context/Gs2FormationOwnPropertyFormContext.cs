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

using Gs2.Unity.Gs2Formation.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Formation.Context
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Formation/PropertyForm/Context/Gs2FormationOwnPropertyFormContext")]
    public partial class Gs2FormationOwnPropertyFormContext : Gs2FormationPropertyFormModelContext
    {
        public new void Start() {
        }
        public override bool HasError() {
            var hasError = base.HasError();
            if (PropertyForm == null || hasError) {
                if (GetComponentInParent<Gs2FormationOwnPropertyFormList>(true) != null) {
                    return false;
                }
                if (GetComponentInParent<Gs2FormationConvertPropertyFormModelToOwnPropertyForm>(true) != null) {
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

    public partial class Gs2FormationOwnPropertyFormContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FormationOwnPropertyFormContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2FormationOwnPropertyFormContext
    {
        [SerializeField]
        private OwnPropertyForm _propertyForm;
        public OwnPropertyForm PropertyForm
        {
            get => _propertyForm;
            set => SetOwnPropertyForm(value);
        }

        public void SetOwnPropertyForm(OwnPropertyForm propertyForm) {
            if (propertyForm == null) return;
            this.PropertyFormModel = PropertyFormModel.New(
                Namespace.New(
                    propertyForm.NamespaceName
                ),
                propertyForm.PropertyFormModelName
            );
            this._propertyForm = propertyForm;

            this.OnUpdate.Invoke();
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FormationOwnPropertyFormContext
    {

    }
}