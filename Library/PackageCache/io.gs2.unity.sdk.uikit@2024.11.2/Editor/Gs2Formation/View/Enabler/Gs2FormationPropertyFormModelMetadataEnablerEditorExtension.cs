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
using Gs2.Unity.UiKit.Gs2Formation.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Formation.Enabler.Editor
{
    [CustomEditor(typeof(Gs2FormationPropertyFormModelMetadataEnabler))]
    public class Gs2FormationPropertyFormModelMetadataEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2FormationPropertyFormModelMetadataEnabler;

            if (original == null) return;

            var context = original.GetComponent<Gs2FormationPropertyFormModelContext>() ?? original.GetComponentInParent<Gs2FormationPropertyFormModelContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2FormationPropertyFormModelContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2FormationPropertyFormModelContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2FormationPropertyFormModelList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2FormationPropertyFormModelContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("PropertyFormModel is auto assign from Gs2FormationPropertyFormModelList.", MessageType.Info);
                }
                else if (context.gameObject.GetComponentInParent<Gs2FormationOwnPropertyFormList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2FormationOwnPropertyFormContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("PropertyFormModel is auto assign from Gs2FormationOwnPropertyFormList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2FormationPropertyFormModelContext), false);
                    EditorGUI.indentLevel++;
                    context.PropertyFormModel = EditorGUILayout.ObjectField("PropertyFormModel", context.PropertyFormModel, typeof(PropertyFormModel), false) as PropertyFormModel;
                    if (context.PropertyFormModel != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.PropertyFormModel?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("PropertyFormModelName", context.PropertyFormModel?.PropertyFormModelName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2FormationPropertyFormModelMetadataEnabler.Expression.In || original.expression == Gs2FormationPropertyFormModelMetadataEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableMetadatas"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableMetadata"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}