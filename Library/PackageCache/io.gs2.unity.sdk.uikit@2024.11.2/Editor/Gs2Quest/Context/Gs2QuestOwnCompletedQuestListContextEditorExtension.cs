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

using Gs2.Unity.Gs2Quest.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Quest.Context;
using Gs2.Unity.UiKit.Gs2Quest.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Quest.Editor
{
    [CustomEditor(typeof(Gs2QuestOwnCompletedQuestListContext))]
    public class Gs2QuestOwnCompletedQuestListContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2QuestOwnCompletedQuestListContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.CompletedQuestList == null) {
                var list = original.GetComponentInParent<Gs2QuestOwnCompletedQuestListList>(true);
                if (list != null) {
                    EditorGUILayout.HelpBox("CompletedQuestList is auto assign from Gs2QuestOwnCompletedQuestListList.", MessageType.Info);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("List", list, typeof(Gs2QuestOwnCompletedQuestListList), false);
                    EditorGUI.EndDisabledGroup();
                }
                else {
                    EditorGUILayout.HelpBox("OwnCompletedQuestList not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_completedQuestList"), true);
                }
            }
            else {
                original.CompletedQuestList = EditorGUILayout.ObjectField("OwnCompletedQuestList", original.CompletedQuestList, typeof(OwnCompletedQuestList), false) as OwnCompletedQuestList;
                EditorGUI.BeginDisabledGroup(true);
                if (original.CompletedQuestList != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", original.CompletedQuestList?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("QuestGroupName", original.CompletedQuestList?.QuestGroupName?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}