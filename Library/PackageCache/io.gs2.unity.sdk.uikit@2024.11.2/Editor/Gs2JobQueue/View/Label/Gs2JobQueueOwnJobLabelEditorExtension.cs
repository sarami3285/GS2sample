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

using Gs2.Unity.Gs2JobQueue.ScriptableObject;
using Gs2.Unity.UiKit.Gs2JobQueue.Context;
using Gs2.Unity.UiKit.Gs2JobQueue.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2JobQueue.Editor
{
    [CustomEditor(typeof(Gs2JobQueueOwnJobLabel))]
    public class Gs2JobQueueOwnJobLabelEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2JobQueueOwnJobLabel;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2JobQueueOwnJobFetcher>() ?? original.GetComponentInParent<Gs2JobQueueOwnJobFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2JobQueueOwnJobFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2JobQueueOwnJobFetcher>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2JobQueueOwnJobFetcher), false);
                EditorGUI.indentLevel++;
                if (fetcher.Context != null) {
                    EditorGUILayout.ObjectField("Job", fetcher.Context.Job, typeof(OwnJob), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", fetcher.Context.Job?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("JobName", fetcher.Context.Job?.JobName?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            original.format = EditorGUILayout.TextField("Format", original.format);

            GUILayout.Label("Add Format Parameter");
            if (GUILayout.Button("JobId")) {
                original.format += "{jobId}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("ScriptId")) {
                original.format += "{scriptId}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("Args")) {
                original.format += "{args}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("CurrentRetryCount")) {
                original.format += "{currentRetryCount}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("MaxTryCount")) {
                original.format += "{maxTryCount}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}