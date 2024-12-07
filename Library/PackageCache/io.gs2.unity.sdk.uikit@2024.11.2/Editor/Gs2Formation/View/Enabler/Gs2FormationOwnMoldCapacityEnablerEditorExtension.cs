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
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Formation.Editor
{
    [CustomEditor(typeof(Gs2FormationOwnMoldCapacityEnabler))]
    public class Gs2FormationOwnMoldCapacityEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2FormationOwnMoldCapacityEnabler;

            if (original == null) return;

            var context = original.GetComponent<Gs2FormationOwnMoldContext>() ?? original.GetComponentInParent<Gs2FormationOwnMoldContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2FormationOwnMoldContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2FormationOwnMoldContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2FormationOwnMoldList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2FormationOwnMoldContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Mold is auto assign from Gs2FormationOwnMoldList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2FormationOwnMoldContext), false);
                    EditorGUI.indentLevel++;
                    context.Mold = EditorGUILayout.ObjectField("Mold", context.Mold, typeof(OwnMold), false) as OwnMold;
                    if (context.Mold != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.Mold?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("MoldModelName", context.Mold?.MoldModelName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2FormationOwnMoldCapacityEnabler.Expression.In || original.expression == Gs2FormationOwnMoldCapacityEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableCapacities"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableCapacity"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}