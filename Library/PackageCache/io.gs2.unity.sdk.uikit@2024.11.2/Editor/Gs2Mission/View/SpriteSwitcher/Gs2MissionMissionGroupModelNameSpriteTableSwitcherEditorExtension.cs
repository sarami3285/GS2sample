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

using Gs2.Unity.Gs2Mission.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Mission.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Mission.SpriteSwitcher.Editor
{
    [CustomEditor(typeof(Gs2MissionMissionGroupModelNameSpriteTableSwitcher))]
    public class Gs2MissionMissionGroupModelNameSpriteTableSwitcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MissionMissionGroupModelNameSpriteTableSwitcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2MissionMissionGroupModelContext>() ?? original.GetComponentInParent<Gs2MissionMissionGroupModelContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2MissionMissionGroupModelContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2MissionMissionGroupModelContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2MissionMissionGroupModelList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2MissionMissionGroupModelContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("MissionGroupModel is auto assign from Gs2MissionMissionGroupModelList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2MissionMissionGroupModelContext), false);
                    EditorGUI.indentLevel++;
                    context.MissionGroupModel = EditorGUILayout.ObjectField("MissionGroupModel", context.MissionGroupModel, typeof(MissionGroupModel), false) as MissionGroupModel;
                    if (context.MissionGroupModel != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.MissionGroupModel?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("MissionGroupName", context.MissionGroupModel?.MissionGroupName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("sprites"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultSprite"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}