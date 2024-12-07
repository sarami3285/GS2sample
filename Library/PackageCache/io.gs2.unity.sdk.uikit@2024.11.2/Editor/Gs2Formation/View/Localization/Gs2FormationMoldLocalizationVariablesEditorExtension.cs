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

using Gs2.Unity.Gs2Formation.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Formation.Context;
using Gs2.Unity.UiKit.Gs2Formation.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2Formation.Localization.Editor
{
    [CustomEditor(typeof(Gs2FormationMoldLocalizationVariables))]
    public class Gs2FormationMoldLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2FormationMoldLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2FormationOwnMoldFetcher>() ?? original.GetComponentInParent<Gs2FormationOwnMoldFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2FormationOwnMoldFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2FormationOwnMoldFetcher>();
                }
            }
            else {
                if (fetcher.gameObject.GetComponentInParent<Gs2FormationOwnMoldList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2FormationOwnMoldFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Mold is auto assign from Gs2FormationOwnMoldList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2FormationOwnMoldContext>() ?? original.GetComponentInParent<Gs2FormationOwnMoldContext>(true);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2FormationOwnMoldFetcher), false);
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
            original.target = EditorGUILayout.ObjectField("Target", original.target, typeof(LocalizeStringEvent), true) as LocalizeStringEvent;

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif