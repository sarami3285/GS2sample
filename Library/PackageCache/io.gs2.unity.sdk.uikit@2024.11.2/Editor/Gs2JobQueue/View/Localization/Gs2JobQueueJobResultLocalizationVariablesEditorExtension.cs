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

using Gs2.Unity.Gs2JobQueue.ScriptableObject;
using Gs2.Unity.UiKit.Gs2JobQueue.Context;
using Gs2.Unity.UiKit.Gs2JobQueue.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2JobQueue.Localization.Editor
{
    [CustomEditor(typeof(Gs2JobQueueJobResultLocalizationVariables))]
    public class Gs2JobQueueJobResultLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2JobQueueJobResultLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2JobQueueOwnJobResultFetcher>() ?? original.GetComponentInParent<Gs2JobQueueOwnJobResultFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2JobQueueOwnJobResultFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2JobQueueOwnJobResultFetcher>();
                }
            }
            else {
                var context = original.GetComponent<Gs2JobQueueOwnJobResultContext>() ?? original.GetComponentInParent<Gs2JobQueueOwnJobResultContext>(true);
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2JobQueueOwnJobResultFetcher), false);
                EditorGUI.indentLevel++;
                context.JobResult = EditorGUILayout.ObjectField("JobResult", context.JobResult, typeof(OwnJobResult), false) as OwnJobResult;
                if (context.JobResult != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.JobResult?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("JobName", context.JobResult?.JobName?.ToString());
                    EditorGUILayout.TextField("TryNumber", context.JobResult?.TryNumber.ToString());
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