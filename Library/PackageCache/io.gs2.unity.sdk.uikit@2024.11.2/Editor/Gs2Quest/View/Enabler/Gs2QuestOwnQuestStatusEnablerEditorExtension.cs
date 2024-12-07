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

namespace Gs2.Unity.UiKit.Gs2Quest.Enabler.Editor
{
    [CustomEditor(typeof(Gs2QuestOwnQuestStatusEnabler))]
    public class Gs2QuestOwnQuestStatusEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2QuestOwnQuestStatusEnabler;

            if (original == null) return;

            {
                var fetcher = original.GetComponent<Gs2QuestOwnCompletedQuestListFetcher>() ?? original.GetComponentInParent<Gs2QuestOwnCompletedQuestListFetcher>(true);
                if (fetcher == null) {
                    EditorGUILayout.HelpBox("Gs2QuestOwnCompletedQuestListFetcher not found.", MessageType.Error);
                    if (GUILayout.Button("Add Context")) {
                        original.gameObject.AddComponent<Gs2QuestOwnCompletedQuestListFetcher>();
                    }
                }
            }
            {
                var fetcher = original.GetComponent<Gs2QuestQuestModelFetcher>() ?? original.GetComponentInParent<Gs2QuestQuestModelFetcher>(true);
                if (fetcher == null) {
                    EditorGUILayout.HelpBox("Gs2QuestQuestModelFetcher not found.", MessageType.Error);
                    if (GUILayout.Button("Add Context")) {
                        original.gameObject.AddComponent<Gs2QuestQuestModelFetcher>();
                    }
                }
                else {
                    if (fetcher.gameObject.GetComponentInParent<Gs2QuestQuestModelList>(true) != null) {
                        EditorGUI.BeginDisabledGroup(true);
                        EditorGUILayout.ObjectField("Context", fetcher.gameObject, typeof(Gs2QuestQuestModelFetcher), false);
                        EditorGUI.EndDisabledGroup();
                        EditorGUILayout.HelpBox("QuestModel is auto assign from Gs2QuestQuestModelList.", MessageType.Info);
                    }
                    else {
                        EditorGUI.BeginDisabledGroup(true);
                        EditorGUILayout.ObjectField("Context", fetcher.gameObject, typeof(Gs2QuestQuestModelFetcher), false);
                        EditorGUI.indentLevel++;
                        fetcher.Context.QuestModel = EditorGUILayout.ObjectField("QuestModel", fetcher.Context.QuestModel, typeof(QuestModel), false) as QuestModel;
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", fetcher.Context.QuestModel?.NamespaceName.ToString());
                        EditorGUILayout.TextField("QuestGroupName", fetcher.Context.QuestModel?.QuestGroupName.ToString());
                        EditorGUILayout.TextField("QuestName", fetcher.Context.QuestModel?.QuestName.ToString());
                        EditorGUI.indentLevel--;
                        EditorGUI.indentLevel--;
                        EditorGUI.EndDisabledGroup();
                    }
                }
            }

            DrawDefaultInspector();
        }
    }
}