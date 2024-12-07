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

using Gs2.Unity.Gs2Inbox.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Inbox.Context;
using Gs2.Unity.UiKit.Gs2Inbox.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2Inbox.Localization.Editor
{
    [CustomEditor(typeof(Gs2InboxMessageLocalizationVariables))]
    public class Gs2InboxMessageLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2InboxMessageLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2InboxOwnMessageFetcher>() ?? original.GetComponentInParent<Gs2InboxOwnMessageFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2InboxOwnMessageFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2InboxOwnMessageFetcher>();
                }
            }
            else {
                if (fetcher.gameObject.GetComponentInParent<Gs2InboxOwnMessageList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2InboxOwnMessageFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Message is auto assign from Gs2InboxOwnMessageList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2InboxOwnMessageContext>() ?? original.GetComponentInParent<Gs2InboxOwnMessageContext>(true);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2InboxOwnMessageFetcher), false);
                    EditorGUI.indentLevel++;
                    context.Message = EditorGUILayout.ObjectField("Message", context.Message, typeof(OwnMessage), false) as OwnMessage;
                    if (context.Message != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.Message?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("MessageName", context.Message?.MessageName?.ToString());
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