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

using System.Globalization;
using Gs2.Core.Util;
using Gs2.Editor.ResourceTree.Core.Editor;
using UnityEditor;

namespace Gs2.Editor.ResourceTree.Gs2Ranking.Editor
{
    public static class CategoryModelEditorExt
    {
        public static void OnGUI(Gs2.Gs2Ranking.Model.CategoryModel item) {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("CategoryModelId", item.CategoryModelId);
            EditorGUILayout.TextField("Name", item.Name);
            EditorGUILayout.TextField("Metadata", item.Metadata);
            EditorGUILayout.TextField("MinimumValue", item.MinimumValue?.ToString());
            EditorGUILayout.TextField("MaximumValue", item.MaximumValue?.ToString());
            EditorGUILayout.TextField("Sum", item.Sum?.ToString());
            EditorGUILayout.TextField("OrderDirection", item.OrderDirection);
            EditorGUILayout.TextField("Scope", item.Scope);
            if (item.GlobalRankingSetting == null) {
                EditorGUILayout.TextField("GlobalRankingSetting", "");
            }
            else {
                EditorGUILayout.LabelField("GlobalRankingSetting");
                EditorGUI.indentLevel++;
                GlobalRankingSettingEditorExt.OnGUI(item.GlobalRankingSetting);
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.TextField("EntryPeriodEventId", item.EntryPeriodEventId);
            EditorGUILayout.TextField("AccessPeriodEventId", item.AccessPeriodEventId);
            EditorGUI.EndDisabledGroup();
        }
    }
}