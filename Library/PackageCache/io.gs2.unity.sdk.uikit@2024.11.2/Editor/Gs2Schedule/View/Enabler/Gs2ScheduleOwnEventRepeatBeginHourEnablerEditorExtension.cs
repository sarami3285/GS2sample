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

using Gs2.Unity.Gs2Schedule.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Schedule.Context;
using UnityEditor;
using UnityEngine;
using Event = Gs2.Unity.Gs2Schedule.ScriptableObject.Event;

namespace Gs2.Unity.UiKit.Gs2Schedule.Editor
{
    [CustomEditor(typeof(Gs2ScheduleOwnEventRepeatBeginHourEnabler))]
    public class Gs2ScheduleOwnEventRepeatBeginHourEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ScheduleOwnEventRepeatBeginHourEnabler;

            if (original == null) return;

            var context = original.GetComponent<Gs2ScheduleOwnEventContext>() ?? original.GetComponentInParent<Gs2ScheduleOwnEventContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2ScheduleOwnEventContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2ScheduleOwnEventContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2ScheduleOwnEventList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2ScheduleOwnEventContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Event is auto assign from Gs2ScheduleOwnEventList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2ScheduleOwnEventContext), false);
                    EditorGUI.indentLevel++;
                    context.Event = EditorGUILayout.ObjectField("Event", context.Event, typeof(OwnEvent), false) as OwnEvent;
                    if (context.Event != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.Event?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("EventName", context.Event?.EventName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2ScheduleOwnEventRepeatBeginHourEnabler.Expression.In || original.expression == Gs2ScheduleOwnEventRepeatBeginHourEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableRepeatBeginHours"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableRepeatBeginHour"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}