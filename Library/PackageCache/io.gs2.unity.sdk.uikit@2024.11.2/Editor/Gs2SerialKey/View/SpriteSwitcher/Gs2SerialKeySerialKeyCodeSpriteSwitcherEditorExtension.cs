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

using Gs2.Unity.Gs2SerialKey.ScriptableObject;
using Gs2.Unity.UiKit.Gs2SerialKey.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2SerialKey.SpriteSwitcher.Editor
{
    [CustomEditor(typeof(Gs2SerialKeySerialKeyCodeSpriteSwitcher))]
    public class Gs2SerialKeySerialKeyCodeSpriteSwitcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2SerialKeySerialKeyCodeSpriteSwitcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2SerialKeySerialKeyContext>() ?? original.GetComponentInParent<Gs2SerialKeySerialKeyContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2SerialKeySerialKeyContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2SerialKeySerialKeyContext>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2SerialKeySerialKeyContext), false);
                EditorGUI.indentLevel++;
                context.SerialKey = EditorGUILayout.ObjectField("SerialKey", context.SerialKey, typeof(SerialKey), false) as SerialKey;
                if (context.SerialKey != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.SerialKey?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("SerialKeyCode", context.SerialKey?.SerialKeyCode?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2SerialKeySerialKeyCodeSpriteSwitcher.Expression.In || original.expression == Gs2SerialKeySerialKeyCodeSpriteSwitcher.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyCodes"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyCode"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("sprite"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}