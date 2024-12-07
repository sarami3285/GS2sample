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

using Gs2.Unity.Gs2SkillTree.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2SkillTree.Context
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/SkillTree/NodeModel/Context/Gs2SkillTreeNodeModelContext")]
    public partial class Gs2SkillTreeNodeModelContext : MonoBehaviour
    {
        public void Start() {
            if (NodeModel == null) {
                Debug.LogWarning($"{gameObject.GetFullPath()}: NodeModel is not set in Gs2SkillTreeNodeModelContext.");
            }
        }

        public virtual bool HasError() {
            if (NodeModel == null) {
                if (GetComponentInParent<Gs2SkillTreeNodeModelList>(true) != null) {
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

    public partial class Gs2SkillTreeNodeModelContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2SkillTreeNodeModelContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2SkillTreeNodeModelContext
    {
        [SerializeField]
        private NodeModel _nodeModel;
        public NodeModel NodeModel
        {
            get => _nodeModel;
            set => SetNodeModel(value);
        }

        public void SetNodeModel(NodeModel nodeModel) {
            if (nodeModel == null) return;

            this._nodeModel = nodeModel;

            this.OnUpdate.Invoke();
        }

        public UnityEvent OnUpdate = new UnityEvent();
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2SkillTreeNodeModelContext
    {

    }
}