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
    [CustomEditor(typeof(Gs2MatchmakingOwnRatingContext))]
    public class Gs2MatchmakingOwnRatingContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MatchmakingOwnRatingContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.Rating == null) {
                var list = original.GetComponentInParent<Gs2MatchmakingOwnRatingList>(true);
                if (list != null) {
                    EditorGUILayout.HelpBox("Rating is auto assign from Gs2MatchmakingOwnRatingList.", MessageType.Info);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("List", list, typeof(Gs2MatchmakingOwnRatingList), false);
                    EditorGUI.EndDisabledGroup();
                }
                else if (original.GetComponentInParent<Gs2MatchmakingConvertRatingModelToOwnRating>(true) != null) {
                    EditorGUILayout.HelpBox("Rating is auto assign from Gs2MatchmakingConvertRatingModelToOwnRating.", MessageType.Info);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Converter", original.GetComponentInParent<Gs2MatchmakingConvertRatingModelToOwnRating>(true), typeof(Gs2MatchmakingConvertRatingModelToOwnRating), false);
                    EditorGUI.EndDisabledGroup();
                }
                else {
                    EditorGUILayout.HelpBox("OwnRating not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_rating"), true);
                }
            }
            else {
                original.Rating = EditorGUILayout.ObjectField("OwnRating", original.Rating, typeof(OwnRating), false) as OwnRating;
                EditorGUI.BeginDisabledGroup(true);
                if (original.Rating != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", original.Rating?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("RatingName", original.Rating?.RatingName?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}