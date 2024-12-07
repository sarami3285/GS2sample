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

using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Enchant.Editor
{
    [CustomEditor(typeof(Gs2EnchantRarityParameterStatusVerifyRarityParameterStatusActionLabel))]
    public class Gs2EnchantRarityParameterStatusVerifyRarityParameterStatusActionLabelEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2EnchantRarityParameterStatusVerifyRarityParameterStatusActionLabel;

            if (original == null) return;

            if (original.action == null) {
                EditorGUILayout.HelpBox("Gs2EnchantRarityParameterStatusVerifyRarityParameterStatusAction not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2EnchantRarityParameterStatusVerifyRarityParameterStatusAction>();
                }
                return;
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("action"), true);
            original.format = EditorGUILayout.TextField("Format", original.format);

            GUILayout.Label("Add Format Parameter");
            if (GUILayout.Button("VerifyType")) {
                original.format += "{verifyType}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("ParameterValueName")) {
                original.format += "{parameterValueName}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("ParameterCount")) {
                original.format += "{parameterCount}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}