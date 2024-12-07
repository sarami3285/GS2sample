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

using Gs2.Unity.Gs2Matchmaking.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Matchmaking.Context;
using Gs2.Unity.UiKit.Gs2Matchmaking.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Matchmaking.Editor
{
    [CustomEditor(typeof(Gs2MatchmakingVoteVoteMultipleAction))]
    public class Gs2MatchmakingVoteVoteMultipleActionEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MatchmakingVoteVoteMultipleAction;

            if (original == null) return;

            var context = original.GetComponent<Gs2MatchmakingVoteContext>() ?? original.GetComponentInParent<Gs2MatchmakingVoteContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2MatchmakingVoteContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2MatchmakingVoteContext>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2MatchmakingVoteContext), false);
                EditorGUI.indentLevel++;
                context.Vote = EditorGUILayout.ObjectField("Vote", context.Vote, typeof(Vote), false) as Vote;
                if (context.Vote != null) {
                    EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.Vote?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("RatingName", context.Vote?.RatingName?.ToString());
                        EditorGUILayout.TextField("GatheringName", context.Vote?.GatheringName?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();

            DrawDefaultInspector();
        }
    }
}