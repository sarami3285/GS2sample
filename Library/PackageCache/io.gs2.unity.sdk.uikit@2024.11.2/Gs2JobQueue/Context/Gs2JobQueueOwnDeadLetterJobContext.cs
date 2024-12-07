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

using Gs2.Unity.Gs2JobQueue.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2JobQueue.Context
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/JobQueue/DeadLetterJob/Context/Gs2JobQueueOwnDeadLetterJobContext")]
    public partial class Gs2JobQueueOwnDeadLetterJobContext : MonoBehaviour
    {
        public void Start() {
            if (DeadLetterJob == null) {
                Debug.LogWarning($"{gameObject.GetFullPath()}: DeadLetterJob is not set in Gs2JobQueueOwnDeadLetterJobContext.");
            }
        }
        public virtual bool HasError() {
            if (DeadLetterJob == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2JobQueueOwnDeadLetterJobContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2JobQueueOwnDeadLetterJobContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2JobQueueOwnDeadLetterJobContext
    {
        [SerializeField]
        private OwnDeadLetterJob _deadLetterJob;
        public OwnDeadLetterJob DeadLetterJob
        {
            get => _deadLetterJob;
            set => SetOwnDeadLetterJob(value);
        }

        public void SetOwnDeadLetterJob(OwnDeadLetterJob deadLetterJob) {
            if (deadLetterJob == null) return;
            this._deadLetterJob = deadLetterJob;

            this.OnUpdate.Invoke();
        }

        public UnityEvent OnUpdate = new UnityEvent();
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2JobQueueOwnDeadLetterJobContext
    {

    }
}