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

namespace Gs2.Unity.UiKit.Gs2Experience.SpriteSwitcher.Editor
{
    [CustomEditor(typeof(Gs2ExperienceOwnStatusPropertyIdSpriteSwitcher))]
    public class Gs2ExperienceOwnStatusPropertyIdSpriteSwitcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ExperienceOwnStatusPropertyIdSpriteSwitcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2ExperienceOwnStatusContext>() ?? original.GetComponentInParent<Gs2ExperienceOwnStatusContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2ExperienceOwnStatusContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2ExperienceOwnStatusContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2ExperienceOwnStatusList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2ExperienceOwnStatusContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Status is auto assign from Gs2ExperienceOwnStatusList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2ExperienceOwnStatusContext), false);
                    EditorGUI.indentLevel++;
                    context.Status = EditorGUILayout.ObjectField("Status", context.Status, typeof(OwnStatus), false) as OwnStatus;
                    if (context.Status != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.Status?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("ExperienceName", context.Status?.ExperienceName?.ToString());
                        EditorGUILayout.TextField("PropertyId", context.Status?.PropertyId?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2ExperienceOwnStatusPropertyIdSpriteSwitcher.Expression.In || original.expression == Gs2ExperienceOwnStatusPropertyIdSpriteSwitcher.Expression.NotIn) {
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