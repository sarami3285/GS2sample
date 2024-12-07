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

#if GS2_ENABLE_LOCALIZATION

using Gs2.Unity.Gs2Realtime.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Realtime.Context;
using Gs2.Unity.UiKit.Gs2Realtime.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2Realtime.Localization.Editor
{
    [CustomEditor(typeof(Gs2RealtimeRoomLocalizationVariables))]
    public class Gs2RealtimeRoomLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2RealtimeRoomLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2RealtimeRoomFetcher>() ?? original.GetComponentInParent<Gs2RealtimeRoomFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2RealtimeRoomFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2RealtimeRoomFetcher>();
                }
            }
            else {
                var context = original.GetComponent<Gs2RealtimeRoomContext>() ?? original.GetComponentInParent<Gs2RealtimeRoomContext>(true);
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2RealtimeRoomFetcher), false);
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
            original.target = EditorGUILayout.ObjectField("Target", original.target, typeof(LocalizeStringEvent), true) as LocalizeStringEvent;

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif