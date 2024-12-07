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

namespace Gs2.Unity.UiKit.Gs2Enchant.SpriteSwitcher.Editor
{
    [CustomEditor(typeof(Gs2EnchantOwnRarityParameterStatusPropertyIdSpriteSwitcher))]
    public class Gs2EnchantOwnRarityParameterStatusPropertyIdSpriteSwitcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2EnchantOwnRarityParameterStatusPropertyIdSpriteSwitcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2EnchantOwnRarityParameterStatusContext>() ?? original.GetComponentInParent<Gs2EnchantOwnRarityParameterStatusContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2EnchantOwnRarityParameterStatusContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2EnchantOwnRarityParameterStatusContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2EnchantOwnRarityParameterStatusList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2EnchantOwnRarityParameterStatusContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("RarityParameterStatus is auto assign from Gs2EnchantOwnRarityParameterStatusList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2EnchantOwnRarityParameterStatusContext), false);
                    EditorGUI.indentLevel++;
                    context.RarityParameterStatus = EditorGUILayout.ObjectField("RarityParameterStatus", context.RarityParameterStatus, typeof(OwnRarityParameterStatus), false) as OwnRarityParameterStatus;
                    if (context.RarityParameterStatus != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.RarityParameterStatus?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("ParameterName", context.RarityParameterStatus?.ParameterName?.ToString());
                        EditorGUILayout.TextField("PropertyId", context.RarityParameterStatus?.PropertyId?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2EnchantOwnRarityParameterStatusPropertyIdSpriteSwitcher.Expression.In || original.expression == Gs2EnchantOwnRarityParameterStatusPropertyIdSpriteSwitcher.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyPropertyIds"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyPropertyId"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("sprite"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}