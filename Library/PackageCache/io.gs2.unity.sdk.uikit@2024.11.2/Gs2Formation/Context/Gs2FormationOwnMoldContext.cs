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

	[AddComponentMenu("GS2 UIKit/Formation/Mold/Context/Gs2FormationOwnMoldContext")]
    public partial class Gs2FormationOwnMoldContext : Gs2FormationMoldModelContext
    {
        public new void Start() {
        }
        public override bool HasError() {
            var hasError = base.HasError();
            if (Mold == null || hasError) {
                if (GetComponentInParent<Gs2FormationOwnMoldList>(true) != null) {
                    return false;
                }
                if (GetComponentInParent<Gs2FormationConvertMoldModelToOwnMold>(true) != null) {
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

    public partial class Gs2FormationOwnMoldContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FormationOwnMoldContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2FormationOwnMoldContext
    {
        [SerializeField]
        private OwnMold _mold;
        public OwnMold Mold
        {
            get => _mold;
            set => SetOwnMold(value);
        }

        public void SetOwnMold(OwnMold mold) {
            if (mold == null) return;
            this.MoldModel = MoldModel.New(
                Namespace.New(
                    mold.NamespaceName
                ),
                mold.MoldModelName
            );
            this._mold = mold;

            this.OnUpdate.Invoke();
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FormationOwnMoldContext
    {

    }
}