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

using Gs2.Unity.Gs2Experience.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Experience.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Experience.Editor
{
    [CustomEditor(typeof(Gs2ExperienceExperienceModelDefaultRankCapEnabler))]
    public class Gs2ExperienceExperienceModelDefaultRankCapEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ExperienceExperienceModelDefaultRankCapEnabler;

            if (original == null) return;

            var context = original.GetComponent<Gs2ExperienceExperienceModelContext>() ?? original.GetComponentInParent<Gs2ExperienceExperienceModelContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2ExperienceExperienceModelContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2ExperienceExperienceModelContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2ExperienceExperienceModelList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2ExperienceExperienceModelContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("ExperienceModel is auto assign from Gs2ExperienceExperienceModelList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2ExperienceExperienceModelContext), false);
                    EditorGUI.indentLevel++;
                    context.ExperienceModel = EditorGUILayout.ObjectField("ExperienceModel", context.ExperienceModel, typeof(ExperienceModel), false) as ExperienceModel;
                    if (context.ExperienceModel != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.ExperienceModel?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("ExperienceName", context.ExperienceModel?.ExperienceName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2ExperienceExperienceModelDefaultRankCapEnabler.Expression.In || original.expression == Gs2ExperienceExperienceModelDefaultRankCapEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableDefaultRankCaps"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableDefaultRankCap"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}