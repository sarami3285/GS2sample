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

using Gs2.Unity.Gs2Chat.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Chat.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Chat.SpriteSwitcher.Editor
{
    [CustomEditor(typeof(Gs2ChatRoomMetadataSpriteSwitcher))]
    public class Gs2ChatRoomMetadataSpriteSwitcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ChatRoomMetadataSpriteSwitcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2ChatRoomContext>() ?? original.GetComponentInParent<Gs2ChatRoomContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2ChatRoomContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2ChatRoomContext>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2ChatRoomContext), false);
                EditorGUI.indentLevel++;
                context.Room = EditorGUILayout.ObjectField("Room", context.Room, typeof(Room), false) as Room;
                if (context.Room != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.Room?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("RoomName", context.Room?.RoomName?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2ChatRoomMetadataSpriteSwitcher.Expression.In || original.expression == Gs2ChatRoomMetadataSpriteSwitcher.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyMetadatas"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyMetadata"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("sprite"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}