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
    [CustomEditor(typeof(Gs2FormationOwnSlotContext))]
    public class Gs2FormationOwnSlotContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2FormationOwnSlotContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.Slot == null) {
                var list = original.GetComponentInParent<Gs2FormationOwnSlotList>(true);
                if (list != null) {
                    EditorGUILayout.HelpBox("OwnSlot is auto assign from Gs2FormationOwnSlotList.", MessageType.Info);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("List", list, typeof(Gs2FormationOwnSlotList), false);
                    EditorGUI.EndDisabledGroup();
                }
                else if (original.GetComponentInParent<Gs2FormationConvertSlotModelToOwnSlot>(true) != null) {
                    EditorGUILayout.HelpBox("Slot is auto assign from Gs2FormationConvertSlotModelToOwnSlot.", MessageType.Info);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Converter", original.GetComponentInParent<Gs2FormationConvertSlotModelToOwnSlot>(true), typeof(Gs2FormationConvertSlotModelToOwnSlot), false);
                    EditorGUI.EndDisabledGroup();
                }
                else {
                    EditorGUILayout.HelpBox("OwnSlot not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_slot"), true);
                }
            }
            else {
                original.Slot = EditorGUILayout.ObjectField("OwnSlot", original.Slot, typeof(OwnSlot), false) as OwnSlot;
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", original.Slot?.NamespaceName.ToString());
                EditorGUILayout.TextField("MoldName", original.Slot?.MoldModelName.ToString());
                EditorGUILayout.TextField("Index", original.Slot?.Index.ToString());
                EditorGUILayout.TextField("SlotName", original.Slot?.SlotName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}