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

using Gs2.Unity.Gs2Stamina.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Stamina.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Stamina.Editor
{
    [CustomEditor(typeof(Gs2StaminaStaminaModelMaxCapacitySpriteSwitcher))]
    public class Gs2StaminaStaminaModelMaxCapacitySpriteSwitcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2StaminaStaminaModelMaxCapacitySpriteSwitcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2StaminaStaminaModelContext>() ?? original.GetComponentInParent<Gs2StaminaStaminaModelContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2StaminaStaminaModelContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2StaminaStaminaModelContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2StaminaStaminaModelList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2StaminaStaminaModelContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("StaminaModel is auto assign from Gs2StaminaStaminaModelList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2StaminaStaminaModelContext), false);
                    EditorGUI.indentLevel++;
                    context.StaminaModel = EditorGUILayout.ObjectField("StaminaModel", context.StaminaModel, typeof(StaminaModel), false) as StaminaModel;
                    if (context.StaminaModel != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.StaminaModel?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("StaminaName", context.StaminaModel?.StaminaName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2StaminaStaminaModelMaxCapacitySpriteSwitcher.Expression.In || original.expression == Gs2StaminaStaminaModelMaxCapacitySpriteSwitcher.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyMaxCapacities"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyMaxCapacity"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("sprite"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}