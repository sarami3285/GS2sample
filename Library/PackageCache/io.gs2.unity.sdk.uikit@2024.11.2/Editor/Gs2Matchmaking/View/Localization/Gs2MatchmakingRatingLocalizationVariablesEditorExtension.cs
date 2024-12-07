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

using Gs2.Unity.Gs2Matchmaking.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Matchmaking.Context;
using Gs2.Unity.UiKit.Gs2Matchmaking.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2Matchmaking.Localization.Editor
{
    [CustomEditor(typeof(Gs2MatchmakingRatingLocalizationVariables))]
    public class Gs2MatchmakingRatingLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MatchmakingRatingLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2MatchmakingOwnRatingFetcher>() ?? original.GetComponentInParent<Gs2MatchmakingOwnRatingFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2MatchmakingOwnRatingFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2MatchmakingOwnRatingFetcher>();
                }
            }
            else {
                if (fetcher.gameObject.GetComponentInParent<Gs2MatchmakingOwnRatingList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2MatchmakingOwnRatingFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Rating is auto assign from Gs2MatchmakingOwnRatingList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2MatchmakingOwnRatingContext>() ?? original.GetComponentInParent<Gs2MatchmakingOwnRatingContext>(true);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2MatchmakingOwnRatingFetcher), false);
                    EditorGUI.indentLevel++;
                    context.Rating = EditorGUILayout.ObjectField("Rating", context.Rating, typeof(OwnRating), false) as OwnRating;
                    if (context.Rating != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.Rating?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("RatingName", context.Rating?.RatingName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            original.target = EditorGUILayout.ObjectField("Target", original.target, typeof(LocalizeStringEvent), true) as LocalizeStringEvent;

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif