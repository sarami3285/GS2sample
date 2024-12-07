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
using Gs2.Unity.UiKit.Gs2Showcase.Context;
using Gs2.Unity.UiKit.Gs2Showcase.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Showcase.Editor
{
    [CustomEditor(typeof(Gs2ShowcaseOwnRandomDisplayItemContext))]
    public class Gs2ShowcaseOwnRandomDisplayItemContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ShowcaseOwnRandomDisplayItemContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.RandomDisplayItem == null) {
                var list = original.GetComponentInParent<Gs2ShowcaseOwnRandomDisplayItemList>(true);
                if (list != null) {
                    EditorGUILayout.HelpBox("RandomDisplayItem is auto assign from Gs2ShowcaseOwnRandomDisplayItemList.", MessageType.Info);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("List", list, typeof(Gs2ShowcaseOwnRandomDisplayItemList), false);
                    EditorGUI.EndDisabledGroup();
                }
                else {
                    EditorGUILayout.HelpBox("OwnRandomDisplayItem not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_randomDisplayItem"), true);
                }
            }
            else {
                original.RandomDisplayItem = EditorGUILayout.ObjectField("OwnRandomDisplayItem", original.RandomDisplayItem, typeof(OwnRandomDisplayItem), false) as OwnRandomDisplayItem;
                EditorGUI.BeginDisabledGroup(true);
                if (original.RandomDisplayItem != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", original.RandomDisplayItem?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("ShowcaseName", original.RandomDisplayItem?.ShowcaseName?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}