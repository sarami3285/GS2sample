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
using Gs2.Unity.UiKit.Gs2SkillTree.Context;
using Gs2.Unity.UiKit.Gs2SkillTree.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2SkillTree.Editor
{
    [CustomEditor(typeof(Gs2SkillTreeNodeModelContext))]
    public class Gs2SkillTreeNodeModelContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2SkillTreeNodeModelContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.NodeModel == null) {
                var list = original.GetComponentInParent<Gs2SkillTreeNodeModelList>(true);
                if (list != null) {
                    EditorGUILayout.HelpBox("NodeModel is auto assign from Gs2SkillTreeNodeModelList.", MessageType.Info);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("List", list, typeof(Gs2SkillTreeNodeModelList), false);
                    EditorGUI.EndDisabledGroup();
                }
                else {
                    EditorGUILayout.HelpBox("NodeModel not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_nodeModel"), true);
                }
            }
            else {
                original.NodeModel = EditorGUILayout.ObjectField("NodeModel", original.NodeModel, typeof(NodeModel), false) as NodeModel;
                EditorGUI.BeginDisabledGroup(true);
                if (original.NodeModel != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", original.NodeModel?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("NodeModelName", original.NodeModel?.NodeModelName?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}