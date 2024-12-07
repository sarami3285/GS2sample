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

namespace Gs2.Unity.UiKit.Gs2Stamina.Editor
{
    [CustomEditor(typeof(Gs2StaminaStaminaSetRecoverIntervalActionLabel))]
    public class Gs2StaminaStaminaSetRecoverIntervalActionLabelEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2StaminaStaminaSetRecoverIntervalActionLabel;

            if (original == null) return;

            if (original.action == null) {
                EditorGUILayout.HelpBox("Gs2StaminaStaminaSetRecoverIntervalAction not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2StaminaStaminaSetRecoverIntervalAction>();
                }
                return;
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("action"), true);
            original.format = EditorGUILayout.TextField("Format", original.format);

            GUILayout.Label("Add Format Parameter");
            if (GUILayout.Button("KeyId")) {
                original.format += "{keyId}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("SignedStatusBody")) {
                original.format += "{signedStatusBody}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("SignedStatusSignature")) {
                original.format += "{signedStatusSignature}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}