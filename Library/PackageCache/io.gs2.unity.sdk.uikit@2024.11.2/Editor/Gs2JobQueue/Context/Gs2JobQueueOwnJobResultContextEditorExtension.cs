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
    [CustomEditor(typeof(Gs2JobQueueOwnJobResultContext))]
    public class Gs2JobQueueOwnJobResultContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2JobQueueOwnJobResultContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.JobResult == null) {
                EditorGUILayout.HelpBox("OwnJobResult not assigned.", MessageType.Error);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_jobResult"), true);
            }
            else {
                original.JobResult = EditorGUILayout.ObjectField("OwnJobResult", original.JobResult, typeof(OwnJobResult), false) as OwnJobResult;
                EditorGUI.BeginDisabledGroup(true);
                if (original.JobResult != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", original.JobResult?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("JobName", original.JobResult?.JobName?.ToString());
                    EditorGUILayout.TextField("TryNumber", original.JobResult?.TryNumber.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}