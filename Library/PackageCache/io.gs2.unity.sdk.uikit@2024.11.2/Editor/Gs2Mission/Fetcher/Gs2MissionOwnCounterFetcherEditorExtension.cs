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
    [CustomEditor(typeof(Gs2MissionOwnCounterFetcher))]
    public class Gs2MissionOwnCounterFetcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MissionOwnCounterFetcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2MissionOwnCounterContext>() ?? original.GetComponentInParent<Gs2MissionOwnCounterContext>(true);
            if (context == null) {
                var context2 = original.GetComponent<Gs2MissionMissionTaskModelContext>() ?? original.GetComponentInParent<Gs2MissionMissionTaskModelContext>(true);
                if (context2 == null) {
                    EditorGUILayout.HelpBox("Gs2MissionOwnCounterContext not found.", MessageType.Error);
                    if (GUILayout.Button("Add Context")) {
                        original.gameObject.AddComponent<Gs2MissionOwnCounterContext>();
                    }
                }
                else {
                    if (context2.transform.parent.GetComponent<Gs2MissionMissionTaskModelContext>() != null) {
                        EditorGUI.BeginDisabledGroup(true);
                        EditorGUILayout.ObjectField("Context", context2.gameObject, typeof(Gs2MissionMissionTaskModelContext), false);
                        EditorGUI.EndDisabledGroup();
                        EditorGUILayout.HelpBox("Counter is auto assign from Gs2MissionOwnCounterList.", MessageType.Info);
                    }
                    else {
                        EditorGUI.BeginDisabledGroup(true);
                        EditorGUILayout.ObjectField("Context", context2.gameObject, typeof(Gs2MissionMissionTaskModelContext), false);
                        EditorGUI.indentLevel++;
                        context2.MissionTaskModel = EditorGUILayout.ObjectField("MissionTaskModel", context2.MissionTaskModel, typeof(MissionTaskModel), false) as MissionTaskModel;
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context2.MissionTaskModel?.NamespaceName.ToString());
                        EditorGUILayout.TextField("MissionTaskModel", context2.MissionTaskModel?.MissionTaskName.ToString());
                        EditorGUI.indentLevel--;
                        EditorGUI.indentLevel--;
                        EditorGUI.EndDisabledGroup();
                    }
                }
            }
            else {
                if (context.transform.parent.GetComponent<Gs2MissionOwnCounterList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2MissionOwnCounterContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Counter is auto assign from Gs2MissionOwnCounterList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2MissionOwnCounterContext), false);
                    EditorGUI.indentLevel++;
                    context.Counter = EditorGUILayout.ObjectField("Counter", context.Counter, typeof(OwnCounter), false) as OwnCounter;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.Counter?.NamespaceName.ToString());
                    EditorGUILayout.TextField("CounterName", context.Counter?.CounterName.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }
            
            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }
    }
}