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

#if GS2_ENABLE_LOCALIZATION

using Gs2.Unity.Gs2SkillTree.ScriptableObject;
using Gs2.Unity.UiKit.Gs2SkillTree.Context;
using Gs2.Unity.UiKit.Gs2SkillTree.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2SkillTree.Localization.Editor
{
    [CustomEditor(typeof(Gs2SkillTreeNodeModelLocalizationVariables))]
    public class Gs2SkillTreeNodeModelLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2SkillTreeNodeModelLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2SkillTreeNodeModelFetcher>() ?? original.GetComponentInParent<Gs2SkillTreeNodeModelFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2SkillTreeNodeModelFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2SkillTreeNodeModelFetcher>();
                }
            }
            else {
                if (fetcher.gameObject.GetComponentInParent<Gs2SkillTreeNodeModelList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2SkillTreeNodeModelFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("NodeModel is auto assign from Gs2SkillTreeNodeModelList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2SkillTreeNodeModelContext>() ?? original.GetComponentInParent<Gs2SkillTreeNodeModelContext>(true);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2SkillTreeNodeModelFetcher), false);
                    EditorGUI.indentLevel++;
                    context.NodeModel = EditorGUILayout.ObjectField("NodeModel", context.NodeModel, typeof(NodeModel), false) as NodeModel;
                    if (context.NodeModel != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.NodeModel?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("NodeModelName", context.NodeModel?.NodeModelName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            original.target = EditorGUILayout.ObjectField("Target", original.target, typeof(LocalizeStringEvent), true) as LocalizeStringEvent;

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif