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
using Gs2.Unity.UiKit.Gs2Schedule.Fetcher;
using UnityEditor;
using UnityEngine;
using Event = Gs2.Unity.Gs2Schedule.ScriptableObject.Event;

namespace Gs2.Unity.UiKit.Gs2Schedule.Editor
{
    [CustomEditor(typeof(Gs2ScheduleOwnEventLabel))]
    public class Gs2ScheduleOwnEventLabelEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ScheduleOwnEventLabel;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2ScheduleOwnEventFetcher>() ?? original.GetComponentInParent<Gs2ScheduleOwnEventFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2ScheduleOwnEventFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2ScheduleOwnEventFetcher>();
                }
            }
            else {
                if (fetcher.gameObject.GetComponentInParent<Gs2ScheduleOwnEventList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2ScheduleOwnEventFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Event is auto assign from Gs2ScheduleOwnEventList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2ScheduleOwnEventFetcher), false);
                    EditorGUI.indentLevel++;
                    if (fetcher.Context != null) {
                        EditorGUILayout.ObjectField("Event", fetcher.Context.Event, typeof(OwnEvent), false);
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", fetcher.Context.Event?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("EventName", fetcher.Context.Event?.EventName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            original.format = EditorGUILayout.TextField("Format", original.format);

            GUILayout.Label("Add Format Parameter");
            if (GUILayout.Button("Name")) {
                original.format += "{name}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("Metadata")) {
                original.format += "{metadata}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("ScheduleType")) {
                original.format += "{scheduleType}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("RepeatType")) {
                original.format += "{repeatType}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("AbsoluteBegin(Year:2020)")) {
                original.format += "{absoluteBegin:yyyy}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("AbsoluteBegin(Year:20)")) {
                original.format += "{absoluteBegin:yy}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("AbsoluteBegin(Month:12)")) {
                original.format += "{absoluteBegin:MM}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("AbsoluteBegin(Month:Dec)")) {
                original.format += "{absoluteBegin:MMM}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("AbsoluteBegin(Day:25)")) {
                original.format += "{absoluteBegin:dd}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("AbsoluteBegin(Hour:6)")) {
                original.format += "{absoluteBegin:hh}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("AbsoluteBegin(Hour:18)")) {
                original.format += "{absoluteBegin:HH}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("AbsoluteBegin(AM/PM)")) {
                original.format += "{absoluteBegin:tt}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("AbsoluteBegin(Min:05)")) {
                original.format += "{absoluteBegin:mm}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("AbsoluteBegin(Sec:09)")) {
                original.format += "{absoluteBegin:ss}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("AbsoluteEnd(Year:2020)")) {
                original.format += "{absoluteEnd:yyyy}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("AbsoluteEnd(Year:20)")) {
                original.format += "{absoluteEnd:yy}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("AbsoluteEnd(Month:12)")) {
                original.format += "{absoluteEnd:MM}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("AbsoluteEnd(Month:Dec)")) {
                original.format += "{absoluteEnd:MMM}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("AbsoluteEnd(Day:25)")) {
                original.format += "{absoluteEnd:dd}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("AbsoluteEnd(Hour:6)")) {
                original.format += "{absoluteEnd:hh}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("AbsoluteEnd(Hour:18)")) {
                original.format += "{absoluteEnd:HH}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("AbsoluteEnd(AM/PM)")) {
                original.format += "{absoluteEnd:tt}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("AbsoluteEnd(Min:05)")) {
                original.format += "{absoluteEnd:mm}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("AbsoluteEnd(Sec:09)")) {
                original.format += "{absoluteEnd:ss}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("RepeatBeginDayOfMonth")) {
                original.format += "{repeatBeginDayOfMonth}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("RepeatEndDayOfMonth")) {
                original.format += "{repeatEndDayOfMonth}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("RepeatBeginDayOfWeek")) {
                original.format += "{repeatBeginDayOfWeek}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("RepeatEndDayOfWeek")) {
                original.format += "{repeatEndDayOfWeek}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("RepeatBeginHour")) {
                original.format += "{repeatBeginHour}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("RepeatEndHour")) {
                original.format += "{repeatEndHour}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("RelativeTriggerName")) {
                original.format += "{relativeTriggerName}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}