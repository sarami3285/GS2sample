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

using Gs2.Unity.Gs2Showcase.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Showcase.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Showcase.Editor
{
    [CustomEditor(typeof(Gs2ShowcaseOwnRandomDisplayItemList))]
    public class Gs2ShowcaseOwnRandomDisplayItemListEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ShowcaseOwnRandomDisplayItemList;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2ShowcaseOwnRandomDisplayItemListFetcher>() ?? original.GetComponentInParent<Gs2ShowcaseOwnRandomDisplayItemListFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2ShowcaseOwnRandomDisplayItemListFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add ListFetcher")) {
                    original.gameObject.AddComponent<Gs2ShowcaseOwnRandomDisplayItemListFetcher>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2ShowcaseOwnRandomDisplayItemListFetcher), false);
                EditorGUI.indentLevel++;
                if (fetcher.Context != null) {
                    EditorGUILayout.ObjectField("OwnRandomShowcase", fetcher.Context.RandomShowcase, typeof(OwnRandomShowcase), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", fetcher.Context.RandomShowcase?.NamespaceName?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("prefab"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumItems"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}