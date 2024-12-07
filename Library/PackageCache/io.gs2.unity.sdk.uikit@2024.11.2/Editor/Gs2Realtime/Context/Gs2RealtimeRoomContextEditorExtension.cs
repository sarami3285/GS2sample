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

using Gs2.Unity.Gs2Realtime.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Realtime.Context;
using Gs2.Unity.UiKit.Gs2Realtime.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Realtime.Editor
{
    [CustomEditor(typeof(Gs2RealtimeRoomContext))]
    public class Gs2RealtimeRoomContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2RealtimeRoomContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.Room == null) {
                EditorGUILayout.HelpBox("Room not assigned.", MessageType.Error);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_room"), true);
            }
            else {
                original.Room = EditorGUILayout.ObjectField("Room", original.Room, typeof(Room), false) as Room;
                EditorGUI.BeginDisabledGroup(true);
                if (original.Room != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", original.Room?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("RoomName", original.Room?.RoomName?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}