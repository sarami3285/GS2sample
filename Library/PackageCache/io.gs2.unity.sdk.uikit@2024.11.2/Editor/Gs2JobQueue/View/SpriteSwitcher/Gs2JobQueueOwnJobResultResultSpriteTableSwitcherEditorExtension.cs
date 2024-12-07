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
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2JobQueue.SpriteSwitcher.Editor
{
    [CustomEditor(typeof(Gs2JobQueueOwnJobResultResultSpriteTableSwitcher))]
    public class Gs2JobQueueOwnJobResultResultSpriteTableSwitcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2JobQueueOwnJobResultResultSpriteTableSwitcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2JobQueueOwnJobResultContext>() ?? original.GetComponentInParent<Gs2JobQueueOwnJobResultContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2JobQueueOwnJobResultContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2JobQueueOwnJobResultContext>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2JobQueueOwnJobResultContext), false);
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
            EditorGUILayout.PropertyField(serializedObject.FindProperty("sprites"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultSprite"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}