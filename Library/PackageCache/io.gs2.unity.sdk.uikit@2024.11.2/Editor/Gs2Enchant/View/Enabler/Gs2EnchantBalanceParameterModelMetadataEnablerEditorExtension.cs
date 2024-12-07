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
    [CustomEditor(typeof(Gs2EnchantBalanceParameterModelMetadataEnabler))]
    public class Gs2EnchantBalanceParameterModelMetadataEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2EnchantBalanceParameterModelMetadataEnabler;

            if (original == null) return;

            var context = original.GetComponent<Gs2EnchantBalanceParameterModelContext>() ?? original.GetComponentInParent<Gs2EnchantBalanceParameterModelContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2EnchantBalanceParameterModelContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2EnchantBalanceParameterModelContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2EnchantBalanceParameterModelList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2EnchantBalanceParameterModelContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("BalanceParameterModel is auto assign from Gs2EnchantBalanceParameterModelList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2EnchantBalanceParameterModelContext), false);
                    EditorGUI.indentLevel++;
                    context.BalanceParameterModel = EditorGUILayout.ObjectField("BalanceParameterModel", context.BalanceParameterModel, typeof(BalanceParameterModel), false) as BalanceParameterModel;
                    if (context.BalanceParameterModel != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.BalanceParameterModel?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("ParameterName", context.BalanceParameterModel?.ParameterName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2EnchantBalanceParameterModelMetadataEnabler.Expression.In || original.expression == Gs2EnchantBalanceParameterModelMetadataEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableMetadatas"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableMetadata"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}