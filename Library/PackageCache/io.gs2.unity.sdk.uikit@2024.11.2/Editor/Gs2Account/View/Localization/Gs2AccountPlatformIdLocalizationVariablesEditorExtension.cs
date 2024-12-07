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

using Gs2.Unity.Gs2Account.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Account.Context;
using Gs2.Unity.UiKit.Gs2Account.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2Account.Localization.Editor
{
    [CustomEditor(typeof(Gs2AccountPlatformIdLocalizationVariables))]
    public class Gs2AccountPlatformIdLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2AccountPlatformIdLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2AccountOwnPlatformIdFetcher>() ?? original.GetComponentInParent<Gs2AccountOwnPlatformIdFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2AccountOwnPlatformIdFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2AccountOwnPlatformIdFetcher>();
                }
            }
            else {
                if (fetcher.gameObject.GetComponentInParent<Gs2AccountOwnPlatformIdList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2AccountOwnPlatformIdFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("PlatformId is auto assign from Gs2AccountOwnPlatformIdList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2AccountOwnPlatformIdContext>() ?? original.GetComponentInParent<Gs2AccountOwnPlatformIdContext>(true);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2AccountOwnPlatformIdFetcher), false);
                    EditorGUI.indentLevel++;
                    context.PlatformId = EditorGUILayout.ObjectField("PlatformId", context.PlatformId, typeof(OwnPlatformId), false) as OwnPlatformId;
                    if (context.PlatformId != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.PlatformId?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("Type", context.PlatformId?.Type.ToString());
                        EditorGUILayout.TextField("UserIdentifier", context.PlatformId?.UserIdentifier?.ToString());
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