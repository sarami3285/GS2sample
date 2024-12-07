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

using Gs2.Unity.Gs2Schedule.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Schedule.Context;
using Gs2.Unity.UiKit.Gs2Schedule.Fetcher;
using UnityEditor;
using UnityEngine;
using Event = Gs2.Unity.Gs2Schedule.ScriptableObject.Event;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2Schedule.Localization.Editor
{
    [CustomEditor(typeof(Gs2ScheduleTriggerLocalizationVariables))]
    public class Gs2ScheduleTriggerLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ScheduleTriggerLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2ScheduleOwnTriggerFetcher>() ?? original.GetComponentInParent<Gs2ScheduleOwnTriggerFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2ScheduleOwnTriggerFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2ScheduleOwnTriggerFetcher>();
                }
            }
            else {
                if (fetcher.gameObject.GetComponentInParent<Gs2ScheduleOwnTriggerList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2ScheduleOwnTriggerFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Trigger is auto assign from Gs2ScheduleOwnTriggerList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2ScheduleOwnTriggerContext>() ?? original.GetComponentInParent<Gs2ScheduleOwnTriggerContext>(true);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2ScheduleOwnTriggerFetcher), false);
                    EditorGUI.indentLevel++;
                    context.Trigger = EditorGUILayout.ObjectField("Trigger", context.Trigger, typeof(OwnTrigger), false) as OwnTrigger;
                    if (context.Trigger != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.Trigger?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("TriggerName", context.Trigger?.TriggerName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            original.target = EditorGUILayout.ObjectField("Target", original.target, typeof(LocalizeStringEvent), true) as LocalizeStringEvent;

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif