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
 *
 * deny overwrite
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
using Gs2.Unity.UiKit.Gs2Showcase.Context;
using Gs2.Unity.UiKit.Gs2Showcase.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Showcase.Enabler.Editor
{
    [CustomEditor(typeof(Gs2ShowcaseOwnSalesItemNameEnabler))]
    public class Gs2ShowcaseOwnSalesItemNameEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ShowcaseOwnSalesItemNameEnabler;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2ShowcaseOwnDisplayItemFetcher>() ?? original.GetComponentInParent<Gs2ShowcaseOwnDisplayItemFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2ShowcaseOwnDisplayItemFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2ShowcaseOwnDisplayItemFetcher>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Context", fetcher.gameObject, typeof(Gs2ShowcaseOwnDisplayItemFetcher), false);
                EditorGUI.indentLevel++;
                fetcher.Context.DisplayItem = EditorGUILayout.ObjectField("DisplayItem", fetcher.Context.DisplayItem, typeof(OwnDisplayItem), false) as OwnDisplayItem;
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", fetcher.Context.DisplayItem?.NamespaceName?.ToString());
                EditorGUILayout.TextField("DisplayItemId", fetcher.Context.DisplayItem?.DisplayItemId?.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2ShowcaseOwnSalesItemNameEnabler.Expression.In || original.expression == Gs2ShowcaseOwnSalesItemNameEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableNames"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableName"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}