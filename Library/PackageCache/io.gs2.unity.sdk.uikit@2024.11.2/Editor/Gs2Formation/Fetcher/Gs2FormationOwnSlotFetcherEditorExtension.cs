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

using Gs2.Unity.Gs2Formation.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Formation.Context;
using Gs2.Unity.UiKit.Gs2Formation.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Formation.Editor
{
    [CustomEditor(typeof(Gs2FormationOwnSlotFetcher))]
    public class Gs2FormationOwnSlotFetcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2FormationOwnSlotFetcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2FormationOwnSlotContext>() ?? original.GetComponentInParent<Gs2FormationOwnSlotContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2FormationOwnSlotContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2FormationOwnSlotContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2FormationOwnSlotList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2FormationOwnSlotContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Slot is auto assign from Gs2FormationOwnSlotList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2FormationOwnSlotContext), false);
                    EditorGUI.indentLevel++;
                    context.Slot = EditorGUILayout.ObjectField("Slot", context.Slot, typeof(OwnSlot), false) as OwnSlot;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.Slot?.NamespaceName.ToString());
                    EditorGUILayout.TextField("MoldName", context.Slot?.MoldModelName.ToString());
                    EditorGUILayout.TextField("Index", context.Slot?.Index.ToString());
                    EditorGUILayout.TextField("SlotName", context.Slot?.SlotName.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }
            
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onError"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}