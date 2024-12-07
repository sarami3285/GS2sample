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

namespace Gs2.Unity.UiKit.Gs2Showcase.Editor
{
    [CustomEditor(typeof(Gs2ShowcaseOwnDisplayItemContext))]
    public class Gs2ShowcaseOwnDisplayItemContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ShowcaseOwnDisplayItemContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.DisplayItem == null) {
                if (original.transform.parent != null && original.transform.parent.GetComponent<Gs2ShowcaseOwnDisplayItemList>() != null) {
                    EditorGUILayout.HelpBox("DisplayItem is auto assign from Gs2ShowcaseDisplayItemList.", MessageType.Info);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("List", original.transform.parent.GetComponent<Gs2ShowcaseOwnDisplayItemList>(), typeof(Gs2ShowcaseOwnDisplayItemList), false);
                    EditorGUI.EndDisabledGroup();
                }
                else {
                    EditorGUILayout.HelpBox("DisplayItem not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("DisplayItem"), true);
                }
            }
            else {
                original.DisplayItem = EditorGUILayout.ObjectField("OwnDisplayItem", original.DisplayItem, typeof(OwnDisplayItem), false) as OwnDisplayItem;
                EditorGUI.BeginDisabledGroup(true);
                if (original.DisplayItem != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", original.DisplayItem?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("ShowcaseName", original.DisplayItem?.ShowcaseName?.ToString());
                    EditorGUILayout.TextField("DisplayItemId", original.DisplayItem?.DisplayItemId?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}