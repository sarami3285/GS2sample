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
 *
 * deny overwrite
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

using Gs2.Unity.Gs2Mission.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Mission.Context;
using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Mission.Editor
{
    [CustomEditor(typeof(Gs2MissionCompleteReceiveRewardsAction))]
    public class Gs2MissionCompleteReceiveRewardsActionEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MissionCompleteReceiveRewardsAction;

            if (original == null) return;

            var context = original.GetComponent<Gs2MissionOwnCompleteContext>() ?? original.GetComponentInParent<Gs2MissionOwnCompleteContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2MissionOwnCompleteContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2MissionOwnCompleteContext>();
                }
            }
            else {
                if (context.transform.parent.GetComponent<Gs2MissionOwnCompleteList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2MissionOwnCompleteContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Complete is auto assign from Gs2MissionCompleteList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2MissionOwnCompleteContext), false);
                    EditorGUI.indentLevel++;
                    context.Complete = EditorGUILayout.ObjectField("OwnComplete", context.Complete, typeof(OwnComplete), false) as OwnComplete;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.Complete?.NamespaceName.ToString());
                    EditorGUILayout.TextField("MissionGroupName", context.Complete?.MissionGroupName.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onChangeMissionTaskName"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onReceiveRewardsComplete"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onError"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}