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
using Gs2.Unity.UiKit.Gs2Enchant.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Enchant.Enabler.Editor
{
    [CustomEditor(typeof(Gs2EnchantRarityParameterModelMetadataEnabler))]
    public class Gs2EnchantRarityParameterModelMetadataEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2EnchantRarityParameterModelMetadataEnabler;

            if (original == null) return;

            var context = original.GetComponent<Gs2EnchantRarityParameterModelContext>() ?? original.GetComponentInParent<Gs2EnchantRarityParameterModelContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2EnchantRarityParameterModelContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2EnchantRarityParameterModelContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2EnchantRarityParameterModelList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2EnchantRarityParameterModelContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("RarityParameterModel is auto assign from Gs2EnchantRarityParameterModelList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2EnchantRarityParameterModelContext), false);
                    EditorGUI.indentLevel++;
                    context.RarityParameterModel = EditorGUILayout.ObjectField("RarityParameterModel", context.RarityParameterModel, typeof(RarityParameterModel), false) as RarityParameterModel;
                    if (context.RarityParameterModel != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.RarityParameterModel?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("ParameterName", context.RarityParameterModel?.ParameterName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2EnchantRarityParameterModelMetadataEnabler.Expression.In || original.expression == Gs2EnchantRarityParameterModelMetadataEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableMetadatas"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableMetadata"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}