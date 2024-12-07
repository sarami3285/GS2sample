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
    [CustomEditor(typeof(Gs2ShowcaseRandomDisplayItemRandomShowcaseBuyAction))]
    public class Gs2ShowcaseRandomDisplayItemRandomShowcaseBuyActionEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ShowcaseRandomDisplayItemRandomShowcaseBuyAction;

            if (original == null) return;

            var context = original.GetComponent<Gs2ShowcaseOwnRandomDisplayItemContext>() ?? original.GetComponentInParent<Gs2ShowcaseOwnRandomDisplayItemContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2ShowcaseOwnRandomDisplayItemContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2ShowcaseOwnRandomDisplayItemContext>();
                }
            }
            else {
                if (context.transform.GetComponentInParent<Gs2ShowcaseOwnRandomDisplayItemList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2ShowcaseOwnRandomDisplayItemContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("RandomDisplayItem is auto assign from Gs2ShowcaseRandomDisplayItemList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2ShowcaseOwnRandomDisplayItemContext), false);
                    EditorGUI.indentLevel++;
                    context.RandomDisplayItem = EditorGUILayout.ObjectField("OwnRandomDisplayItem", context.RandomDisplayItem, typeof(OwnRandomDisplayItem), false) as OwnRandomDisplayItem;
                    if (context.RandomDisplayItem != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.RandomDisplayItem?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("ShowcaseName", context.RandomDisplayItem?.ShowcaseName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();

            DrawDefaultInspector();
        }
    }
}