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

using Gs2.Unity.Gs2Enchant.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Enchant.Context
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Enchant/RarityParameterStatus/Context/Gs2EnchantOwnRarityParameterStatusContext")]
    public partial class Gs2EnchantOwnRarityParameterStatusContext : MonoBehaviour
    {
        public void Start() {
            if (RarityParameterStatus == null) {
                Debug.LogWarning($"{gameObject.GetFullPath()}: RarityParameterStatus is not set in Gs2EnchantOwnRarityParameterStatusContext.");
            }
        }
        public virtual bool HasError() {
            if (RarityParameterStatus == null) {
                if (GetComponentInParent<Gs2EnchantOwnRarityParameterStatusList>(true) != null) {
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

    public partial class Gs2EnchantOwnRarityParameterStatusContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2EnchantOwnRarityParameterStatusContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2EnchantOwnRarityParameterStatusContext
    {
        [SerializeField]
        private OwnRarityParameterStatus _rarityParameterStatus;
        public OwnRarityParameterStatus RarityParameterStatus
        {
            get => _rarityParameterStatus;
            set => SetOwnRarityParameterStatus(value);
        }

        public void SetOwnRarityParameterStatus(OwnRarityParameterStatus rarityParameterStatus) {
            if (rarityParameterStatus == null) return;
            this._rarityParameterStatus = rarityParameterStatus;

            this.OnUpdate.Invoke();
        }

        public UnityEvent OnUpdate = new UnityEvent();
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2EnchantOwnRarityParameterStatusContext
    {

    }
}