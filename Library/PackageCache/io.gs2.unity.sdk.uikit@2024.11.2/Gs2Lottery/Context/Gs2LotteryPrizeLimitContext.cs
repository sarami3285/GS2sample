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

using Gs2.Unity.Gs2Lottery.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Lottery.Context
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Lottery/PrizeLimit/Context/Gs2LotteryPrizeLimitContext")]
    public partial class Gs2LotteryPrizeLimitContext : MonoBehaviour
    {
        public void Start() {
            if (PrizeLimit == null) {
                Debug.LogWarning($"{gameObject.GetFullPath()}: PrizeLimit is not set in Gs2LotteryPrizeLimitContext.");
            }
        }

        public virtual bool HasError() {
            if (PrizeLimit == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2LotteryPrizeLimitContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2LotteryPrizeLimitContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2LotteryPrizeLimitContext
    {
        [SerializeField]
        private PrizeLimit _prizeLimit;
        public PrizeLimit PrizeLimit
        {
            get => _prizeLimit;
            set => SetPrizeLimit(value);
        }

        public void SetPrizeLimit(PrizeLimit prizeLimit) {
            if (prizeLimit == null) return;

            this._prizeLimit = prizeLimit;

            this.OnUpdate.Invoke();
        }

        public UnityEvent OnUpdate = new UnityEvent();
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LotteryPrizeLimitContext
    {

    }
}